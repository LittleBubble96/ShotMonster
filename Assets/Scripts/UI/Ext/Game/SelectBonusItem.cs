using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectBonusItem : MonoBehaviour
{
    [SerializeField] private Image qualityIcon;
    [SerializeField] private Image bonusIcon;
    [SerializeField] private TextMeshProUGUI bonusName;
    [SerializeField] private TextMeshProUGUI bonusDesc;
    [SerializeField] private Button bonusButton;

    public int BonusId { get; private set; }
    public Action<int> OnClickAction { get; set; }
    
    public void Init(int bonusId)
    {
        BonusId = bonusId;
        var bonusConfigItem = BonusConfig.GetConfigItem(BonusId);
        SetQualityIcon(bonusConfigItem.BonusQuality);
        SetIcon();
        bonusName.text = bonusConfigItem.Name;
        bonusDesc.text = bonusConfigItem.Desc;
        bonusButton.onClick.AddListener(() =>
        {
            OnClickAction?.Invoke(BonusId); 
        });
    }
    
    private void SetIcon()
    {
        
    }
    
    private void SetQualityIcon(EBonusQuality quality)
    {
        switch (quality)
        {
            case EBonusQuality.Silver:
                qualityIcon.color = Color.white;
                break;
            case EBonusQuality.Gold:
                qualityIcon.color = Color.yellow;
                break;
            case EBonusQuality.Color:
                qualityIcon.color = Color.cyan;
                break;
        }
    }
}