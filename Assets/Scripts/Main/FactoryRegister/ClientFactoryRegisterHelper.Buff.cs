public partial class ClientFactoryRegisterHelper
{ 
        public static void RegisterBuff()
        {
                ClientFactory.Instance.GetBaseBuffFactory().RegisterType<BuffBurn>(EBufferType.Burn);
                ClientFactory.Instance.GetBaseBuffFactory().RegisterType<BaseMonsterDamageBuff>(EBufferType.BaseMonsterDamage);
                ClientFactory.Instance.GetBaseBuffFactory().RegisterType<SpeedUpBuff>(EBufferType.AttackSpeed);
                ClientFactory.Instance.GetBaseBuffFactory().RegisterType<AttackRateUpBuff>(EBufferType.AttackUpRateAlways);
        }

}