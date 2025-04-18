public class SpeedUpBuff: BaseBuff
{
    private float speedRate;
    
    protected override void OnParameterChange(CapParameter parameter)
    {
        base.OnParameterChange(parameter);
        if (parameter is CapParameter<float> damageParameter)
        {
            speedRate = damageParameter.Value;
        }
        //Debug.Log( "[buff] BaseMonsterDamageBuff OnParameterChange:  Dur" + Duration);
    }
    

    public override void OnStart()
    {
        base.OnStart();
        Actor target = RoomManager.Instance.GetActorById(TargetActorId);
        if (target != null)
        {
            target.UpdateAttribute(EPlayerAttribute.AttackBonusSpeedUpAlways, speedRate);
        }
        //Debug.Log("[buff] BaseMonsterDamageBuff OnStart");
    }
}