using UnityEngine;

public enum MonsterSpawnState
{
    None = 0,
    WaitSpawn = 1,
    Spawn = 2,
}

public class MonsterSpawnPoint
{
    private MonsterSpawnConfigItem _configItem;
    private float _spawnTime;
    private MonsterSpawnState _spawnState;
    protected MonsterSpawnState SpawnState
    {
        get => _spawnState;
        set
        {
            _spawnState = value;
            if (_spawnState == MonsterSpawnState.WaitSpawn)
            {
                _spawnTime = _configItem.ReSpawnTime;
            }
            else if (_spawnState == MonsterSpawnState.Spawn)
            {
                SpawnState = MonsterSpawnState.WaitSpawn;
            }
        }
    }
    
    public MonsterSpawnPoint(MonsterSpawnConfigItem configItem)
    {
        _configItem = configItem;
        _spawnState = MonsterSpawnState.WaitSpawn;
    }
    
    public void DoUpdate(float dt)
    {
        if (_configItem == null)
        {
            return;
        }
        
        if (_spawnState == MonsterSpawnState.WaitSpawn)
        {
            _spawnTime -= dt;
            if (_spawnTime <= 0)
            {
                SpawnState = MonsterSpawnState.Spawn;
                SpawnMonster();
            }
        }
    }
    
    protected void SpawnMonster()
    {
        int randomMonsterIndex = Random.Range(0, _configItem.MonsterIds.Length);
        int monsterId = _configItem.MonsterIds[randomMonsterIndex];
        float randomX = Random.Range(-_configItem.RandomRadius, _configItem.RandomRadius);
        float randomZ = Random.Range(-_configItem.RandomRadius, _configItem.RandomRadius);
        Vector3 spawnPos = new Vector3(_configItem.Position.x + randomX, _configItem.Position.y, _configItem.Position.z + randomZ);
        MonsterConfigItem monsterConfigItem = MonsterConfig.GetConfigItem(monsterId);
        if (monsterConfigItem == null)
        {
            Debug.LogError($"MonsterConfigItem not found for id: {monsterId}");
            return;
        }
        RoomManager.Instance.CreateActor(monsterId,EActorRoleType.Monster,monsterConfigItem.Prefab,spawnPos,Quaternion.identity,null);
    }
}