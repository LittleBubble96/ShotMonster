public class BuffFunc
{
    //怪物基础伤害
    public static void AttachMonsterBaseDamage(int releaseActorId,int targetActorId)
    {
        Actor releaseActor = RoomManager.Instance.GetActorById(releaseActorId);
        Actor targetActor = RoomManager.Instance.GetActorById(targetActorId);
        if (releaseActor == null || targetActor == null)
        {
            return;
        }

        int damage = releaseActor.GetIntAttribute(EActorAttribute.Attack);
        float duration = targetActor.GetFloatAttribute(EMonsterAttribute.HurtAnimDuration);
        BuffManager.Instance.AddBuff(8,new CapParameter<int,float>(damage,duration),releaseActorId,targetActorId);
    }
    
    //加速
    public static void SpeedUp(int releaseActorId,int targetActorId,float speedRate)
    {
        BuffManager.Instance.AddBuff(6,new CapParameter<float>(speedRate),releaseActorId,targetActorId);
    }
    
    //加伤害
    public static void AttackUpRate(int releaseActorId,int targetActorId,float attackRate)
    {
        BuffManager.Instance.AddBuff(9,new CapParameter<float>(attackRate),releaseActorId,targetActorId);
    }
}