using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Run : StateMachineBehaviour
{
    Transform player;

    Rigidbody2D rb;

    public float speed = 2.5f;

    Boss boss;

    public float attackRange = 3f;

    Transform objetive;

    public float jumpForce;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb = animator.GetComponent<Rigidbody2D>();

        boss = animator.GetComponent<Boss>();

        objetive = GameObject.FindGameObjectWithTag("Victory").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        //Vector2 target = new Vector2(player.position.x, player.position.y);

        rb.position = Vector2.MoveTowards(rb.position, player.position, speed * Time.deltaTime);

        //rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange && player.GetComponent<Player_Controller>().vidaActual > 0)
        {
            animator.SetTrigger("Attack");
        }

        else if (Vector2.Distance(player.position, rb.position) > attackRange && player.GetComponent<Player_Controller>().vidaActual > 0)
        {
            animator.SetBool("Run", true);
        }

        else if (player.GetComponent<Player_Controller>().vidaActual <= 0)
        {
            animator.SetBool("Run", true);

            rb.transform.localScale = new Vector3(10, 10, 1);

            //Vector2 newTarget = new Vector2(objetive.position.x, rb.position.y);

            //rb.position = Vector2.MoveTowards(rb.position, objetive.position, speed * Time.deltaTime);

            //rb.MovePosition(actualPos);

            rb.position = Vector2.MoveTowards(rb.transform.position, objetive.transform.position, speed * Time.deltaTime);

            if (Vector2.Distance(rb.transform.position, objetive.transform.position) <= 1f)
            {
                rb.GetComponent<Animator>().SetBool("Run", false);

                rb.transform.localScale = new Vector3(-10, 10, 1);
            }

            //if (rb.position == new Vector2(objetive.position.x, rb.position.y))
            //{
            //    animator.SetBool("Run", false);

            //    rb.transform.localScale = new Vector3(-10, 10, 1);
            //}
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
