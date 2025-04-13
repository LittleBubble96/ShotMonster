using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    private int curActorId = 0;

    private int mainPlayerId = 0;

    private int maxActorId = 10000;

    private Dictionary<int, Actor> actorDict = new Dictionary<int, Actor>();
    public void Init()
    {
        // 初始化房间管理器
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

    public void CreateActor( string resName , Vector3 position , Quaternion rotation , System.Action<Actor> callback)
    {
        GameManager.Instance.StartCoroutine(GOtPoolManager.Instance.GetAsync<Actor>(resName, (actor) =>
        {
            SetActorPosition(actor, position, rotation);
            int actorId = GenerateActorId();
            actor.InitActor();
            actorDict[actorId] = actor;
            callback?.Invoke(actor);
        }));
        
    }

    private void SetActorPosition(Actor actor, Vector3 position, Quaternion rotation)
    {
        //如果是 characterController 则设置位置
        CharacterController characterController = actor.GetComponent<CharacterController>();
        if (characterController != null)
        {
            characterController.enabled = false;
        }
        actor.transform.position = position;
        actor.transform.rotation = rotation;
        if (characterController != null)
        {
            characterController.enabled = true;
        }
    }


}

