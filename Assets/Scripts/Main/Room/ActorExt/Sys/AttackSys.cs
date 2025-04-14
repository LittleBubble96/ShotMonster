public class AttackSys : SystemBase
{
    public override void OnExecute(float dt)
    {
        AttackComponent attackComponent = Owner.GetActorComponent<AttackComponent>();
        if (attackComponent == null)
        {
            return;
        }
        TargetComponent targetComponent = Owner.GetActorComponent<TargetComponent>();
        if (targetComponent == null)
        {
            return;
        }
        Owner.SetLayerWeight(1,targetComponent.TargetIsValid() ? 1 : 0);

        attackComponent.CurrentAttackTime += dt;
        if (attackComponent.CurrentAttackTime > attackComponent.AttackSpeed)
        {
            //can attack
            Owner.SetBool("attack", true);
            attackComponent.CurrentAttackTime = 0;
            attackComponent.CurrentAttackAnimationTime = attackComponent.AttackAnimationTime;
            
        }
        
        //更新攻击动画时间
        if (attackComponent.CurrentAttackAnimationTime > 0)
        {
            attackComponent.CurrentAttackAnimationTime -= dt;
            if (attackComponent.CurrentAttackAnimationTime <= 0)
            {
                attackComponent.CurrentAttackAnimationTime = 0;
                Owner.SetBool("attack", false);
            }
        }
    }
}