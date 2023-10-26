using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementV2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 horizontalMovement;
    [SerializeField] private float maxSpeed = 5f;//, acceleration = 50f, deacceleration = 100f;
    private float currentSpeed;
    [Tooltip("How much the player's speed is divided by while attacking")][SerializeField] private float attackSpeedDivider;
    //private float currentSpeed = 0f;
    //private Vector2 oldMovement;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioSource JumpSoundEffect;
    [SerializeField] private AudioSource DoubleJumpSoundEffect;
    private bool jumped = false, doubleJumped = false;
    private Animator animator;
    [SerializeField] private InputActionReference movement, jump;
    public bool knockbackActive;
    public bool attacking;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        currentSpeed = maxSpeed;
    }

    private void FixedUpdate()
    {
        /*if (horizontalMovement.magnitude > 0 && currentSpeed >= 0) // MOVEMENT SHIT - FUCKS WITH THE JUMP THOUGH
        {
            oldMovement = horizontalMovement;
            currentSpeed += acceleration * maxSpeed * Time.deltaTime;
        }
        else
        {
            currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
        }
        currentSpeed = Mathf.Clamp(currentSpeed, 0f, maxSpeed);
        rb.velocity = oldMovement * currentSpeed;*/

        if (!knockbackActive)
        {
            rb.velocity = new Vector2(horizontalMovement.x * currentSpeed, rb.velocity.y); // BASIC MOVEMENT
            animator.SetFloat("speed", Mathf.Abs(horizontalMovement.x));
        }

        /*if (rb.velocity.x < 0) // FLIPS PLAYER TO LEFT FACING - THIS SHOULD BE BASED ON PLAYER INPUT NOT VELOCITY DUE TO KNOCKBACK. MOVE THIS TO ONMOVE()
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rb.velocity.x > 0) // FLIPS PLAYER TO RIGHT FACING
        {
            gameObject.transform.localScale = Vector3.one;
        }*/

        if (jumped)
        {
            if (IsGrounded()) // JUMP
            {
                animator.SetTrigger("jump");
                doubleJumped = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                JumpSoundEffect.Play();
            }
            else if (!IsGrounded() && !doubleJumped) // DOUBLE JUMP
            {
                animator.SetTrigger("jump");
                doubleJumped = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                DoubleJumpSoundEffect.Play();
            }
            jumped = false;
        }
    }
    
    public void OnMove(InputAction.CallbackContext context) // CALLED WHENEVER PLAYER MOVES
    {
        horizontalMovement = context.ReadValue<Vector2>();
        if (horizontalMovement.x > 0)
        {
            gameObject.transform.localScale = Vector3.one;
        }
        else if (horizontalMovement.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void OnJump(InputAction.CallbackContext context) // CALLED WHENEVER PLAYER JUMPS
    {
        if (context.performed)
        {
            jumped = context.action.triggered;
        }
    }

    private bool IsGrounded() // Returns true if the player's groundcheck is touching ground
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    public void SetAttackingSpeed(bool setAttackingSpeed)
    {
        if (setAttackingSpeed)
        {
            currentSpeed = maxSpeed / attackSpeedDivider;
            attacking = true;
        }
        else
        {
            currentSpeed = maxSpeed;
            attacking = false;
        }
    }
}
