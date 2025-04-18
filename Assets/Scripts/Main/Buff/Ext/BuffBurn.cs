//火焰 

public class BuffBurn : BaseBuff
{
    protected override void OnParameterChange(CapParameter parameter)
    {
        base.OnParameterChange(parameter);
        // if (parameter is Bu burnParameter)
        // {
        //     durationTimeCount = burnParameter.DurationTime;
        //     damage = burnParameter.Damage;
        // }
    }

    public override void OnStart()
    {
        base.OnStart();
        //添加火焰特效在 目标上
    }

    public override void OnEnd()
    {
        base.OnEnd();
        //结束火焰特效
    }
}