using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker2 : StateMachineBehaviour
{
    private Transform player;
    private Rigidbody2D rb;
    public float speed = 5;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(rb.position, player.position) > 10)
            animator.SetBool("headRotate", true);
        else
            animator.SetBool("headRotate", false);
        Move();
    }

    private void Move()
    {
        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }
}
