
public class FireBallBonus : BonusBase, IDirectBonus
{
    public override void Init(BonusConfigItem configItem, CapParameter parameter)
    {
        base.Init(configItem, parameter);
    }

    public void OnDirectBonus()
    {
        int mainPlayerId = RoomManager.Instance.GetMainPlayerId();
        ActorAttachFireBall(mainPlayerId);
    }
    
    private void ActorAttachFireBall(int actorId)
    {
        Actor actor = RoomManager.Instance.GetActorById(actorId);
        if (actor != null)
        {
            ProjectileComponent projectileComponent = actor.GetActorComponent<ProjectileComponent>();
            projectileComponent.AddProjectileAttr(EAttackAttr.Fire);
        }
    }
}