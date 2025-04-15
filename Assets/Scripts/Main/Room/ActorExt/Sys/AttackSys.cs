using UnityEngine;

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
        // Owner.SetLayerWeight(1,targetComponent.TargetIsValid() ? 1 : 0);

        attackComponent.CurrentAttackTime += dt;
        if (attackComponent.CurrentAttackTime > attackComponent.AttackSpeed)
        {
            //can attack
            Owner.SetBool("attack", true);
            attackComponent.CurrentAttackTime = 0;
            attackComponent.CurrentAttackAnimationTime = attackComponent.AttackAnimationTime;
            attackComponent.WaitAttack = true;
        }
        
        //更新攻击动画时间
        if (attackComponent.CurrentAttackAnimationTime > 0)
        {
            attackComponent.CurrentAttackAnimationTime -= dt;
            //检测是否可以攻击
            if (attackComponent.WaitAttack && attackComponent.AttackAnimationTime - attackComponent.CurrentAttackAnimationTime > attackComponent.AttackAnimationKeyFrameTime)
            {
                //到攻击关键帧了
                Attack(attackComponent);
                attackComponent.WaitAttack = false;
            }
            if (attackComponent.CurrentAttackAnimationTime <= 0)
            {
                attackComponent.CurrentAttackAnimationTime = 0;
                Owner.SetBool("attack", false);
            }
        }
    }

    private void Attack(AttackComponent attackComponent)
    {
        //组件一定存在
        Transform[] attackPoints = attackComponent.GetCurrentMuzzles();
        if (attackPoints == null || attackPoints.Length == 0)
        {
            return;
        }
        for (int i = 0; i < attackPoints.Length; i++)
        {
            Transform attackPoint = attackPoints[i];
            if (attackPoint == null)
            {
                continue;
            }
            Actor owner = attackComponent.GetActor();
            if (owner == null)
            {
                return;
            }

            Vector3 dir = owner.transform.forward;
            dir =  attackPoint.localRotation * dir;
            ProjectileManager.Instance.CreateProjectile(attackComponent.ProjectileConfigId,
                attackPoint.position,
                dir,attackComponent.GetActor().GetActorId()
                );
        }
    }
}