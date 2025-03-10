using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
    public float maxSpeed;
    [SerializeField] float SpeedChangevalue = 5f;
    [SerializeField] float CurSpeed = 0f;
    Vector2 curMovementInput;
    [SerializeField] private float jumpPower;
    [Header("시점")]
    public Transform cameraContainer;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float maxXRot;
    [SerializeField] private float minXRot;
    float curCamXRot;
    [SerializeField] bool canMove = true;
    [SerializeField] bool canJump = true;
    public bool CanMove { get { return canMove; } set { canMove = value; } }
    public bool CanJump { get { return canJump; } set { canJump = value; } }
    [Header("Ground Check")]
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] float CheckDistance = 1f;
    public bool isGround = true;
    public bool isJumping = false;
    Rigidbody rigid;
    Vector2 mouseDelta;
    Animator ani;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        GroundCheck();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CamLook();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled || !CanMove)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && canJump && isGround && !isJumping) 
        {
            StartCoroutine(Jump());
        }
    }

    void GroundCheck()
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, CheckDistance, GroundLayer)) 
        {
            isGround = true;
            CanJump = true;
            ani.SetBool("IsLand", isGround);
        }
        else
        {
            isGround = false;
            CanJump = false;
            ani.SetBool("IsLand", isGround);
        }
        Debug.DrawRay(transform.position, Vector3.down * CheckDistance, Color.red);
    }
    private void Move()
    {
        if (canMove)
        {
            float targetSpeed = curMovementInput.magnitude;
            CurSpeed = Mathf.Lerp(CurSpeed, targetSpeed, Time.deltaTime * SpeedChangevalue);
            Vector3 direction = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
            direction *= moveSpeed;
            direction.y = rigid.velocity.y;

            ani.SetFloat("MovementValue", CurSpeed);
            ani.SetFloat("MoveForward", curMovementInput.x);
            ani.SetFloat("MoveSide", curMovementInput.y);
            rigid.velocity = direction;
        }
    }

    void CamLook()
    {
        curCamXRot += mouseDelta.y * mouseSensitivity;
        curCamXRot = Mathf.Clamp(curCamXRot, minXRot, maxXRot);
        cameraContainer.localEulerAngles = new Vector3(-curCamXRot, cameraContainer.localEulerAngles.y, cameraContainer.localEulerAngles.z);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * mouseSensitivity, 0);
    }

    public void PlusSpeed(float input)
    {
        moveSpeed = Mathf.Clamp(moveSpeed + input, 0, maxSpeed);
    }

    public void ChangeSpeed(float input)
    {
        moveSpeed = Mathf.Clamp(input, 0, maxSpeed);
    }

    IEnumerator Jump()
    {
        isJumping = true;
        ani.SetBool("IsJump", true);
        rigid.AddForce(jumpPower * Vector2.up, ForceMode.Impulse);
        yield return null;
        while(isGround)
        {
            yield return null;
        }
        
        while (!isGround)
        {
            yield return null;
        }
        ani.SetBool("IsJump", false);
        while (!ani.GetCurrentAnimatorStateInfo(0).IsName("Landing"))
        {
            yield return null;
        }
        Debug.Log("움직임 멈추기");
        canMove = false;
        while (ani.GetCurrentAnimatorStateInfo(0).IsName("Landing")) 
        {
            yield return null;
        }
        Debug.Log("움직임 개시");
        canMove = true;
        isJumping = false;
        yield return null;
    }
}
