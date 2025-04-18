using UnityEngine;

public class CrossBulletBonus :BonusBase , IHitBonus
{
    public BonusExecuteResult OnHitMonster(RaycastHit hit,ProjectileBase projectile, Actor target)
    {
        //播放击中特效
        string hitEffectPath = ProjectileDefine.ProjectileResHitPath + projectile.GetHitEffectPath();
        if (!string.IsNullOrEmpty(hitEffectPath))
        {
            EffectManager.Instance.PlayEffect(hitEffectPath, ProjectileHelper.FixedHitPoint(hit), Quaternion.LookRotation(hit.normal));
        }
        if (target == null)
        {
            return BonusExecuteResult.None;
        }
        //怪物掉血
        BuffFunc.AttachMonsterBaseDamage(projectile.GetOwnerActorId(),target.GetActorId());
        return BonusExecuteResult.BreakDefaultBonus;
    }
}