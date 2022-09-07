using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Run : StateMachineBehaviour
{
    Transform playerPos;

    Rigidbody2D rb;

    Enemy_scale enemyScale;

    public float attackRange = 3f;

    public float salto = 13f;

    public float speed = 2.5f;

    SpriteRenderer spriteRenderer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;

        rb = animator.GetComponent<Rigidbody2D>();

        spriteRenderer = animator.GetComponent<SpriteRenderer>();

        enemyScale = animator.GetComponent<Enemy_scale>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //spriteRenderer.flipX = false;

        //Vector2 target = new Vector2(playerPos.position.x, rb.position.y);

        //Vector2 updatedPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        //rb.MovePosition(updatedPos);

        //rb.position = Vector2.MoveTowards(rb.position, playerPos.transform.position, speed * Time.deltaTime);

        //enemyScale.ChangingScale();

        //enemyScale.Salto();

        if (Vector2.Distance(playerPos.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }

        //if (enemyScale.checkGround.GetComponent<Check_Ground>().estaEnElSuelo == false && enemyScale.checkGround.GetComponent<Check_Ground>().bustjump == false)
        //{
        //    animator.SetBool("Run", false);

        //    animator.SetBool("Jump", true);
        //}

        //else
        //{
        //    animator.SetBool("Run", true);

        //    animator.SetBool("Jump", false);
        //}
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");

        //animator.ResetTrigger("Jump");
    }
}
