

public class AttackUpRateAlwaysBonus : BonusBase, IDirectBonus
{
    private float attackUpValue;
    public override void Init(BonusConfigItem configItem, CapParameter parameter)
    {
        base.Init(configItem, parameter);
        attackUpValue = float.Parse(configItem.Param1);
    }

    public void OnDirectBonus()
    {
        int mainPlayerId = RoomManager.Instance.GetMainPlayerId();
        BuffFunc.AttackUpRate(mainPlayerId,mainPlayerId,attackUpValue);
    }
}