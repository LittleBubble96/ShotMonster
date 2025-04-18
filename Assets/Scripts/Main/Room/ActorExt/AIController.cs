using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Actor
{
    //行为树
    protected BehaviorTree behaviorTree;
    private MonsterConfigItem configItem;
    //Nav
    [SerializeField]
    private NavMeshAgent navMeshAgent;

    [SerializeField] private float attackAnimDuration = 0.875f;
    [SerializeField] private float attackHitTime = 0.5f;
    [SerializeField] private float hurtAnimDuration = 0.7f;
    [SerializeField] private float deathAnimDuration = 1f;
    protected override void OnInit()
    {
        base.OnInit();
        configItem = MonsterConfig.GetConfigItem(ConfigId);
        InitAttribute();
        behaviorTree = new BehaviorTree();
        behaviorTree.Init(new BTGenInfo(configItem.AiId),this);
        AgentStart();
        //停止距离 为 攻击距离
        // navMeshAgent.stoppingDistance = GetAttackDistance();
    }

    public override void DoFixedUpdate(float dt)
    {
        base.DoFixedUpdate(dt);
        if (behaviorTree != null)
        {
            behaviorTree.Execute(dt);
        }
         // lerp 旋转
        Vector3 targetDir = navMeshAgent.desiredVelocity;
        if (targetDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
        navMeshAgent.speed = GetMoveSpeed();
        float speed = navMeshAgent.velocity.magnitude;
        SetFloat("MoveSpeedPet",speed);
    }

    
    //攻击动画时长
    public float GetAttackAnimDuration()
    {
        return attackAnimDuration;
    }
    
    //攻击命中时长
    public float GetAttackHitTime()
    {
        return attackHitTime;
    }
    
    //攻击距离
    public float GetAttackDistance()
    {
        return configItem.AttackRange;
    }
    
    public float GetAttackInterval()
    {
        return configItem.AttackInterval;
    }
    
    public void AgentStop()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = true;
        }
    }
    
    public void AgentStart()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = false;
        }
    }
    
    
    //设置目标位置
    public void SetTargetPosition(Vector3 pos)
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(pos);
        }
    }
    
    //初始化变量
    private void InitAttribute()
    {
        AddAttribute(EMonsterAttribute.BaseSpeed, navMeshAgent.speed);
        AddAttribute(EMonsterAttribute.DamageIncreaseSpeed, 0);
        AddAttribute(EActorAttribute.HP, configItem.Hp);
        AddAttribute(EActorAttribute.MaxHP, configItem.Hp);
        AddAttribute(EMonsterAttribute.HurtAnimDuration, hurtAnimDuration);
        AddAttribute(EMonsterAttribute.DeathAnimDuration, deathAnimDuration);
    }

    private float GetMoveSpeed()
    {
        float baseSpeed = GetFloatAttribute(EMonsterAttribute.BaseSpeed);
        float damageIncreaseSpeed = GetFloatAttribute(EMonsterAttribute.DamageIncreaseSpeed);
        return baseSpeed + damageIncreaseSpeed;
    }

    protected override void OnDamage(int damage)
    {
        base.OnDamage(damage);
        UpdateAttribute(EActorAttribute.HP, GetFloatAttribute(EActorAttribute.HP) - damage);
        if (GetFloatAttribute(EActorAttribute.HP) <= 0)
        {
            //死亡
            WaitDestroy();
        }
    }

    protected override void OnWaitDestroy()
    {
        base.OnWaitDestroy();
        m_stateTimeCount = GetFloatAttribute(EMonsterAttribute.DeathAnimDuration);
        AgentStop();
    }
}

