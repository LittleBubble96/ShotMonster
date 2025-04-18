using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    private int curProjectileId = 0;
    private int maxProjectileId = 10000;
    private Dictionary<int, ProjectileBase> activeProjectiles = new Dictionary<int, ProjectileBase>();
    //待销毁队列
    private Queue<ProjectileBase> destroyQueue = new Queue<ProjectileBase>();
    public void Init()
    {
        // Initialize the projectile manager
    }
    
    public void DoUpdate(float dt)
    {
        // Update all active projectiles
        foreach (var projectile in activeProjectiles.Values)
        {
            if (projectile != null)
            {
                if (projectile.bNeedDestroy)
                {
                    destroyQueue.Enqueue(projectile);
                }
                else
                {
                    projectile.DoUpdate(dt);
                }
            }
        }
        
        // Destroy projectiles that are marked for destruction
        while (destroyQueue.Count > 0)
        {
            ProjectileBase projectile = destroyQueue.Dequeue();
            ReturnProjectile(projectile);
        }
    }
    
    public void CreateProjectile(int projectileCfgId , Vector3 position , Vector3 direction , int actorId)
    {
        // Create a new projectile
        ProjectileConfigItem projectileConfig = ProjectileConfig.GetConfigItem(projectileCfgId);
        if (projectileConfig == null)
        {
            Debug.LogError($"ProjectileConfigItem with ID {projectileCfgId} not found.");
            return;
        }
        string startEffectPath = ProjectileDefine.ProjectileResStartPath + projectileConfig.StartEffect;
        string projectilePath = ProjectileDefine.ProjectileResFlyingPath + projectileConfig.Prefab;
        int projectileId = GenerateProjectileId();
        ProjectileBase projectile = GOtPoolManager.Instance.Get<ProjectileBase>(projectilePath);
        if (projectile != null)
        {
            projectile.transform.position = position;
            projectile.transform.rotation = Quaternion.LookRotation(direction);
            projectile.Init(projectileId,actorId,projectileConfig);
            Debug.Log($"[ProjectileManager] Create projectile: {projectile.GetBulletId()} at {projectile.transform.position}");
            activeProjectiles.Add(projectileId, projectile);
        }
        // Play start effect
        if (!string.IsNullOrEmpty(startEffectPath))
        {
            EffectManager.Instance.PlayEffect(startEffectPath, position, Quaternion.LookRotation(direction));
        }
    }
    
    public void DestroyProjectile(ProjectileBase projectile)
    {
        if (projectile != null)
        {
            projectile.MakeDestroy();
        }
        Debug.Log($"[ProjectileManager] Destroying projectile: {projectile.GetBulletId()} at {projectile.transform.position}");
    }
    
    private void ReturnProjectile(ProjectileBase projectile)
    {
        if (projectile != null)
        {
            activeProjectiles.Remove(projectile.GetBulletId());
            GOtPoolManager.Instance.Return(projectile);
        }
    }
    
    public void OutMaxDistance(ProjectileBase projectile)
    {
        if (projectile != null)
        {
            //TODO 
            Debug.Log($"[ProjectileManager] Out of max distance: {projectile.GetBulletId()} at {projectile.transform.position}");
            DestroyProjectile(projectile);
        }
    }
    
    public void OnUpdateDistance(ProjectileBase projectile , float distance)
    {
        if (projectile != null)
        {
        }
    }

    public void Hit(ProjectileBase projectile, Actor target, RaycastHit hit)
    {
        if (projectile == null)
        {
            return;
        }
        BonusMgr.Instance.OnHitMonster(hit, projectile, target);
    }

    private int GenerateProjectileId()
    {
        curProjectileId++;
        if (curProjectileId >= maxProjectileId)
        {
            curProjectileId = 1;
        }
        while (activeProjectiles.ContainsKey(curProjectileId))
        {
            curProjectileId++;
            if (curProjectileId >= maxProjectileId)
            {
                curProjectileId = 0;
            }
        }
        return curProjectileId;
    }
}