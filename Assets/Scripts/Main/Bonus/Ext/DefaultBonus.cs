using UnityEngine;

public class DefaultBonus : BonusBase, IHitBonus
{
    public BonusExecuteResult OnHitMonster(RaycastHit hit,ProjectileBase projectile, Actor target)
    {
        //播放击中特效
        string hitEffectPath = ProjectileDefine.ProjectileResHitPath + projectile.GetHitEffectPath();
        if (!string.IsNullOrEmpty(hitEffectPath))
        {
            EffectManager.Instance.PlayEffect(hitEffectPath, ProjectileHelper.FixedHitPoint(hit), Quaternion.LookRotation(hit.normal));
        }
        //TODO : 是否有 牌子可以 检测是否需要销毁
        ProjectileManager.Instance.DestroyProjectile(projectile);
        if (target == null)
        {
            return BonusExecuteResult.None;
        }
        //怪物掉血
        BuffFunc.AttachMonsterBaseDamage(projectile.GetOwnerActorId(),target.GetActorId());
        return BonusExecuteResult.None;
    }
}