using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementV2 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 horizontalMovement;
    [SerializeField] private float maxSpeed = 5f;//, acceleration = 50f, deacceleration = 100f;
    //private float currentSpeed = 0f;
    private Vector2 oldMovement;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool jumped = false, doubleJumped = false;
    private Animator animator;
    [SerializeField] private InputActionReference movement, jump;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
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

        rb.velocity = new Vector2(horizontalMovement.x * maxSpeed, rb.velocity.y); // BASIC MOVEMENT
        animator.SetFloat("speed", Mathf.Abs(horizontalMovement.x));

        if (rb.velocity.x < 0) // FLIPS PLAYER TO LEFT FACING
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rb.velocity.x > 0) // FLIPS PLAYER TO RIGHT FACING
        {
            gameObject.transform.localScale = Vector3.one;
        }

        if (jumped)
        {
            if (IsGrounded()) // JUMP
            {
                // JUMP ANIMATION HERE
                doubleJumped = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (!IsGrounded() && !doubleJumped) // DOUBLE JUMP
            {
                // JUMP ANIMATION HERE
                doubleJumped = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            jumped = false;
        }
    }
    
    public void OnMove(InputAction.CallbackContext context) // CALLED WHENEVER PLAYER MOVES
    {
        horizontalMovement = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context) // CALLED WHENEVER PLAYER JUMPS
    {
        if (context.performed)
        {
            jumped = context.action.triggered;
            Debug.Log("OnJump run");
        }
        
    }

    private bool IsGrounded() // Returns true if the player's groundcheck is touching ground
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
