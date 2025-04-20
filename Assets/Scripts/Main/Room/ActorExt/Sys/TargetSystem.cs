using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : SystemBase
{
    public override void OnExecute(float dt)
    {
        TargetComponent targetComponent = Owner.GetActorComponent<TargetComponent>();
        if (targetComponent == null)
        {
            return;
        }

        targetComponent.TargetTime += dt;
        if (targetComponent.TargetTime < targetComponent.TargetDuration)
        {
            return;
        }
        targetComponent.TargetTime = 0;
        
        List<Actor> actorList = RoomManager.Instance.GetActorList((actor) => 
            actor.GetActorRoleType() == EActorRoleType.Monster && 
            actor.GetActorState() == EActorState.Play);
        int[] sortedByDistance = new int[actorList.Count];
        //先根据距离进行排序 距离近的在前面 
        for (int i = 0; i < actorList.Count; i++)
        {
            sortedByDistance[i] = i;
        }
        for (int i = 0; i < actorList.Count; i++)
        {
            for (int j = i + 1; j < actorList.Count; j++)
            {
                if (Vector3.Distance(Owner.GetPosition(), actorList[sortedByDistance[i]].GetPosition()) >
                    Vector3.Distance(Owner.GetPosition(), actorList[sortedByDistance[j]].GetPosition()))
                {
                    (sortedByDistance[i], sortedByDistance[j]) = (sortedByDistance[j], sortedByDistance[i]);
                }
            }
        }
        //根据距离排序完成 sortedByDistance 再从近到远检查中间是否有障碍物
        for (int i = 0; i < sortedByDistance.Length; i++)
        {
            Actor targetActor = actorList[sortedByDistance[i]];
            //忽略 Player和Projectile ，"Monster"层级
            int layerMask = LayerMask.GetMask("Default");
            
            Ray ray = new Ray(Owner.GetPosition(), targetActor.GetPosition() - Owner.GetPosition());
            float distance = Vector3.Distance(Owner.GetPosition(), targetActor.GetPosition());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance, layerMask))
            {
                //如果射线检测到的物体是目标物体
                if (hit.collider)
                {
                    continue;
                }
            }
            //如果没有障碍物
            targetComponent.SetTargetActorId(targetActor.GetActorId());
            return;
        }
        //如果所有目标物体都有障碍物
        targetComponent.SetTargetActorId(-1);
    }
}