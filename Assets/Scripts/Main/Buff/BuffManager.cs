// buff管理器  需要支持
//1.火焰持续灼烧buff  2.中毒持续掉血 3.冰冻 4.几秒内加快主角攻速 5.主角加血 6.主角攻速增加  7.怪物死亡金币掉落概率增加
//需要有特效支持
using UnityEngine;
using System.Collections.Generic;

public class BuffManager : Singleton<BuffManager>
{

    private Dictionary<int, BaseBuff> activeBuffs = new Dictionary<int, BaseBuff>();
    private int buffIdCounter = 0;

    // Buff类型枚举
    // 添加buff
    public int AddBuff(int buffId, BuffParameter parameter)
    {
        buffIdCounter++;
        BaseBuff newBuff = CreateBuff(type, target, duration, value);
        if (newBuff != null)
        {
            activeBuffs.Add(buffIdCounter, newBuff);
            newBuff.OnStart();
        }
        return buffIdCounter;
    }

    // 移除buff
    public void RemoveBuff(int buffId)
    {
        if (activeBuffs.ContainsKey(buffId))
        {
            activeBuffs[buffId].OnEnd();
            activeBuffs.Remove(buffId);
        }
    }

    private BaseBuff CreateBuff(BuffType type, GameObject target, float duration, float value)
    {
        switch (type)
        {
            case BuffType.Burn:
                return new BurnBuff(target, duration, value);
            case BuffType.Poison:
                return new PoisonBuff(target, duration, value);
            case BuffType.Freeze:
                return new FreezeBuff(target, duration, value);
            case BuffType.SpeedBoost:
                return new SpeedBoostBuff(target, duration, value);
            case BuffType.Heal:
                return new HealBuff(target, duration, value);
            case BuffType.AttackSpeed:
                return new AttackSpeedBuff(target, duration, value);
            case BuffType.GoldDropRate:
                return new GoldDropRateBuff(target, duration, value);
            default:
                return null;
        }
    }

    private void Update()
    {
        List<int> expiredBuffs = new List<int>();

        foreach (var buff in activeBuffs)
        {
            buff.Value.OnUpdate();
            if (buff.Value.IsExpired())
            {
                expiredBuffs.Add(buff.Key);
            }
        }

        foreach (int buffId in expiredBuffs)
        {
            RemoveBuff(buffId);
        }
    }
}

