

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