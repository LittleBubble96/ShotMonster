using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomMain_UI : UIBase
{
   [SerializeField] private Button beginBtn;


   public override void OnInit()
   {
      base.OnInit();
      //初始调用刷新房间列表
      beginBtn.onClick.AddListener(() =>
      {
         GameManager.GetGameStateMachine().ChangeGameState(GameStateEnum.Game);
      });
     
   }

   public override void OnShow()
   {
      base.OnShow();
     
   }
   

   public override void OnHide()
   {
      base.OnHide();
   }

   public override void DoUpdate(float dt)
   {
      base.DoUpdate(dt);
    
   }
   
   
}
