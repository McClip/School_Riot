using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_scale : MonoBehaviour
{
    public GameObject player;

    public bool isFlipped;

    public GameObject checkGround;

    public GameObject enemy;

    private Rigidbody2D rb2d;

    public float salto = 13f;

    public bool saltoMejorado;

    public float velocidadDeCaida;

    public float velocidadDeSubida;

    public bool tecladoActivado;

    public Animator animator;

    public float speed = 2.5f;

    public float direccion;

    //public bool isGrounded;

    public void ChangingScale()
    {
        Vector3 direccion = player.transform.position - transform.position;

        if (direccion.x >= 0.0f)
        {
            transform.localScale = new Vector3(10.0f, 10.0f, 1.0f); // Salvando el script
        }

        else
        {
            transform.localScale = new Vector3(-10.0f, 10.0f, 1.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        StartCoroutine("activacion");

        direccion = gameObject.transform.localScale.x;

        //isGrounded = true;

        //animator.SetBool("Run", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ChangingScale();

        if (!tecladoActivado)
        {
            rb2d.position = Vector2.MoveTowards(rb2d.position, player.transform.position, speed * Time.deltaTime);

            if (gameObject.transform.localScale.x != direccion) 
            {
                direccion = gameObject.transform.localScale.x;

                //Debug.Log("Ha cambiado de direccion");

                //animator.SetBool("Jump", false);

                //animator.SetBool("Run", false);

                //StartCoroutine(delayAnimacion(0f, false, false));

                DetenerMovimiento();
            }

            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && (checkGround.GetComponent<Check_Ground>().bustjump == true) && (checkGround.GetComponent<Check_Ground>().estaEnElSuelo == true) && player.GetComponent<Player_Combat>().knockBack == false)
            {
                StartCoroutine("delaySalto");
            }

            if (enemy.transform.GetChild(1).gameObject.GetComponent<Identify>().tangible == false && checkGround.GetComponent<Check_Ground>().bustjump == true)
            {
                animator.SetBool("Jump", true);

                animator.SetBool("Run", false);
            }

            else if (enemy.GetComponent<Enemy_scale>().speed == 0)
            {
                animator.SetBool("Jump", false);

                animator.SetBool("Run", false);
            }

            else if (enemy.transform.GetChild(2).gameObject.GetComponent<Empuje>().empuje == true)
            {
                animator.SetBool("Jump", false);

                animator.SetBool("Run", false);
            }

            else
            {
                animator.SetBool("Jump", false);

                animator.SetBool("Run", true);
            }

            //else if (checkGround.GetComponent<Check_Ground>().estaEnElSuelo == true && checkGround.GetComponent<Check_Ground>().bustjump == true && player.GetComponent<Player_Combat>().knockBack == false)
            //{
            //    //StartCoroutine(delayAnimacion(0.2f, true, false));
            //    animator.SetBool("Jump", false);
            //    animator.SetBool("Run", true);

            //    isGrounded = true;
            //}

            //else if (checkGround.GetComponent<Check_Ground>().estaEnElSuelo == false && checkGround.GetComponent<Check_Ground>().bustjump == true && player.GetComponent<Player_Combat>().knockBack == false)
            //{
            //    StartCoroutine(delayAnimacion(0.2f, true, false));
            //    //animator.SetBool("Jump", true);
            //    //animator.SetBool("Run", false);

            //    isGrounded = false;
            //}

            //else if (checkGround.GetComponent<Check_Ground>().estaEnElSuelo == true && checkGround.GetComponent<Check_Ground>().bustjump == false && player.GetComponent<Player_Combat>().knockBack == false)
            //{
            //    animator.SetBool("Jump", false);
            //    animator.SetBool("Run", false);
            //    //StartCoroutine(delayAnimacion(0f, false, false));

            //    isGrounded = true;
            //}

            //else if (checkGround.GetComponent<Check_Ground>().bustjump == false && checkGround.GetComponent<Check_Ground>().estaEnElSuelo == false && player.GetComponent<Player_Combat>().knockBack == false)
            //{
            //    animator.SetBool("Jump", false);
            //    animator.SetBool("Run", false);
            //    //StartCoroutine(delayAnimacion(0f, false, true));

            //    isGrounded = true;
            //}

            if (saltoMejorado == true)
            {
                if (rb2d.velocity.y < 0)
                {
                    rb2d.velocity += Vector2.up * Physics2D.gravity.y * velocidadDeCaida * Time.deltaTime;
                }
                else if ((rb2d.velocity.y > 0))
                {
                    rb2d.velocity += Vector2.up * Physics2D.gravity.y * velocidadDeSubida * Time.deltaTime;
                }
            }
        }
    }

    private void DetenerMovimiento() {

        speed = 0;

        Invoke(nameof(DelayMovimiento), 1.2f);
    }

    private void DelayMovimiento() {

        speed = 5;
    }

    IEnumerator delaySalto()
    {
        yield return new WaitForSeconds(0.4f);

        rb2d.velocity = new Vector2(rb2d.velocity.x, salto);

        //animator.SetBool("Jump", true);
    }

    IEnumerator activacion()
    {
        tecladoActivado = true;

        yield return new WaitForSeconds(2f);

        tecladoActivado = false;

        //animator.SetBool("Run", true);
    }

    // IEnumerator delayCaida(float time, bool estate1 /*, bool estate2*/)
    //{
    //    yield return new WaitForSeconds(time); //0.4f

    //    animator.SetBool("Jump", estate1); //false

    //    //animator.SetBool("Run", estate2); //true
    //}

    //IEnumerator delayAnimacion(float time, bool estate1, bool estate2)
    //{
    //    yield return new WaitForSeconds(time); //0.4f

    //    animator.SetBool("Jump", estate1); //true

    //    animator.SetBool("Run", estate2); //false
    //}
}
