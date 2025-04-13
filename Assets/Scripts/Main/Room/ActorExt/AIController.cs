using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : Actor
{
    //行为树
    protected BehaviorTree behaviorTree;
    //Nav
    [SerializeField]
    private NavMeshAgent navMeshAgent;

    [SerializeField] private float attackAnimDuration = 0.875f;
    [SerializeField] private float attackHitTime = 0.5f;
    protected override void OnInit()
    {
        base.OnInit();
        behaviorTree = new BehaviorTree();
        behaviorTree.Init(new BTGenInfo(0),this);
        //停止距离 为 攻击距离
        // navMeshAgent.stoppingDistance = GetAttackDistance();
    }

    public override void DoFixedUpdate()
    {
        base.DoFixedUpdate();
        if (behaviorTree != null)
        {
            behaviorTree.Execute(Time.fixedDeltaTime);
        }
         // lerp 旋转
        Vector3 targetDir = navMeshAgent.desiredVelocity;
        if (targetDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
        float speed = navMeshAgent.velocity.magnitude;
        // GetAnimationController().SetFloat("MoveSpeedPet",speed);
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