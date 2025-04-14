using UnityEngine;

public abstract class BaseBuff
{
    protected GameObject target;
    protected float duration;
    protected float value;
    protected float startTime;

    public BaseBuff(GameObject target, float duration, float value)
    {
        this.target = target;
        this.duration = duration;
        this.value = value;
        this.startTime = Time.time;
    }

    public virtual void OnStart() { }

    public virtual void OnUpdate() { }

    public virtual void OnEnd() { }

    public bool IsExpired()
    {
        return Time.time >= startTime + duration;
    }
}
