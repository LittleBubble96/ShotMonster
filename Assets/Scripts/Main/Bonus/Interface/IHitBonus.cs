using System;
using UnityEngine;

[Flags]
public enum BonusExecuteResult
{
    None = 0,
    BreakDefaultBonus = 1<<0,
    BreakAllBonus = 1<<1,
}
public interface IHitBonus
{
    BonusExecuteResult OnHitMonster(RaycastHit hit,ProjectileBase projectile, Actor target);
}