using System.Collections.Generic;

public class MonsterConfigItem
    {
        public int Id;
        public string Name;
        public string Icon;
        public string Prefab;
        public string Desc;
        public int AiId;
        //hp
        public int Hp;
        //攻击伤害
        public int AttackDamage;
        //攻击距离
        public float AttackRange;
        //取消攻击距离
        public float CancelAttackRange;
        //攻击间隔
        public float AttackInterval;
    }

public class MonsterConfig
{
    public static Dictionary<int, MonsterConfigItem> ConfigDict =
        new Dictionary<int, MonsterConfigItem>()
        {
            {
                1,
                new MonsterConfigItem()
                {
                    Id = 1, Name = "Pet1", Icon = "Pet1", Prefab = "Role/Pet/Pet1", Desc = "Pet1", AiId = 1001,
                    AttackRange = 2f, CancelAttackRange = 5f, AttackInterval = 2f, AttackDamage = 1,Hp = 100
                }
            },
            {
                2,
                new MonsterConfigItem()
                {
                    Id = 2, Name = "Pet2", Icon = "Pet2", Prefab = "Role/Pet/Pet2", Desc = "Pet2", AiId = 1001,
                    AttackRange = 2f, CancelAttackRange = 5f, AttackInterval = 3f, AttackDamage = 1 ,Hp = 100
                }
            }
        };

    public static MonsterConfigItem GetConfigItem(int CfgId)
    {
        if (ConfigDict.ContainsKey(CfgId))
        {
            return ConfigDict[CfgId];
        }

        return null;
    }

}