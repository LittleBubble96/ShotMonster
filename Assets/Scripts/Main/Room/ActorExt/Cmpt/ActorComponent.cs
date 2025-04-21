public class ActorComponent : IRecycle
{
    protected Actor actor;
    public void Init(Actor actor)
    {
        this.actor = actor;
        OnInit();
    }

    public void Recycle()
    {
        actor = null;
    }

    public Actor GetActor()
    {
        return actor;
    }

    protected virtual void OnInit()
    {
        
    }
}