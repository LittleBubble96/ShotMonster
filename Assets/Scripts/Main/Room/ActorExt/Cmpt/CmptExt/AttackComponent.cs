using UnityEngine;

public class AttackComponent : ActorComponent
{
    public Transform AttackPoint { get; set; }
    public int ProjectileConfigId { get; set; }
    
    public float AttackSpeed { get; set; }
    //临时记录
    public float CurrentAttackTime { get; set; }
    
    //记录攻击动画时间
    public float AttackAnimationTime { get; set; }
    public float CurrentAttackAnimationTime { get; set; }
}