public class BonusBase : IRecycle
{
    public BonusConfigItem ConfigItem { get; private set; }
    public virtual void Init(BonusConfigItem configItem ,CapParameter parameter)
    {
        ConfigItem = configItem;
    }
    public void Recycle()
    {
        
    }
}