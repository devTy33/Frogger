using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator ani;
    private SpriteRenderer sprite;

    private float xDir = 0;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpF = 14f;
    [SerializeField]private LayerMask jumpGround;

    private enum MovementPos {idle, running, jumping, falling}
    
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //move left/right
        xDir = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveSpeed*xDir, rb.velocity.y);

        //jump
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpF);
        }

        UpdateAni();
    }

    private void UpdateAni() {
        //running check
        MovementPos pos;

        if (xDir > 0f) {
            pos = MovementPos.running;
            sprite.flipX = false;
        }
        else if (xDir < 0f) {
            pos = MovementPos.running;
            sprite.flipX = true;
        }
        else {
            pos = MovementPos.idle;
        }

        if (rb.velocity.y > .1f) {
            pos = MovementPos.jumping;
        }
        else if(rb.velocity.y < -.1f) {
            pos = MovementPos.falling;
        }

        ani.SetInteger("pos", (int)pos);
    }

    private bool IsGrounded() {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpGround);
    }
}
