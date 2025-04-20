using UnityEngine;

public class BaseMonsterDamageBuff : BaseBuff
{
    private int damage;
    
    protected override void OnParameterChange(CapParameter parameter)
    {
        base.OnParameterChange(parameter);
        if (parameter is CapParameter<int,float> damageParameter)
        {
            damage = damageParameter.Value;
            Duration = damageParameter.Value1;
        }
        //Debug.Log( "[buff] BaseMonsterDamageBuff OnParameterChange:  Dur" + Duration);
    }

    protected override void OnKeepUpdate(float dt)
    {
        base.OnKeepUpdate(dt);
        //Debug.Log( "[buff] BaseMonsterDamageBuff OnKeepUpdate:  Dur" + Duration);
    }

    public override void OnStart()
    {
        base.OnStart();
        Actor target = RoomManager.Instance.GetActorById(TargetActorId);
        if (target != null)
        {
            if (target.GetActorState() == EActorState.Play)
            {
                target.Damage(damage);
                if (target.GetActorState() != EActorState.WaitDestroy)
                {
                    target.PlayAnimation("Damage");
                    target.UpdateAttribute(EMonsterAttribute.DamageIncreaseSpeed, -2f);
                }
            }
        }
        //Debug.Log("[buff] BaseMonsterDamageBuff OnStart");
    }

    public override void OnEnd()
    {
        base.OnEnd();
        Actor target = RoomManager.Instance.GetActorById(TargetActorId);
        if (target != null)
        {
            target.UpdateAttribute(EMonsterAttribute.DamageIncreaseSpeed, 0);
        }
        Debug.Log("[buff] BaseMonsterDamageBuff OnEnd");
    }
}