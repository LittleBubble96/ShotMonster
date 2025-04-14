


using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnConfigItem
{
    public int Id;
    public string Name;
    public int[] MonsterIds;
    public float RandomRadius;
    public Vector3 Position;
    public Vector3 Rotation;
    public float ReSpawnTime;
}
public class MonsterSpawnConfig
{
    public static Dictionary<int, MonsterSpawnConfigItem> ConfigDict =
        new Dictionary<int, MonsterSpawnConfigItem>()
        {
            {
                1,
                new MonsterSpawnConfigItem()
                { Id = 1, Name = "Point1", MonsterIds =new []{1,2}, 
                    RandomRadius = 1f,Position = new Vector3(-39, 0, -19), Rotation = new Vector3() ,
                    ReSpawnTime = 3000f,
                }
            },
            {
                2,
                new MonsterSpawnConfigItem()
                {
                    Id = 2, Name = "Point2", MonsterIds =new []{1,2}, RandomRadius = 1f, Position = new Vector3(19f, 0f, 39f), Rotation = new Vector3() ,
                    ReSpawnTime = 3000f,
                }
            }
            ,
            {
                3,
                new MonsterSpawnConfigItem()
                {
                    Id = 3, Name = "Point3", MonsterIds =new []{1,2}, RandomRadius = 1f, Position = new Vector3(39f, 0f, 19f), Rotation = new Vector3() ,
                    ReSpawnTime = 3000f,
                }
            }
        };

    public static MonsterSpawnConfigItem GetConfigItem(int cfgId)
    {
        if (ConfigDict.ContainsKey(cfgId))
        {
            return ConfigDict[cfgId];
        }

        return null;
    }


}