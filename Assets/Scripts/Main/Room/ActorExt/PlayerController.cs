using UnityEngine;

public class PlayerController : Actor
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [Header("Shot")]
    [SerializeField] private Transform shotSpawn;
    [SerializeField] private float shotSpeed = 1f;
    [SerializeField] private float shotAnimDuration = 0.5f;
    [Header("Target")]
    [SerializeField] private float checkTargetDuration = 0.5f;
    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector3 targetDirection;
    
    protected override void OnInit()
    {
        TryOrAddActorComponent<AttackComponent>();
        InitAttackData();
        TryOrAddActorComponent<TargetComponent>();
        InitTargetData();
        characterController = GetComponent<CharacterController>();
        RegisterSystem<AttackSys>();
        RegisterSystem<TargetSystem>();
    }

    public override void DoFixedUpdate(float dt)
    {
        base.DoFixedUpdate(dt);
#if UNITY_EDITOR
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
        HandleLerpStop(dt);
        MoveCharacter(dt);
        RotateCharacter(dt);
        UpdateAnimation();
    }
    
    private Vector2 pressTouchPos;
    private bool isPress = false;
    private float stopTime = 0f;
    private float startTime = 0f;
    private void HandleTouchInput()
    {
        //手指按下 记录按下位置
        //手指滑动 计算滑动方向
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                pressTouchPos = touch.position;
                isPress = true;
            }
            if (touch.phase == TouchPhase.Moved && isPress)
            {
                Vector2 deltaPos = touch.position - pressTouchPos;
                moveDirection = new Vector3(deltaPos.x, 0, deltaPos.y).normalized;
                targetDirection = new Vector3(deltaPos.x, 0, deltaPos.y).normalized;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                moveDirection = Vector3.zero;
                targetDirection = Vector3.zero;
                isPress = false;
                stopTime = 0.5f;
            }
           
        }
    }
    
    private void HandleMouseInput()
    {
        // 鼠标按下 记录按下位置
        // 鼠标滑动 计算滑动方向
        if (Input.GetMouseButton(0))
        {
            if (!isPress)
            {
                pressTouchPos = Input.mousePosition;
            }
            Vector2 deltaPos = (Vector2)Input.mousePosition - pressTouchPos;
            moveDirection = new Vector3(deltaPos.x, 0, deltaPos.y).normalized;
            targetDirection = new Vector3(deltaPos.x, 0, deltaPos.y).normalized;
            isPress = true;
        }
        else
        {
            if (isPress)
            {
                stopTime = 0.5f;
            }
            isPress = false;
        }
    }

    private void HandleLerpStop(float dt)
    {
        if (isPress)
        {
            stopTime = 0f;
        }
        else
        {
            stopTime -= dt;
            float t = 1f - stopTime / 0.5f;
            if (stopTime > 0f)
            {
                moveDirection = Vector3.Lerp(moveDirection, Vector3.zero, t);
                targetDirection = Vector3.Lerp(targetDirection, Vector3.zero, t);
            }
            else
            {
                moveDirection = Vector3.zero;
                targetDirection = Vector3.zero;
            }
        }
    }

    private void MoveCharacter(float dt)
    {
        if (moveDirection != Vector3.zero)
        {
            //重力
            Vector3 gravity = characterController.isGrounded ? Vector3.zero : Vector3.down * 9.81f * dt;
            characterController.Move(moveDirection * moveSpeed * dt + gravity);
        }
    }
    
    private void RotateCharacter(float dt)
    {
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * dt);
        }
    }

    private void UpdateAnimation()
    {
        if (GetAnimator() != null)
        {
            string horizontalParam = "hor";
            string verticalParam = "ver";
            //目标方向为正方向 ，计算移动方向的 hor 和 ver
            float hor = Vector3.Dot(moveDirection, transform.right);
            float ver = Vector3.Dot(moveDirection, transform.forward);
            //设置动画参数
            GetAnimator().SetFloat(horizontalParam, hor);
            GetAnimator().SetFloat(verticalParam, ver);
        }
    }

    #region 初始化攻击数据

    public void InitAttackData()
    {
        //攻击数据
        AttackComponent attackComponent = GetActorComponent<AttackComponent>();
        attackComponent.AttackPoint = shotSpawn;
        attackComponent.AttackAnimationTime = shotAnimDuration;
        attackComponent.AttackSpeed = shotSpeed;
    }

    public void InitTargetData()
    {
        //目标数据
        TargetComponent targetComponent = GetActorComponent<TargetComponent>();
        targetComponent.TargetDuration = checkTargetDuration;
    }

    #endregion
}
