using UnityEngine;

public class ProjectileBase : RecycleObject
{
    [SerializeField] private float maxDistance = 100f;
    
    [SerializeField] private float m_MoveSpeed = 25f;
    private Vector3 m_startPos;
    private Vector3 m_lastPos;

    private ProjectileConfigItem m_configItem;
    
    private int m_bufferId = 0;

    private int OwnerActorId;

    public bool bNeedDestroy { get; private set; }
    
    private TrailRenderer[] m_trailRenderers;
    
    private int ownerLayerMask;

    public void Init(int id,int ownerId, ProjectileConfigItem configItem)
    {
        m_configItem = configItem;
        m_bufferId = id;
        OwnerActorId = ownerId;
        m_startPos = transform.position;
        if (m_trailRenderers == null)
        {
            m_trailRenderers = GetComponentsInChildren<TrailRenderer>();
        }
        if (m_trailRenderers != null)
        {
            foreach (var trailRenderer in m_trailRenderers)
            {
                trailRenderer.Clear();
            }
        }
        //检查自身到 起始位置的射线
        CheckStartRay();
    }
    

    public void DoUpdate(float dt)
    {
        //朝前方移动
        Vector3 forward = transform.forward;
        Vector3 moveDir = forward * m_MoveSpeed * dt;
        m_lastPos = transform.position;
        transform.position += moveDir;
        
        //距离检测
        float distance = Vector3.Distance(transform.position, m_startPos);
        ProjectileManager.Instance.OnUpdateDistance(this, distance);
        if (distance >= maxDistance)
        {
            ProjectileManager.Instance.OutMaxDistance(this);
        }
        //射线检测碰撞物体、
        CheckRay();
    }
    
    public string GetHitEffectPath()
    {
        return m_configItem.HitEffect;
    }
    
    public string GetStartEffectPath()
    {
        return m_configItem.StartEffect;
    }
    
    public int GetBulletId()
    {
        return m_bufferId;
    }
    
    public int GetOwnerActorId()
    {
        return OwnerActorId;
    }
    
    public void MakeDestroy()
    {
        bNeedDestroy = true;
    }
    
    private void CheckRay()
    {
        Vector3 offset = new Vector3(0,-0.5f,0);
        float distance = Vector3.Distance(transform.position, m_lastPos);
        Ray ray = new Ray(m_lastPos + offset, transform.position - m_lastPos);
        RaycastHit hit;
        //忽略 ownerLayerMask
        int layerMask = ~(1 << ownerLayerMask);
        if (Physics.SphereCast(ray, 0.3f,out hit,distance,layerMask))
        {
            if (hit.collider == null)
            {
                return;
            }
            
            //TODO : 发送攻击命中事件
            Actor targetActor = hit.collider.GetComponent<Actor>();
            ProjectileManager.Instance.Hit(this, targetActor,hit);
        }
    }

    private void CheckStartRay()
    {
        Actor ownerActor = RoomManager.Instance.GetActorById(OwnerActorId);
        if (ownerActor == null)
        {
            return;
        }
        ownerLayerMask = ownerActor.gameObject.layer;
        Vector3 offset = new Vector3(0,-0.5f,0);
        Vector3 start = ownerActor.transform.position;
        start.y = m_startPos.y;
        start -= offset;
        float distance = Vector3.Distance(start, m_startPos);
        Ray ray = new Ray(start, m_startPos - start);
        RaycastHit hit;
        //忽略 ownerLayerMask
        int layerMask = ~(1 << ownerLayerMask);
        if (Physics.SphereCast(ray, 0.3f,out hit,distance,layerMask))
        {
            if (hit.collider == null)
            {
                return;
            }
            
            //TODO : 发送攻击命中事件
            Actor targetActor = hit.collider.GetComponent<Actor>();
            ProjectileManager.Instance.Hit(this, targetActor,hit);
        }
    }

    public override void Recycle()
    {
        base.Recycle();
        m_bufferId = 0;
        m_configItem = null;
        m_lastPos = transform.position;
        m_startPos = transform.position;
        bNeedDestroy = false;
        
    }
}