using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public enum EActorState
{
    None,
    Play,
    WaitDestroy,
}

public enum EActorRoleType
{
    None,
    Player,
    Monster,
    Interactive,
    BreakInteractive,
}

public enum CAP_ControlMode
{
    /// <summary>
    /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
    /// </summary>
    Player,
    /// <summary>
    /// Character freely moves in the chosen direction from the perspective of the camera
    /// </summary>
    Server
}

public class Actor : RecycleObject
{
    private EActorState actorState = EActorState.None;
    private EActorRoleType actorRoleType = EActorRoleType.None;

    private int actorId = 0;
    /// 角色配置ID
    protected int ConfigId = 0;
    [SerializeField] protected Animator m_animator = null;

    
    public Animator GetAnimator()
    {
        return m_animator;
    }
    
    public int GetActorId()
    {
        return actorId;
    }
    

    public void InitActor(int id ,EActorRoleType roleType , int configId)
    {
        // Init Actor
        actorState = EActorState.Play;
        actorRoleType = roleType;
        ConfigId = configId;
        actorId = id;
        OnInit();
    }
   

    public virtual void DoFixedUpdate(float dt)
    {
    }
    
    public virtual void DoUpdate(float dt)
    {
        // Update Actor
        if (actorState == EActorState.Play)
        {
            UpdateSystem(dt);
        }
    }

    //客户端控制

    public EActorState GetActorState()
    {
        return actorState;
    }
    
    public void SetActorState(EActorState state)
    {
        actorState = state;
        OnChangeState(state);
    }
    
    protected virtual void OnChangeState(EActorState state)
    {
        
    }

    public EActorRoleType GetActorRoleType()
    {
        return actorRoleType;
    }
    
    
   
    protected virtual void OnInit()
    {
        
    }

    #region 组件

    //逻辑组件
    protected ConcurrentDictionary<Type,ActorComponent> m_actorComponents = new ConcurrentDictionary<Type, ActorComponent>();
    
    public T GetActorComponent<T>() where T : ActorComponent
    {
        Type type = typeof(T);
        if (m_actorComponents.ContainsKey(type))
        {
            return m_actorComponents[type] as T;
        }
        return null;
    }
    
    public T TryOrAddActorComponent<T>() where T : ActorComponent, new()
    {
        Type type = typeof(T);
        if (m_actorComponents.ContainsKey(type))
        {
            return m_actorComponents[type] as T;
        }
        else
        {
            ActorComponent component = ClientFactory.Instance.GetActorComponentFactory().GetObject(type);
            if (component != null)
            {
                component.Init(this);
                m_actorComponents.TryAdd(type, component);
                return component as T;
            }
        }

        return null;
    }

    #endregion

    #region 系统

    protected List<SystemBase> m_systems = new List<SystemBase>();
    
    protected void RegisterSystem<T>() where T : SystemBase, new()
    {
        SystemBase system = new T();
        system.Init(this);
        m_systems.Add(system);
    }

    protected void UpdateSystem(float dt)
    {
        foreach (var system in m_systems)
        {
            system.OnExecute(dt);
        }
    }


    #endregion

    #region 销毁
    
    public void DestroyActor()
    {
        //销毁组件
        foreach (var component in m_actorComponents)
        {
            ClientFactory.Instance.GetActorComponentFactory().PutObject(component.Value);
        }
        m_actorComponents.Clear();
        
        
        //销毁Actor
        Destroy(gameObject);
    }
    

    #endregion

    #region 动画

    public void SetBool(string name, bool value)
    {
        if (m_animator != null)
        {
            m_animator.SetBool(name, value);
        }
    }
    
    public void SetFloat(string name, float value)
    {
        if (m_animator != null)
        {
            m_animator.SetFloat(name, value);
        }
    }
    
    public void SetLayerWeight(int layer, float weight)
    {
        if (m_animator != null)
        {
            m_animator.SetLayerWeight(layer, weight);
        }
    }
    

    #endregion

    #region 接口

    public float GetDistance(Actor actor)
    {
        if (actor == null)
        {
            return 0;
        }
        return Vector3.Distance(transform.position, actor.transform.position);
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    


    #endregion
}