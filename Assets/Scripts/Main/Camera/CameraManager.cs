//要求 1. 相机跟随玩家 相机的高度不变 斜着看玩家，不会跟随玩家旋转，可以通过接口调整距离

using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;
    public float distance = 10f;
    public float height = 5f;
    public float rotationX = 0f;
    public float rotationY = 0f;
    
    public float maxCurveDistance = 2f;
    
    public AnimationCurve curve;

    public void Init (Transform player)
    {
        this.player = player;
        // 设置相机初始位置
        transform.position = player.position + new Vector3(0, height, -distance);
        // 设置相机初始旋转
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
    }
    
    void LateUpdate()
    {
        
        if (player == null)
        {
            return;
        }
        Vector3 pos = player.position + new Vector3(0, height, -distance);
        float dis =  Vector3.Distance(transform.position, pos);
        float dist = dis / maxCurveDistance;
        if (dist > 1)
        {
            dist = 1;
        }
        //根据曲线计算相机位置
        float curveValue = curve.Evaluate(dist);
        //lerp 相机位置
        transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, height, -distance), 1);
        //lerp 相机旋转
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotationX, rotationY, 0), Time.deltaTime * 10f);
    }

    public void SetDistance(float distance)
    {
        this.distance = distance;
    }

    public void SetHeight(float height)
    {
        this.height = height;
    }

    public void SetRotationX(float rotationX)
    {
        this.rotationX = rotationX;
    }

    public void SetRotationY(float rotationY)
    {
        this.rotationY = rotationY;
    }
    
    
}


