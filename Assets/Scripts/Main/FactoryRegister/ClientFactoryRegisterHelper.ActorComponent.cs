public partial class ClientFactoryRegisterHelper
{
    public static void RegisterActorComponent()
    {
        ClientFactory.Instance.GetActorComponentFactory().RegisterType<TargetComponent>(typeof(TargetComponent));
        ClientFactory.Instance.GetActorComponentFactory().RegisterType<AttackComponent>(typeof(AttackComponent));
    }
}