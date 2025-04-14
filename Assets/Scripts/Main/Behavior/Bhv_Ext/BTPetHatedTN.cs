using UnityEngine;

public class BTPetHatedTN : BTTaskNode
{
    //如果找不到的时间间隔 
    private float findTargetInterval = 1f;
    private float findTargetTime = 0;
    protected override void OnRecycle()
    {
        
    }

    protected override void OnBegin()
    {
        TargetComponent targetComponent = behaviorTree.GetAIController().TryOrAddActorComponent<TargetComponent>();
        findTargetTime = findTargetInterval;
        //toDO 先 直接设置为主角
        Actor actor = GetTargetActor();
        if (actor == null)
        {
            return;
        }
        targetComponent.SetTargetActorId(actor.GetActorId());
    }

    protected override void OnEnd()
    {
        
    }

    protected override BtNodeResult OnExecute(float deltaTime)
    {
        findTargetTime -= deltaTime;
        if (findTargetTime > 0)
        {
            return BtNodeResult.InProgress;
        }
        findTargetTime = findTargetInterval;
        TargetComponent targetComponent = behaviorTree.GetAIController().GetActorComponent<TargetComponent>();
        Actor actor = GetTargetActor();
        if (actor == null)
        {
            return BtNodeResult.Failed;
        }
        targetComponent.SetTargetActorId(actor.GetActorId());
        targetComponent = behaviorTree.GetAIController().GetActorComponent<TargetComponent>();
        return targetComponent.TargetIsValid() ? BtNodeResult.Succeeded : BtNodeResult.Failed;
    }

    protected override void OnParseParams(string[] args)
    {
        
    }

    private Actor GetTargetActor()
    {
        return RoomManager.Instance.GetMainPlayer();
    }
}