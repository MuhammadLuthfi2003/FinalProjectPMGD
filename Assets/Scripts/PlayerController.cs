using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private SpriteRenderer sprite;
    public bool isJumping = false;
    public bool hasJumped = false;

    public bool isFacingRight = true;

    private float dirX = 0;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState { idle, running, jumping, falling }

   // [SerializeField] private AudioSource jumpSound;
    //[SerializeField] private AudioSource stepSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        if (dirX != 0)
        {
            isFacingRight = dirX > 0;
        }

        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (rb.velocity.x != 0 && IsGrounded())
        {
            //if (!stepSound.isPlaying)
           // {
               // stepSound.Play();
           // }
        }
        else
        {
           // stepSound.Stop();
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                //jumpSound.Play();
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        UpdateAnimation();

    }


    private void UpdateAnimation()
    {

        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
            anim.SetBool("isWalking", true);
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
            anim.SetBool("isWalking", true);
        }
        else
        {
            state = MovementState.idle;
            anim.SetBool("isWalking", false);
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
            if (!isJumping)
            {
                anim.SetTrigger("jump");
            }
            isJumping = true;
        }
        else if (rb.velocity.y < -0.1f)
        {
            state = MovementState.falling;
        }
        else if (IsGrounded() && isJumping)
        {
            isJumping = false;
            anim.SetTrigger("grounded");
        }

        //anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        bool res =  Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
        return res;
    }
}
