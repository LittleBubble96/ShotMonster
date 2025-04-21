

public class ProjectileComponent : ActorComponent
{
     public EAttackAttr AttackAttr { get; private set; }
     
     public int AttrProjectileConfigId { get; private set; }
     
     protected override void OnInit()
     {
          base.OnInit();
          AttrProjectileConfigId = 1;
          AttackAttr = EAttackAttr.Normal;
     }
     
     public void AddProjectileAttr(EAttackAttr attackAttr)
     {
          AttackAttr |= attackAttr;
          UpdateAttrProjectileCfg();
     }

     private void UpdateAttrProjectileCfg()
     {
          if (AttackAttr == 0)
          {
               AttrProjectileConfigId = 1;
          }

          if (AttackAttr == EAttackAttr.Fire)
          {
               AttrProjectileConfigId = 2; 
          }
     }


}