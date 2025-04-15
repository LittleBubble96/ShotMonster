public partial class ClientFactoryRegisterHelper
{ 
        public static void RegisterBuff()
        {
                ClientFactory.Instance.GetBaseBuffFactory().RegisterType<BuffBurn>(EBufferType.Burn);
                ClientFactory.Instance.GetBaseBuffFactory().RegisterType<BaseMonsterDamageBuff>(EBufferType.BaseMonsterDamage);
        }

}