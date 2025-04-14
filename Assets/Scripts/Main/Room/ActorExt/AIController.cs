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
    protected override void OnInit()
    {
        base.OnInit();
        configItem = MonsterConfig.GetConfigItem(ConfigId);
        behaviorTree = new BehaviorTree();
        behaviorTree.Init(new BTGenInfo(configItem.AiId),this);
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
}