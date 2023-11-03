using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1Move : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 horizontalMovement;
    [SerializeField] private float maxSpeed = 5f;//, acceleration = 50f, deacceleration = 100f;
    private float currentSpeed;
    [Tooltip("How much the player's speed is divided by while attacking")] [SerializeField] private float attackSpeedDivider;
    //private float currentSpeed = 0f;
    //private Vector2 oldMovement;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private AudioSource JumpSoundEffect;
    [SerializeField] private AudioSource DoubleJumpSoundEffect;
    private bool jumped = false, doubleJumped = false;
    private Animator animator;
    public bool knockbackActive;
    public bool attacking;
    private Player1Combat playerCombat;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        currentSpeed = maxSpeed;
        playerCombat = GetComponent<Player1Combat>();
    }

    private void FixedUpdate()
    {


        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input = input.normalized;


        if (Input.GetKeyDown("space"))
        {
            jumped = true;
        }



        if (input.x > 0)
        {
            gameObject.transform.localScale = Vector3.one;
            playerCombat.isFacingRight = true;
        }
        else if (input.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            playerCombat.isFacingRight = false;
        }


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
            rb.velocity = new Vector2(input.x * currentSpeed, rb.velocity.y); // BASIC MOVEMENT
            animator.SetFloat("speed", Mathf.Abs(input.x));
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
                jumped = false;
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

        animator.SetFloat("yvelocity", rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context) // CALLED WHENEVER PLAYER MOVES
    {
        horizontalMovement = context.ReadValue<Vector2>();
        if (horizontalMovement.x > 0)
        {
            gameObject.transform.localScale = Vector3.one;
            playerCombat.isFacingRight = true;
        }
        else if (horizontalMovement.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            playerCombat.isFacingRight = false;
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
