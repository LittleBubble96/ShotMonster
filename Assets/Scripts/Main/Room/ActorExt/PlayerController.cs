using UnityEngine;

public class PlayerController : Actor
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    
    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector3 targetDirection;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    private void Update()
    {
        HandleTouchInput();
        MoveCharacter();
        RotateCharacter();
        UpdateAnimation();
    }
    
    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            
            // 获取触摸位置相对于屏幕中心的方向
            Vector2 touchPosition = touch.position;
            Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            Vector2 touchDirection = (touchPosition - screenCenter).normalized;
            
            // 直接使用触摸方向作为移动方向
            moveDirection = new Vector3(touchDirection.x, 0, touchDirection.y);
            targetDirection = moveDirection;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
    }
    
    private void MoveCharacter()
    {
        if (moveDirection != Vector3.zero)
        {
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
    
    private void RotateCharacter()
    {
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void UpdateAnimation()
    {
        if (GetAnimator() != null)
        {
            // 计算移动速度（0-1之间）
            float moveMagnitude = moveDirection.magnitude;
            GetAnimator().SetFloat("Speed", moveMagnitude);
        }
    }
}
