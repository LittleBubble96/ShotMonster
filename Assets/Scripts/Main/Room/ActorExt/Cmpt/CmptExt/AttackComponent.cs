using UnityEngine;

public class AttackComponent : ActorComponent
{
    public int ProjectileConfigId { get; set; }
    
    public float AttackSpeed { get; set; }
    //临时记录
    public float CurrentAttackTime { get; set; }
    
    //记录攻击动画时间
    public float AttackAnimationTime { get; set; }
    public float CurrentAttackAnimationTime { get; set; }
    
    //攻击的动画的关键帧
    public float AttackAnimationKeyFrameTime { get; set; }
    public bool WaitAttack { get; set; }

    public float TempAnimSpeed { get; set; }
    
    //当前枪口数量 
    public int MuzzleCount { get; set; }
    
    //单枪口
    public Transform SingleMuzzle { get; set; }
    //双枪口
    public Transform[] DoubleMuzzles { get; set; }
    //三枪口
    public Transform[] TripleMuzzles { get; set; }
    
    //获取当前枪口数量
    public Transform[] GetCurrentMuzzles()
    {
        if (MuzzleCount == 1)
        {
            return new Transform[] { SingleMuzzle };
        }
        else if (MuzzleCount == 2)
        {
            return DoubleMuzzles;
        }
        else if (MuzzleCount == 3)
        {
            return TripleMuzzles;
        }
        
        return null;
    }
    
    //获取完整的攻击时间
    public float GetFullAttackTime()
    {
        float attackSpeedBonus = GetActor().GetFloatAttribute(EPlayerAttribute.AttackBonusSpeedUpAlways);
        float attackSpeed = AttackSpeed * attackSpeedBonus;
        return attackSpeed;
    }
    
    //获取完整的动画时间
    public float GetFullAttackAnimationSpeed()
    {
        float fullAttackTime = GetFullAttackTime();
        if (AttackAnimationTime > fullAttackTime)
        {
            return AttackAnimationTime / fullAttackTime;
        }

        return 1;
    }

}