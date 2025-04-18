using System.Collections.Generic;

//1.火焰持续灼烧buff  2.中毒持续掉血 3.冰冻 4.几秒内加快主角攻速 5.主角加血 6.主角攻速增加  7.怪物死亡金币掉落概率增加
public enum EBonusType
{
    None = 0,
    //穿透
    CrossBullet = 1,
}

public enum EBonusQuality
{
    None = 0,
    //银色
    Silver = 1,
    //金色
    Gold = 2,
    //彩色
    Color = 3,
}

public class BonusConfigItem
    {       
        public int Id;
        public string Name;
        public string Icon;
        public string Desc;
       
        public EBonusType BonusType;
        public EBonusQuality BonusQuality;
        
    }
    public class BonusConfig
    {
        public static Dictionary<int, BonusConfigItem> ConfigDict =
            new Dictionary<int, BonusConfigItem>()
            {
                {
                    1,
                    new BonusConfigItem()
                        { 
                            Id = 1, 
                            Name = "穿透", 
                            Icon = "Buff1", 
                            Desc = "子弹击中后穿透", 
                            BonusType = EBonusType.CrossBullet, 
                            BonusQuality = EBonusQuality.Silver,
                            }
                },
                {
                    2,
                    new BonusConfigItem()
                        { 
                            Id = 2, 
                            Name = "穿透", 
                            Icon = "Buff1", 
                            Desc = "子弹击中后穿透", 
                            BonusType = EBonusType.CrossBullet, 
                            BonusQuality = EBonusQuality.Gold,
                            }
                },
            };

        public static BonusConfigItem GetConfigItem(int CfgId)
        {
            if (ConfigDict.ContainsKey(CfgId))
            {
                return ConfigDict[CfgId];
            }

            return null;
        }
        
    }
    