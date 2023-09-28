using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalMovement;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    public bool playerOne; // this might not be necessary with proper control input
    private bool doubleJumped;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerOne)
        {
            horizontalMovement = Input.GetAxis("Horizontal"); // Axis set to use wasd
        }
        else
        {
            horizontalMovement = Input.GetAxis("Horizontal2"); // Axis set to use arrow keys
        }
        MovePlayer();
        //Debug.Log("Gameobject " + gameObject.name + " is grounded = " + IsGrounded());
    }

    void MovePlayer()
    {
        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);

        if (playerOne)
        {
            if (Input.GetKeyDown(KeyCode.W) && IsGrounded()) // player one jump
            {
                animator.SetTrigger("jumping");
                doubleJumped = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (Input.GetKeyDown(KeyCode.W) && !IsGrounded() && !doubleJumped)
            {
                animator.SetTrigger("jumping");
                doubleJumped = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded()) // player two jump
            {
                animator.SetTrigger("jumping");
                doubleJumped = false;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && !IsGrounded() && !doubleJumped)
            {
                animator.SetTrigger("jumping");
                doubleJumped = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        // Flip player
        if (horizontalMovement > 0) 
        {
            gameObject.transform.localScale = new Vector2(1, 1);
        }
        else if (horizontalMovement < 0)
        {
            gameObject.transform.localScale = new Vector2(-1, 1);
        }

        if (rb.velocity.y < 0)
        {
            animator.SetBool("falling", true);
        }
        else if (rb.velocity.y == 0)
        {
            animator.SetBool("falling", false);
        }

        if (rb.velocity.x != 0)
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
    }

    private bool IsGrounded() // Returns true if the player's groundcheck is touching ground
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}