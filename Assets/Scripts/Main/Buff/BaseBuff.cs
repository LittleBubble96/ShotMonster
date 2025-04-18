using UnityEngine;

public class BaseBuff : IRecycle
{
    public int BuffId { get; set; }
    //施放buff ActorID
    public int ReleaseActorId { get; set; }


    //目标 buff ActorID
    public int TargetActorId { get; set; }

    //持续时间
    public float Duration { get; set; }
    private float durationTimeCount;

    //循环时间
    public float LoopTime { get; set; }
    private float loopTimeCount;

    //循环次数
    public int LoopCount { get; set; }
    private int loopCountCount;
    
    //结束标志
    public bool IsEnd { get; set; }

    private BuffConfigItem buffConfigItem;
    
    public bool CanOverlay => buffConfigItem.CanOverlay;

    public void Init(int buffId,BuffConfigItem buffConfig, int releaseActorId, int targetActorId, CapParameter parameter)
    {
        BuffId = buffId;
        ReleaseActorId = releaseActorId;
        TargetActorId = targetActorId;
        buffConfigItem = buffConfig;
        Duration = buffConfigItem.Duration;
        LoopTime = buffConfigItem.LoopTime;
        LoopCount = buffConfigItem.LoopCount;
        OnParameterChange(parameter);
        durationTimeCount = Duration;
        loopTimeCount = LoopTime;
        loopCountCount = 0;
    }

    public void Overlay(CapParameter parameter)
    {
        OnParameterChange(parameter);
    }

    protected virtual void OnParameterChange(CapParameter parameter)
    {
        //可以在这里修改参数
    }
    public virtual void OnStart()
    {
        
    }

    public virtual void OnAttach()
    {
        
    }

    public virtual void OnEnd()
    {
        if (!IsEnd) return;
    }
    
    public EBufferType GetBuffType()
    {
        return buffConfigItem.BuffType;
    }

    #region 保持
    
    protected virtual void OnKeepUpdate(float dt)
    {
        
    }

    #endregion
    
    #region 循环
    protected virtual void OnLoop()
    {
        
    }
    #endregion
   
    public void DoUpdate(float dt)
    {
        //
        if (IsEnd)
        {
            return;
        }
        if (CheckKeep())
        {
            KeepUpdate(dt);
        }
        else if (CheckLoop())
        {
            LoopUpdate(dt);
        }
        else
        {
            End();
        }
    }
    
    private void KeepUpdate(float dt)
    {
        durationTimeCount -= dt;
        OnKeepUpdate (dt);
        if (durationTimeCount <= 0)
        {
            End();
        }
    }
    
    private void LoopUpdate(float dt)
    {
        //如果LoopCount < 0 则无限循环
        if (LoopCount < 0 || loopCountCount < LoopCount)
        {
            loopTimeCount -= dt;
            if (loopTimeCount <= 0)
            {
                loopCountCount++;
                OnLoop();
                loopTimeCount = LoopTime;
            }
        }
        else
        {
            End();
        }
    }

    private void End()
    {
        IsEnd = true;
    }
    

    //检查buff是否持续
    private bool CheckKeep()
    {
        return Duration > 0 && LoopTime <= 0;
    }
    
    //检查buff是否循环
    private bool CheckLoop()
    {
        return LoopTime > 0 && Duration <= 0;
    }


    public void Recycle()
    {
        buffConfigItem = null;
        ReleaseActorId = 0;
        TargetActorId = 0;
        Duration = 0;
        LoopTime = 0;
        LoopCount = 0;
        IsEnd = false;
    }
    
}
