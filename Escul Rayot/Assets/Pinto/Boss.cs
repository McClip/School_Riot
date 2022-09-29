using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;

    public bool isFlipped = true;

    Rigidbody2D rb;

    Check_Ground checkGround;

    Animator animator;

    public float jumpForce = 4f;

    public bool saltoMejorado;

    public float velocidadDeCaida;

    public float velocidadDeSubida;

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;

        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;

            transform.Rotate(0f, 180f, 0f);

            isFlipped = false;
        }

        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;

            transform.Rotate(0f, 180f, 0f);

            isFlipped = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        checkGround = player.transform.GetChild(0).gameObject.GetComponent<Check_Ground>();

        animator = rb.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && (checkGround.GetComponent<Check_Ground>().bustjump == true) && (checkGround.GetComponent<Check_Ground>().estaEnElSuelo == true) /*&& player.GetComponent<Player_Combat>().knockBack == false*/ && player.GetComponent<Player_Controller>().vidaActual > 0)
        {
            StartCoroutine("delaySalto");
        }

        //if (enemy.transform.GetChild(1).gameObject.GetComponent<Identify>().tangible == false && checkGround.GetComponent<Check_Ground>().bustjump == true)
        //{
        //    animator.SetBool("Jump", true);

        //    animator.SetBool("Run", false);
        //}

        //else if (enemy.GetComponent<Enemy_scale>().speed == 0)
        //{
        //    animator.SetBool("Jump", false);

        //    animator.SetBool("Run", false);
        //}

        //else if (enemy.transform.GetChild(2).gameObject.GetComponent<Empuje>().empuje == true)
        //{
        //    animator.SetBool("Jump", false);

        //    animator.SetBool("Run", false);
        //}

        //else
        //{
        //    animator.SetBool("Jump", false);

        //    animator.SetBool("Run", true);
        //}

        if (saltoMejorado == true)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * velocidadDeCaida * Time.deltaTime;
            }
            else if ((rb.velocity.y > 0))
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * velocidadDeSubida * Time.deltaTime;
            }
        }
    }

    IEnumerator delaySalto()
    {
        yield return new WaitForSeconds(0.4f);

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        //animator.SetBool("Jump", true);
    }
}
