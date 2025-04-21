

public class AttackRateUpBuff: BaseBuff
{
    private float attakRate;
    
    protected override void OnParameterChange(CapParameter parameter)
    {
        base.OnParameterChange(parameter);
        if (parameter is CapParameter<float> damageParameter)
        {
            attakRate = damageParameter.Value;
        }
        //Debug.Log( "[buff] BaseMonsterDamageBuff OnParameterChange:  Dur" + Duration);
    }
    

    public override void OnStart()
    {
        base.OnStart();
        Actor target = RoomManager.Instance.GetActorById(TargetActorId);
        if (target != null)
        {
            target.UpdateAttribute(EActorAttribute.Attack, target.GetIntAttribute(EActorAttribute.Attack) * 1 + attakRate);
        }
    }
}