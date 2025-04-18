using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EGameRunningState
{
    None = 0,
    Running = 1,
    WaitSelectBonus = 2,
}

public class RoomGameState : GameStateBase
{
    public override IEnumerator OnEnterAsync()
    {
        GameManager.GetUIManager().ShowUI<RoomGame_UI>();
        yield return OnEnterAsync_Internal();
    }
    
    private IEnumerator OnEnterAsync_Internal()
    {
        
        // yield return new WaitUntil(() => RoomManager.Instance.RoomState == ERoomState.Loading);
        GameManager.GetAppEventDispatcher().BroadcastListener(EventName.EVENT_LoadingUIProcess, 0.3f);
        RoomManager.Instance.RoomState = ERoomState.Loading;
        //加载场景 0.3 - 0.6
        yield return CAP_SceneManager.Instance.LoadScene("GameScene",null, (progress) =>
        {
            GameManager.GetAppEventDispatcher().BroadcastListener(EventName.EVENT_LoadingUIProcess, 0.3f + progress * 0.3f);
        });
        //加载场景物体
        // yield return RoomManager.Instance.LoadSceneActor((process) =>
        // {
        //     GameManager.GetAppEventDispatcher().BroadcastListener(EventName.EVENT_LoadingUIProcess, 0.6f + 0.3f * process);
        // });
        //通知创建 当前角色 并初始化相机
        RoomManager.Instance.CreateActor(-1,EActorRoleType.Player,"Character/C1",Vector3.zero, Quaternion.identity, (actor) =>
        {
            RoomManager.Instance.SetMainPlayer(actor);
        });
        //默认添加一个计时器
        AddBonusListener(new TimeBonusListener(10f));
        GameManager.GetAppEventDispatcher().AddEventListener<EventType>(EventName.Event_SelectBonusOver,OnSelectBonusOver);
        RoomManager.Instance.RoomState = ERoomState.Playing;
    }


    public override IEnumerator OnExitAsync()
    {
        EffectManager.Instance.StopAllEffects();
        GameManager.GetAppEventDispatcher().RemoveEventListener<EventType>(EventName.Event_SelectBonusOver,OnSelectBonusOver);
        foreach (var listener in bonusListeners)
        {
            listener.OnDestroy();
        }
        return null;
    }

    #region 逻辑
    
    private IBonusListener bonusListener;
    
    private void OnSelectBonusOver<TEvent>(TEvent obj) where TEvent : EventType
    {
        GameRunningState = EGameRunningState.Running;
    }
    
    private EGameRunningState gameRunningState = EGameRunningState.Running;
    
    protected EGameRunningState GameRunningState
    {
        get => gameRunningState;
        set
        {
            gameRunningState = value;
            switch (gameRunningState)
            {
                case EGameRunningState.Running:
                    //恢复时间
                    Time.timeScale = 1f;
                    if (bonusListener != null)
                    {
                        bonusListener.Reset();
                    }
                    break;
                case EGameRunningState.WaitSelectBonus:
                    //时停
                    Time.timeScale = 0.1f;
                    GameManager.GetUIManager().ShowUI<SelectBonus_PopupUI>();
                    break;
            }
        }
    }
    
    private List<IBonusListener> bonusListeners = new List<IBonusListener>();
    
    public void AddBonusListener(IBonusListener listener)
    {
        if (bonusListeners.Contains(listener))
        {
            return;
        }
        bonusListeners.Add(listener);
        listener.OnInit();
    }

    public override void DoUpdate(float dt)
    {
        base.DoUpdate(dt);
        if (GameRunningState == EGameRunningState.Running)
        {
            foreach (var listener in bonusListeners)
            {
                if (listener.OnSuc(dt))
                {
                    bonusListener = listener;
                    GameRunningState = EGameRunningState.WaitSelectBonus;
                }
            }
        }
    }

    #endregion
}