public partial class ClientFactoryRegisterHelper
{
    public static void RegisterBonus()
    {
        ClientFactory.Instance.GetBonusFactory().RegisterType<CrossBulletBonus>(EBonusType.CrossBullet);
    }
}