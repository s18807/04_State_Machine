using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Text text;
    public float moveSpeed, jumpSpeed;
    private bool grounded = false;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;
    int hp = 3;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            animator.SetTrigger("jump");
        }
        float xDisplacement = Input.GetAxis("Horizontal");
        if (xDisplacement < 0)
            sr.flipX = true;
        if (xDisplacement > 0)
            sr.flipX = false;
        animator.SetFloat("xSpeed", Mathf.Abs(xDisplacement));
        rb.velocity = new Vector2(xDisplacement * moveSpeed, rb.velocity.y);
        text.text = "HP: "+hp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
            animator.SetBool("grounded", true);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hp--;
            if (hp <= 0) Application.LoadLevel(Application.loadedLevel);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            animator.SetBool("grounded", false);
        }
    }
}
