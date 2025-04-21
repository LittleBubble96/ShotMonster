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
    Destroy,
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
    [SerializeField] protected Animator m_animator = null;
    [SerializeField] protected Transform m_hudTransform = null;
    private EActorState actorState = EActorState.None;
    private EActorRoleType actorRoleType = EActorRoleType.None;

    private int actorId = 0;

    /// 角色配置ID
    protected int ConfigId = 0;
    
    protected float m_stateTimeCount = 0f;


    public Animator GetAnimator()
    {
        return m_animator;
    }

    public int GetActorId()
    {
        return actorId;
    }


    public void InitActor(int id, EActorRoleType roleType, int configId)
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
            UpdateHudState(dt);
        }
    }
    
    public virtual void DoUpdateWaitDestroy(float dt)
    {
        UpdateHudState(dt);
        m_stateTimeCount -= dt;
        if (m_stateTimeCount <= 0)
        {
            actorState = EActorState.Destroy;
        }
    }
    
    protected void UpdateHudState(float dt)
    {
        if (m_hudBase != null)
        {
            m_hudBase.DoUpdate(dt);
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
    protected ConcurrentDictionary<Type, ActorComponent> m_actorComponents =
        new ConcurrentDictionary<Type, ActorComponent>();

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
    
    public void PlayAnimation(string name , float blendTime = 0.25f)
    {
        if (m_animator != null)
        {
            m_animator.CrossFade(name,blendTime,0,0f);
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

    #region 属性

    private Dictionary<Enum, object> m_attributes = new Dictionary<Enum, object>();

    protected void AddAttribute(Enum name, object value)
    {
        if (m_attributes.ContainsKey(name))
        {
            m_attributes[name] = value;
        }
        else
        {
            m_attributes.Add(name, value);
        }
    }

    public float GetFloatAttribute(Enum name)
    {
        if (m_attributes.ContainsKey(name))
        {
            return Convert.ToSingle(m_attributes[name]);
        }

        return 0;
    }
    public int GetIntAttribute(Enum name)
    {
        if (m_attributes.ContainsKey(name))
        {
            return Convert.ToInt32(m_attributes[name]);
        }

        return 0;
    }

    public void UpdateAttribute(Enum name, object value)
    {
        if (m_attributes.ContainsKey(name))
        {
            m_attributes[name] = value;
        }
        else
        {
            m_attributes.Add(name, value);
        }
        OnAttributeChange(name, value);
    }

    protected virtual void OnAttributeChange(Enum name, object value)
    {
        if (Equals(name, EActorAttribute.HP))
        {
            ChangeHudValue();
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

    public void Damage(int damage)
    {
        //TODO : 伤害
        OnDamage(damage);
    }

    protected virtual void OnDamage(int damage)
    {

    }

    protected void WaitDestroy()
    {
        actorState = EActorState.WaitDestroy;
        OnWaitDestroy();
    }

    protected virtual void OnWaitDestroy()
    {
    }

    #endregion

    #region hud

    private HudBase m_hudBase = null;
    private EHubState m_hudState = EHubState.None;

    protected EHubState hudState
    {
        get { return m_hudState; }
        set
        {
            m_hudState = value;
            OnHudStateChange(m_hudState);
        }
    }

    protected virtual void OnHudStateChange(EHubState state)
    {
        if (m_hudBase!= null)
        {
            GOtPoolManager.Instance.Return(m_hudBase);
        }
        GameManager.Instance.StartCoroutine(GOtPoolManager.Instance.GetAsync<HudBase>(HudHelper.GetHubRes(state),
            (obj) =>
            {
                m_hudBase = obj;
                if (m_hudBase != null)
                {
                    m_hudBase.Init(GetIntAttribute(EActorAttribute.MaxHP));
                    m_hudBase.transform.SetParent(m_hudTransform);
                    m_hudBase.transform.localPosition = Vector3.zero;
                    m_hudBase.transform.localScale = Vector3.one;
                }
            }));
    }

    protected void ChangeHudValue()
    {
        int hp = GetIntAttribute(EActorAttribute.HP);
        if (m_hudBase != null)
        {
            m_hudBase.SetValue(hp);
        }
    }

    #endregion
}