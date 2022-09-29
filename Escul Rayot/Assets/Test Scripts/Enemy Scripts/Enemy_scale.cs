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

    public GameObject tope1;

    public GameObject tope2;

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
            //rb2d.position = Vector2.MoveTowards(rb2d.position, player.transform.position, speed * Time.deltaTime);

            if (gameObject.transform.localScale.x != direccion) 
            {
                direccion = gameObject.transform.localScale.x;

                Debug.Log("Ha cambiado de direccion");

                //animator.SetBool("Jump", false);

                //animator.SetBool("Run", false);

                DetenerMovimiento();
            }

            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && (checkGround.GetComponent<Check_Ground>().bustjump == true) && (checkGround.GetComponent<Check_Ground>().estaEnElSuelo == true) && player.GetComponent<Player_Combat>().knockBack == false /*&& enemy.GetComponent<Enemy_Combat>().knockbackPlayer == false*/ && player.GetComponent<Player_Controller>().vidaActual > 0)
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

            Debug.Log(Vector2.Distance(tope1.transform.position, rb2d.position));

            //Debug.Log(Vector2.Distance(tope2.transform.position, rb2d.position));

            if (Vector2.Distance(tope1.transform.position, enemy.transform.position) <= 13f && Vector2.Distance(player.transform.position, rb2d.position) <= 5/*&& player.GetComponent<Player_Controller>().vidaActual > 0*/)
            {
                rb2d.position = Vector2.MoveTowards(rb2d.position, player.transform.position, 0 * Time.deltaTime);

                Debug.Log("Alto");

                animator.SetBool("Run", false);
            }

            else if (Vector2.Distance(tope2.transform.position, enemy.transform.position) <= 13f && Vector2.Distance(player.transform.position, rb2d.position) <= 5/* && player.GetComponent<Player_Controller>().vidaActual > 0*/)
            {
                rb2d.position = Vector2.MoveTowards(rb2d.position, player.transform.position, 0 * Time.deltaTime);

                Debug.Log("Alto");

                animator.SetBool("Run", false);
            }

            else
            {
                rb2d.position = Vector2.MoveTowards(rb2d.position, player.transform.position, speed * Time.deltaTime);
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
}
