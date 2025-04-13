using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public enum EActorState
{
    None,
    WaitSync, // 创建好时  等待更新
    Syncing,  // 同步位置到服务器
    Ready,    // 同步完成  可以操作
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

    [SerializeField] protected Animator m_animator = null;

    public Animator GetAnimator()
    {
        return m_animator;
    }
    

    public void InitActor()
    {
        // Init Actor
        actorState = EActorState.WaitSync;
       
        OnInit();
    }
   

    public virtual void DoFixedUpdate()
    {
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
        return  EActorRoleType.None;
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
}