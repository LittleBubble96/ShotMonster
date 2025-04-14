using UnityEngine;

public class BTPetAttackTN : BTTaskNode
{
    private float attackInterval = 0;
    private float attackAnimTime = 0;
    private float attackHitTime = 0;
    protected override void OnRecycle()
    {
        attackInterval = 0;
        attackAnimTime = 0;
        attackHitTime = 0;
    }

    protected override void OnBegin()
    {
        attackInterval = behaviorTree.GetAIController().GetAttackInterval();
        //播放攻击动画
        attackAnimTime = behaviorTree.GetAIController().GetAttackAnimDuration();
        attackHitTime = behaviorTree.GetAIController().GetAttackHitTime();
        behaviorTree.GetAIController().SetBool("Attack",true);
        Debug.Log("[Attack] Set Attack Animation: " + attackAnimTime + "attackInterval: " + attackInterval + " attackHitTime: " + attackHitTime);
    }

    protected override void OnEnd()
    {
        
    }

    protected override BtNodeResult OnExecute(float deltaTime)
    {
        if (attackInterval > 0)
        {
            attackInterval -= deltaTime;
            //攻击间隔未到
            if (attackAnimTime > 0)
            {
                attackAnimTime -= deltaTime;
                if (attackAnimTime <= 0)
                {
                    behaviorTree.GetAIController().SetBool("Attack",false);
                }
            }
            //打击时间未到
            if (attackHitTime > 0)
            {
                attackHitTime -= deltaTime;
                if (attackHitTime <= 0)
                {
                    TargetComponent target = behaviorTree.GetAIController().GetActorComponent<TargetComponent>();
                    if (target != null)
                    {
                        Actor targetActor = RoomManager.Instance.GetActorById(target.TargetActorId);
                        Vector3 hitPos = GetAttackHitPosition(behaviorTree.GetAIController(),targetActor);
                        //TODO : 发送攻击命中事件
                    }
                }
            }
            return BtNodeResult.InProgress;
        }
        behaviorTree.GetAIController().SetBool("Attack",false);
        return BtNodeResult.Succeeded;
    }

    protected override void OnParseParams(string[] args)
    {
        
    }
    
    protected Vector3 GetAttackHitPosition(Actor petActor, Actor targetActor)
    {
        //从宠物位置打出射线 只检测“Break”Tag
        Vector3 petPos = petActor.transform.position;
        Vector3 targetPos = targetActor.transform.position;
        Vector3 dir = targetPos - petPos;
        Ray ray = new Ray(petPos, dir);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, dir.magnitude + 1, LayerMaskMgr.PlayerLayerMask))
        {
            //Debug.Log("[Attack] Hit: " + hit.collider.name);
            return hit.point;
        }
        return targetPos;
    }
    
}