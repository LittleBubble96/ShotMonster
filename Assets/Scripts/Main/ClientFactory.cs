public class ClientFactory
{
    private readonly MutilFactoryWithPool<GameStateBase> gameStateFactory = new MutilFactoryWithPool<GameStateBase>();
    private readonly MutilTypeFactoryWithPool<BehaviorNode> behaviorNodeFactory = new MutilTypeFactoryWithPool<BehaviorNode>();
    private readonly MutilTypeFactoryWithPool<ActorComponent> actorComponentFactory = new MutilTypeFactoryWithPool<ActorComponent>();
    private readonly MutilFactoryWithPool<BaseBuff> baseBuffFactory = new MutilFactoryWithPool<BaseBuff>();
    private readonly MutilFactoryWithPool<BonusBase> bonusFactory = new MutilFactoryWithPool<BonusBase>();
    
    protected static ClientFactory _instance = new ClientFactory();

    public MutilFactoryWithPool<GameStateBase> GetGameStateFactory()
    {
        return gameStateFactory;
    }
    
    public MutilTypeFactoryWithPool<BehaviorNode> GetBehaviorNodeFactory()
    {
        return behaviorNodeFactory;
    }

    public MutilTypeFactoryWithPool<ActorComponent> GetActorComponentFactory()
    {
        return actorComponentFactory;
    }
    
    public MutilFactoryWithPool<BaseBuff> GetBaseBuffFactory()
    {
        return baseBuffFactory;
    }
    
    public MutilFactoryWithPool<BonusBase> GetBonusFactory()
    {
        return bonusFactory;
    }

    public static ClientFactory Instance
    {
        get
        {
            return _instance;
        }
    }

}