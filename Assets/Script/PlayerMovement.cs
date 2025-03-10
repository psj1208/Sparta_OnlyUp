using System.Collections;
using System.Collections.Generic;
using TreeEditor;
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
    [Header("Ω√¡°")]
    public Transform cameraContainer;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float maxXRot;
    [SerializeField] private float minXRot;
    float curCamXRot;
    bool canMove = true;
    bool canJump = true;
    public bool CanMove { get { return canMove; } set { canMove = value; } }
    public bool CanJump { get { return canJump; } set { canJump = value; } }

    Rigidbody rigid;
    Vector2 mouseDelta;
    Animator ani;
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        //Cursor.lockState = CursorLockMode.Locked;
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
        if (context.phase == InputActionPhase.Performed && canMove)
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
        if (context.phase == InputActionPhase.Started && canJump)
        {
            rigid.AddForce(jumpPower * Vector2.up, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        float targetSpeed = curMovementInput.magnitude;
        CurSpeed = Mathf.Lerp(CurSpeed, targetSpeed, Time.deltaTime * SpeedChangevalue);
        Vector3 direction = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        direction *= moveSpeed;
        direction.y = rigid.velocity.y;

        ani.SetFloat("MovementValue", CurSpeed);
        rigid.velocity = direction;
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
}
