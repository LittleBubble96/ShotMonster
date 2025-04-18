using UnityEngine;

public class ProjectileHelper
{
    public static Vector3 FixedHitPoint(RaycastHit hit)
    {
        if (hit.collider == null)
        {
            return hit.point;
        }

        if (hit.collider is CapsuleCollider ca)
        {
            Vector3 hitPoint = hit.point;
            hitPoint.y = ca.transform.position.y + ca.center.y;
            return hitPoint;
        }
        return hit.point;
    }
}