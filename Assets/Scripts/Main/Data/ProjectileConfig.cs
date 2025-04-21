

using System.Collections.Generic;
using UnityEngine;

public class ProjectileConfigItem
{
    public int Id;
    public string Name;
    public string Prefab;
    public string StartEffect;
    public string HitEffect;
}
public class ProjectileConfig
{
    public static Dictionary<int, ProjectileConfigItem> ConfigDict =
        new Dictionary<int, ProjectileConfigItem>()
        {
            {
                1,
                new ProjectileConfigItem()
                { Id = 1, Name = "通用", Prefab = "Trail05_Orange",StartEffect ="Trail05_Orange", HitEffect = "Trail05_Orange", }
            },
            {
                2,
                new ProjectileConfigItem()
                { Id = 2, Name = "火球", Prefab = "Projectile_Fireball",StartEffect ="Muzzle_Fireball", HitEffect = "Hit_Fireball", }
            },
            {
                3,
                new ProjectileConfigItem()
                { Id = 3, Name = "冰球", Prefab = "IceBall",StartEffect ="IceBall", HitEffect = "IceBall", }
            },
            {
                4,
                new ProjectileConfigItem()
                { Id = 4, Name = "雷电", Prefab = "ThunderBall",StartEffect ="ThunderBall", HitEffect = "ThunderBall", }
            },
            {
                5,
                new ProjectileConfigItem()
                { Id = 5, Name = "毒球", Prefab = "PoisonBall",StartEffect ="PoisonBall", HitEffect = "PoisonBall", }
            },
            {
                21,
                new ProjectileConfigItem()
                { Id = 21, Name = "冰火球", Prefab = "IceFireBall",StartEffect ="IceFireBall", HitEffect = "IceFireBall", }
            },
            
        };

    public static ProjectileConfigItem GetConfigItem(int cfgId)
    {
        if (ConfigDict.ContainsKey(cfgId))
        {
            return ConfigDict[cfgId];
        }

        return null;
    }


}