using System.Collections.Generic;

public class SelectBonus_PopupUI : UIBase
{
    private SelectBonusItem[] bonusItems;

    public override void OnInit()
    {
        base.OnInit();
        bonusItems = GetComponentsInChildren<SelectBonusItem>();
        int[] bonusIds = BonusMgr.Instance.GetRandomBonus(GameConst.BonusCount);
        for (int i = 0; i < bonusItems.Length; i++)
        {
            if (i < bonusIds.Length)
            {
                bonusItems[i].Init(bonusIds[i]);
                bonusItems[i].gameObject.SetActive(true);
                bonusItems[i].OnClickAction += (id) =>
                {
                    BonusMgr.Instance.AddBonus(id,null);
                    GameManager.GetAppEventDispatcher().BroadcastListener(EventName.Event_SelectBonusOver);
                    Hide();
                };
            }
            else
            {
                bonusItems[i].gameObject.SetActive(false);
            }
        }
    }
}