using System.Collections.Generic;

//1.火焰持续灼烧buff  2.中毒持续掉血 3.冰冻 4.几秒内加快主角攻速 5.主角加血 6.主角攻速增加  7.怪物死亡金币掉落概率增加
public enum EBufferType
{
    None = 0,
    //火焰
    Burn = 1,
    //中毒
    Poison = 2,
    //冰冻
    Freeze = 3,
    //几秒内加快主角攻速
    SpeedBoost = 4,
    //主角加血
    Heal = 5,
    //主角攻速增加
    AttackSpeed = 6,
    //怪物死亡金币掉落概率增加
    GoldDropRate = 7,
    //基础上海
    BaseMonsterDamage = 8,
}

public class BuffConfigItem
    {       
        public int Id;
        public string Name;
        public string Icon;
        public string Desc;
       
        public EBufferType BuffType;

        public float LoopTime = -1f; //如果循环时间小于0，则表示不循环

        public int LoopCount = -1; //如果循环次数小于0，则表示无限循环
        
        public float Duration = -1f; //持续时间，-1表示直接生效
        
        public bool CanOverlay = false; //是否可以叠加

    }
    public class BuffConfig
    {
        public static Dictionary<int, BuffConfigItem> ConfigDict =
            new Dictionary<int, BuffConfigItem>()
            {
                {
                    1,
                    new BuffConfigItem()
                        { Id = 1, 
                            Name = "Buff1", 
                            Icon = "Buff1", 
                            Desc = "火伤持续", 
                            BuffType = EBufferType.Burn, 
                            LoopTime = 1f, 
                            LoopCount = 5,
                            }
                },
                {
                    2,
                    new BuffConfigItem()
                        { Id = 2, 
                            Name = "Buff2", 
                            Icon = "Buff2", 
                            Desc = "中毒持续",
                            BuffType = EBufferType.Poison, 
                            LoopTime = 1f, 
                            LoopCount = 5,
                            }
                },
                {
                    3,
                    new BuffConfigItem()
                        { Id = 3, 
                            Name = "Buff3", 
                            Icon = "Buff3", 
                            Desc = "冰冻",
                            BuffType = EBufferType.Freeze, 
                            Duration = 2,
                            }
                },
                {
                    4,
                    new BuffConfigItem()
                        { Id = 4, 
                            Name = "Buff4", 
                            Icon = "Buff4", 
                            Desc = "一定时间内加速",
                            BuffType = EBufferType.SpeedBoost, 
                            Duration = 2,
                            }
                },
                {
                    5,
                    new BuffConfigItem()
                        { Id = 5, 
                            Name = "Buff5", 
                            Icon = "Buff5", 
                            Desc = "直接回血",
                            BuffType = EBufferType.Heal, 
                            }
                },
                {
                    6,
                    new BuffConfigItem()
                        { Id = 6, 
                            Name = "Buff6", 
                            Icon = "Buff6", 
                            Desc = "攻速增加",
                            BuffType = EBufferType.AttackSpeed, 
                            }
                },
                {
                    7,
                    new BuffConfigItem()
                        { Id = 7, 
                            Name = "Buff7", 
                            Icon = "Buff7", 
                            Desc = "金币掉落概率增加",
                            BuffType = EBufferType.GoldDropRate, 
                            }
                },
                {
                    8,
                    new BuffConfigItem()
                        { Id = 8, 
                            Name = "怪物基础上海", 
                            Icon = "", 
                            Desc = "基础伤害增加",
                            BuffType = EBufferType.BaseMonsterDamage, 
                            Duration = 1,
                            CanOverlay = true,
                            }
                },
            };

        public static BuffConfigItem GetConfigItem(int CfgId)
        {
            if (ConfigDict.ContainsKey(CfgId))
            {
                return ConfigDict[CfgId];
            }

            return null;
        }
        
    }
    