public abstract class SystemBase
{
    protected Actor Owner;
    
    public void Init(Actor owner)
    {
        this.Owner = owner;
    }
    public abstract void OnExecute(float dt);
}