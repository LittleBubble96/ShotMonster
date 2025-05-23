﻿public class SpeedUpAlwaysBonus : BonusBase, IDirectBonus
{
    private float speedUpValue;
    public override void Init(BonusConfigItem configItem, CapParameter parameter)
    {
        base.Init(configItem, parameter);
        speedUpValue = float.Parse(configItem.Param1);
    }

    public void OnDirectBonus()
    {
        int mainPlayerId = RoomManager.Instance.GetMainPlayerId();
        BuffFunc.SpeedUp(mainPlayerId,mainPlayerId,speedUpValue);
    }
}