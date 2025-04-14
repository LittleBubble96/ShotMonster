using System;
using System.Collections;
using UnityEngine;

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
    }


    public override IEnumerator OnExitAsync()
    {
        EffectManager.Instance.StopAllEffects();
        return null;
    }
}