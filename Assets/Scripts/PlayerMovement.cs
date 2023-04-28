using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform gunTransform;
    [SerializeField] Transform shotPointTransform;
    [SerializeField] GroundCheck groundCheck;
    [SerializeField] float moveSpeed;
    [Tooltip("To calculate jump height use following equation: Jump Height = Velocity^2 / (2*Gravity)")]
    [SerializeField] float jumpVelocity;
    [SerializeField] float jumpDownGravity;
    [SerializeField] float jumpUpGravity;
    [SerializeField] private float movingJumpForce;

    private float horizontal;
    private bool jump;
    private bool inAir;
    private bool descending;

    private Animator anim;
    private Rigidbody2D rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovementInput();
        JumpInput();
        RotateWithMouse();
    }

    private void FixedUpdate()
    {
        HorizontalMovement();
        Jump();
    }

    void HorizontalMovementInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }
    }

    void HorizontalMovement()
    {
        rb.velocity = new Vector2((horizontal * moveSpeed) * Time.deltaTime, rb.velocity.y);
    }

    void JumpInput()
    {
        if (groundCheck.IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
            {
                jump = true;
                anim.SetBool("IsJumping", true);
            }
            else if (descending)
            {
                descending = false;
                rb.gravityScale = 1;
                anim.SetBool("IsJumping", false);
                anim.SetBool("InAir", false);
            }
        }
        else if (!groundCheck.IsGrounded && !descending)
        {
            inAir = true;
            anim.SetBool("InAir", true);
        }
    }

    void Jump()
    {
        if (jump)
        {
            inAir = true;
            jump = false;

            AudioManager.Instance.Play("PlayerJump");

            if (horizontal != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
                rb.AddForce(new Vector2(movingJumpForce * horizontal, 0f));
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            }
        }

        if (inAir)
        {
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = jumpDownGravity;
                inAir = false;
                descending = true;
            }
            else if (rb.velocity.y > 0)
            {
                rb.gravityScale = jumpUpGravity;
            }
        }
    }

    void RotateWithMouse()
    {
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

            gunTransform.GetComponent<SpriteRenderer>().flipX = true;

            gunTransform.GetComponent<Weapon>().offset = 180;

            if (shotPointTransform.localPosition.x > 0)
            {
                shotPointTransform.localPosition = new Vector2(shotPointTransform.localPosition.x * -1, shotPointTransform.localPosition.y);
            }
        }
        else if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            gunTransform.GetComponent<SpriteRenderer>().flipX = false;

            gunTransform.GetComponent<Weapon>().offset = 0;

            if (shotPointTransform.localPosition.x < 0)
            {
                shotPointTransform.localPosition = new Vector2(shotPointTransform.localPosition.x * -1, shotPointTransform.localPosition.y);
            }
        }
    }
}
