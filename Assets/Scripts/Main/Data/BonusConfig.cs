using System.Collections.Generic;

//1.火焰持续灼烧buff  2.中毒持续掉血 3.冰冻 4.几秒内加快主角攻速 5.主角加血 6.主角攻速增加  7.怪物死亡金币掉落概率增加
public enum EBonusType
{
    None = 0,
    //穿透
    CrossBullet = 1,
    // 加速
    SpeedUpAlways = 2,
    //攻击力永久增加百分之20
    AttackUpAlways = 3,
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
        public string Param1, Param2, Param3;
        
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
                            Name = "cross bullet", 
                            Icon = "Buff1", 
                            Desc = "when hit monster,bullet can cross monster", 
                            BonusType = EBonusType.CrossBullet, 
                            BonusQuality = EBonusQuality.Silver,
                            }
                },
                {
                    2,
                    new BonusConfigItem()
                        { 
                            Id = 2, 
                            Name = "加速", 
                            Icon = "Buff1", 
                            Desc = "attack duration always increase 20%", 
                            BonusType = EBonusType.SpeedUpAlways, 
                            BonusQuality = EBonusQuality.Gold,
                            Param1 = "0.2",
                            }
                },
                {
                    3,
                    new BonusConfigItem()
                        { 
                            Id = 3, 
                            Name = "attack up", 
                            Icon = "Buff1", 
                            Desc = "attack always up 60%", 
                            BonusType = EBonusType.AttackUpAlways, 
                            BonusQuality = EBonusQuality.Color,
                            Param1 = "0.6",
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
        
        public static Dictionary<int, BonusConfigItem> GetAllConfig()
        {
            return ConfigDict;
        }
    }
    