using System;
using System.Collections.Generic;
using UnityEngine;

public class BonusMgr : Singleton<BonusMgr>
{
    private List<BonusBase> activeBonuses = new List<BonusBase>();
    private DefaultBonus defaultBonus;
    
    //缓存集合
    private List<IHitBonus> hitBonusList = new List<IHitBonus>();
    private Dictionary<EBonusQuality, List<int>> qualityBonusCfgDict = new Dictionary<EBonusQuality, List<int>>();
    private List<int> allBonusCfgList = new List<int>();
    
    public void Init()
    {
        // Initialize the default bonus
        defaultBonus = new DefaultBonus();
        Dictionary<int,BonusConfigItem> configDict = BonusConfig.GetAllConfig();
        foreach (var item in configDict)
        {
            if (!qualityBonusCfgDict.ContainsKey(item.Value.BonusQuality))
            {
                qualityBonusCfgDict[item.Value.BonusQuality] = new List<int>();
            }
            qualityBonusCfgDict[item.Value.BonusQuality].Add(item.Key);
            allBonusCfgList.Add(item.Key);
        }
    }

    public void AddBonus(int bonusCfgId,CapParameter parameter)
    {
        BonusConfigItem bonusConfig = BonusConfig.GetConfigItem(bonusCfgId);
        if (bonusConfig == null)
        {
            Debug.LogError($"BonusConfigItem with ID {bonusCfgId} not found.");
            return;
        }

        BonusBase newBonus = ClientFactory.Instance.GetBonusFactory().GetObject(bonusConfig.BonusType);
        if (newBonus != null)
        {
            newBonus.Init(bonusConfig,parameter);
            activeBonuses.Add(newBonus);
            UpdateTypeDict(newBonus);
        }
        OnDirect(newBonus);
    }

    //直接调用
    private void OnDirect(BonusBase newBonus)
    {
        if (newBonus == null)
        {
            return;
        }
        if (newBonus is IDirectBonus directBonus)
        {
            directBonus.OnDirectBonus();
        }
    }

    //击中后调用
    public void OnHitMonster(RaycastHit hit, ProjectileBase projectile, Actor target)
    {
        BonusExecuteResult result = BonusExecuteResult.None;
        foreach (var bonus in hitBonusList)
        {
            if (bonus != null)
            {
                result |= bonus.OnHitMonster(hit, projectile, target);
                if ((result & BonusExecuteResult.BreakAllBonus) != 0)
                {
                    break;
                }
            }
        }
        if ((result & BonusExecuteResult.BreakDefaultBonus) == 0)
        {
            // Handle breaking default bonus logic here
            defaultBonus.OnHitMonster(hit, projectile, target);
        }
    }

    private void UpdateTypeDict(BonusBase bonus)
    {
        if (bonus is IHitBonus hitBonus)
        {
            hitBonusList .Add(hitBonus);
        }
    }

    #region 翻牌
    
    //翻牌 
    public int[] GetRandomBonus(int count)
    {
        int[] randomBonuses = new int[count];
        List<int> allCfgList = new List<int>(allBonusCfgList);
        for (int i = 0; i < count; i++)
        {
            if (allCfgList.Count == 0)
            {
                break;
            }
            int randomIndex = UnityEngine.Random.Range(0, allCfgList.Count);
            randomBonuses[i] = allCfgList[randomIndex];
            allCfgList.RemoveAt(randomIndex);
        }
        return randomBonuses;
    }
    

    #endregion

}