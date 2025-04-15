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

        int damage = releaseActor.GetIntAttribute(EPlayerAttribute.Attack);
        float duration = targetActor.GetFloatAttribute(EMonsterAttribute.HurtAnimDuration);
        BuffManager.Instance.AddBuff(8,new BuffParameter<int,float>(damage,duration),releaseActorId,targetActorId);
    }
}