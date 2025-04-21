public partial class ClientFactoryRegisterHelper
{
    public static void RegisterBonus()
    {
        ClientFactory.Instance.GetBonusFactory().RegisterType<CrossBulletBonus>(EBonusType.CrossBullet);
        ClientFactory.Instance.GetBonusFactory().RegisterType<SpeedUpAlwaysBonus>(EBonusType.SpeedUpAlways);
        ClientFactory.Instance.GetBonusFactory().RegisterType<AttackUpRateAlwaysBonus>(EBonusType.AttackUpAlways);
        ClientFactory.Instance.GetBonusFactory().RegisterType<FireBallBonus>(EBonusType.FireBall);
    }
}