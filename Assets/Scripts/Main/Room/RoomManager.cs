using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class RoomManager : Singleton<RoomManager>
{
    private int curActorId = 0;

    private int mainPlayerId = 0;

    private int maxActorId = 10000;

    private Dictionary<int, Actor> actorDict = new Dictionary<int, Actor>();
    private List<Actor> actorList = new List<Actor>();
    private Queue<Actor> destoryActorList = new Queue<Actor>();
    
    private CameraManager cameraManager;
    private MonsterSpawnController monsterSpawnController;
    
    private ERoomState roomState = ERoomState.None;
    public ERoomState RoomState
    {
        get { return roomState; }
        set { 
            roomState = value;
            if (roomState == ERoomState.None)
            {
                actorList.Clear();
                actorDict.Clear();
                destoryActorList.Clear();
            }
        }
    }
    public void Init()
    {
        // 初始化房间管理器
        monsterSpawnController = new MonsterSpawnController();
        monsterSpawnController.Init();
        RoomState = ERoomState.None;
    }

    private int GenerateActorId()
    {
        curActorId++;
        if (curActorId >= maxActorId)
        {
            curActorId = 0;
        }
        while (actorDict.ContainsKey(curActorId))
        {
            curActorId++;
            if (curActorId >= maxActorId)
            {
                curActorId = 0;
            }
        }
        return curActorId;
    }
    
    public void DoFixedUpdate(float dt)
    {
        if (roomState != ERoomState.Playing)
        {
            return;
        }

        for (int i = actorList.Count - 1; i >= 0; i--)
        {
            Actor actor = actorList[i];
            if (actor != null && actor.GetActorState() == EActorState.Play)
            {
                actor.DoFixedUpdate(dt);
            }
        }
    }
    
    public void DoUpdate(float dt)
    {
        if (roomState != ERoomState.Playing)
        {
            return;
        }
        if (monsterSpawnController != null)
        {
            monsterSpawnController.DoUpdate(dt);
        }
        for (int i = actorList.Count - 1; i >= 0; i--)
        {
            Actor actor = actorList[i];
            if (actor != null && actor.GetActorState() == EActorState.Play)
            {
                actor.DoUpdate(dt);
            }
            else if (actor != null && actor.GetActorState() == EActorState.WaitDestroy)
            {
                actor.DoUpdateWaitDestroy(dt);
            }
            if (actor != null && actor.GetActorState() == EActorState.Destroy)
            {
                destoryActorList.Enqueue(actor);
            }
        }
        
        while (destoryActorList.Count > 0)
        {
            Actor actor = destoryActorList.Dequeue();
            DestroyActor(actor);
        }
    }

    public void CreateActor( int configId , EActorRoleType role ,string resName , Vector3 position , Quaternion rotation , System.Action<Actor> callback)
    {
        GameManager.Instance.StartCoroutine(GOtPoolManager.Instance.GetAsync<Actor>(resName, (actor) =>
        {
            SetActorPosition(actor, position, rotation);
            int actorId = GenerateActorId();
            actor.InitActor(actorId,role,configId);
            AddActor(actor);
            callback?.Invoke(actor);
        }));
        
    }
    
    private void AddActor(Actor actor)
    {
        if (actor == null)
        {
            return;
        }
        actorList.Add(actor);
        actorDict[actor.GetActorId()] = actor;
    }
    
    private void RemoveActor(Actor actor)
    {
        if (actor == null)
        {
            return;
        }
        actorList.Remove(actor);
        actorDict.Remove(actor.GetActorId());
    }
    
    public void SetMainPlayer(Actor actor)
    {
        if (actor == null)
        {
            return;
        }
        mainPlayerId = actor.GetActorId();
        CameraManager.Init(actor.transform);
    }
    
    //获取主角
    public Actor GetMainPlayer()
    {
        if (actorDict.ContainsKey(mainPlayerId))
        {
            return actorDict[mainPlayerId];
        }
        return null;
    }
    
    public int GetMainPlayerId()
    {
        return mainPlayerId;
    }

    private void SetActorPosition(Actor actor, Vector3 position, Quaternion rotation)
    {
        //如果是 characterController 则设置位置
        CharacterController characterController = actor.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false;
        }
        NavMeshAgent agent = actor.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
             agent.Warp(position);
        }
        else
        {
            actor.transform.position = position;
        }

        actor.transform.rotation = rotation;
        if (characterController != null)
        {
            characterController.enabled = true;
        }
    }

    
    public CameraManager CameraManager 
    {
        get
        {
            if (cameraManager == null)
            {
                cameraManager = GameObject.FindObjectOfType<CameraManager>();
            }
            return cameraManager;
        }
    }

    public void DestroyActor(Actor actor)
    {
        if (actor == null)
        {
            return;
        }
        actor.SetActorState(EActorState.None);
        RemoveActor(actor);
        GOtPoolManager.Instance.Return(actor);
    }
    
    public List<Actor> GetActorList(Predicate<Actor> predicate = null)
    {
        if (predicate != null)
        {
            return actorList.FindAll(predicate);
        }
        return actorList;
    }
    
    public Actor GetActorById(int actorId)
    {
        if (actorDict.ContainsKey(actorId))
        {
            return actorDict[actorId];
        }
        return null;
    }

}

