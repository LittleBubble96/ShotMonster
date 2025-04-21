public enum EPlayerAttribute
{
    PlayerE = 1000,
    AttackBonusSpeedUpAlways,//牌子攻速加成永久
}

public enum EMonsterAttribute
{
    MonsterE = 2000,
    BaseSpeed ,
    DamageIncreaseSpeed,
    HurtAnimDuration ,
    DeathAnimDuration,
}

public enum EActorAttribute
{
    HP = 1,

    MaxHP = 2,
    Attack = 3,
}

//子弹属性类型
[System.Flags]
public enum EAttackAttr 
{
    Normal = 0,
    Fire = 1 << 0,
    Ice = 1 << 1,
    //毒
    Poison = 1 << 2,
}