using System.Collections.Generic;

public class MonsterSpawnController
{
    private List<MonsterSpawnPoint> _monsterSpawnPoints = new List<MonsterSpawnPoint>();
    public void Init()
    {
        // Initialize the monster spawn controller
        ParseSpawnConfig();
    }

    protected void ParseSpawnConfig()
    {
        foreach (var config in MonsterSpawnConfig.ConfigDict)
        {
            MonsterSpawnPoint spawnPoint = new MonsterSpawnPoint(config.Value);
            _monsterSpawnPoints.Add(spawnPoint);
        }
    }
    
    public void DoUpdate(float dt)
    {
        foreach (var spawnPoint in _monsterSpawnPoints)
        {
            spawnPoint.DoUpdate(dt);
        }
    }

}