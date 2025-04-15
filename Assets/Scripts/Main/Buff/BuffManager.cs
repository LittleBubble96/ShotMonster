// buff管理器  需要支持
//1.火焰持续灼烧buff  2.中毒持续掉血 3.冰冻 4.几秒内加快主角攻速 5.主角加血 6.主角攻速增加  7.怪物死亡金币掉落概率增加
//需要有特效支持
using UnityEngine;
using System.Collections.Generic;

public class BuffManager : Singleton<BuffManager>
{

    private Dictionary<int, BaseBuff> activeBuffs = new Dictionary<int, BaseBuff>();
    private int buffIdCounter = 0;
    private int buffMaxId = 10000;

    // Buff类型枚举
    // 添加buff
    public int AddBuff(int buffId, BuffParameter parameter,int releaseActorId, int targetActorId)
    {
        BuffConfigItem buffConfig = BuffConfig.GetConfigItem(buffId);
        if (buffConfig == null)
        {
            Debug.LogError($"BuffConfigItem with ID {buffId} not found.");
            return -1;
        }
        BaseBuff newBuff = CheckOverlayBuff(buffConfig.BuffType,targetActorId);
        if (newBuff == null)
        {
            return CreateNewBuff(buffConfig,parameter,releaseActorId,targetActorId);
        }
        return OverlayBuff(newBuff,parameter);
    }
    
    private int CreateNewBuff(BuffConfigItem buffConfig, BuffParameter parameter, int releaseActorId, int targetActorId)
    {
        GenerateBuffId();
        BaseBuff newBuff = CreateBuff(buffConfig.BuffType);
        if (newBuff != null)
        {
            activeBuffs.Add(buffIdCounter, newBuff);
            newBuff.Init(buffIdCounter,buffConfig, releaseActorId, targetActorId, parameter);
            newBuff.OnStart();
        }

        return buffIdCounter;
    }
    
    private int OverlayBuff(BaseBuff buff ,BuffParameter parameter)
    {
        if (buff == null)
        {
            return -1;
        }
        buff.Overlay(parameter);
        buff.OnStart();
        return buff.BuffId;
    }
    
    private BaseBuff CheckOverlayBuff(EBufferType bufferType, int targetActorId)
    {
        foreach (var buff in activeBuffs)
        {
            if (buff.Value != null && buff.Value.GetBuffType() == bufferType && buff.Value.TargetActorId == targetActorId && 
                buff.Value.CanOverlay)
            {
                return buff.Value;
            }
            {
                return buff.Value;
            }
        }
        return null;
    }
    
    // 移除buff
    public void RemoveBuff(int buffId)
    {
        if (activeBuffs.ContainsKey(buffId))
        {
            BaseBuff buff = activeBuffs[buffId];
            activeBuffs[buffId].OnEnd();
            activeBuffs.Remove(buffId);
            ClientFactory.Instance.GetBaseBuffFactory().PutObject(buff);
        }
    }
    
    private BaseBuff CheckAttachBuff(EBufferType type)
    {
        foreach (var buff in activeBuffs)
        {
            if (buff.Value != null && buff.Value.GetBuffType() == type)
            {
                return buff.Value;
            }
        }
        return null;
    }
    
    private int GenerateBuffId()
    {
        buffIdCounter++;
        if (buffIdCounter >= buffMaxId)
        {
            buffIdCounter = 0;
        }
        while (activeBuffs.ContainsKey(buffIdCounter))
        {
            buffIdCounter++;
            if (buffIdCounter >= buffMaxId)
            {
                buffIdCounter = 0;
            }
        }
        return buffIdCounter;
    }

    private BaseBuff CreateBuff(EBufferType type)
    {
        BaseBuff buff = ClientFactory.Instance.GetBaseBuffFactory().GetObject(type);
        return buff;
    }

    // 需要移除的buff
    private List<int> expiredBuffs = new List<int>();
    public void DoUpdate(float dt)
    {
        foreach (var buff in activeBuffs)
        {
            buff.Value.DoUpdate(dt);
            if (buff.Value.IsEnd)
            {
                expiredBuffs.Add(buff.Key);
            }
        }
        for (int i = expiredBuffs.Count - 1; i >= 0; i--)
        {
            int buffId = expiredBuffs[i];
            RemoveBuff(buffId);
            expiredBuffs.RemoveAt(i);
        }
        
    }
}

