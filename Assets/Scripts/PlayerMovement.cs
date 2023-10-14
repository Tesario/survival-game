using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float runningSpeed = 11f;
    [SerializeField] private float walkingSpeed = 8f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private VitalFunctionsManager vitalFunctionsManager;

    private bool isMoving = false;
    private bool isGrounded;
    private bool isJumping = false;
    private Vector3 velocity;
    private float jumpVelocityY;
    private float currentSpeed;

    private void Start()
    {
        gameInput.OnJumpAction += GameInput_OnJumpAction;
        gameInput.OnRunningAction += GameInput_OnRunningAction;
        gameInput.OnStopRunningAction += GameInput_OnStopRunningAction;

        jumpVelocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        currentSpeed = walkingSpeed;
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e)
    {
        isJumping = true;
    }

    private void GameInput_OnRunningAction(object sender, System.EventArgs e)
    {
        if (vitalFunctionsManager.HasEnergy())
        {
            currentSpeed = runningSpeed;
        }
    }

    private void GameInput_OnStopRunningAction(object sender, System.EventArgs e)
    {
        currentSpeed = walkingSpeed;
    }

    private void Update()
    {
        HandleMovement();
    }

    public void HandleMovement()
    {
        if (vitalFunctionsManager.HasEnergy() && runningSpeed == currentSpeed && isMoving)
        {
            vitalFunctionsManager.UpdateEnergy(-5 * Time.deltaTime);
        }
        else
        {
            currentSpeed = walkingSpeed;
            vitalFunctionsManager.UpdateEnergy(2 * Time.deltaTime);
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector2 inputMoveVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveVector = transform.right * inputMoveVector.x + transform.forward * inputMoveVector.y;

        controller.Move(moveVector * currentSpeed * Time.deltaTime);
        isMoving = moveVector != Vector3.zero;

        if (isJumping && isGrounded)
        {
            velocity.y = jumpVelocityY;
            isJumping = false;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isMoving && currentSpeed == walkingSpeed;
    }

    public bool IsRunning()
    {
        return isMoving && currentSpeed == runningSpeed;
    }
}
