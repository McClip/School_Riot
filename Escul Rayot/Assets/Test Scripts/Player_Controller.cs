using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    //variables:

    [Header("Variables de Control:")]

    //Movimiento:

    public Animator animator;

    private Rigidbody2D Rb;

    public SpriteRenderer spriteRenderer;

    public bool tecladoActivado;

    public GameObject player;

    public GameObject enemigo;

    public GameObject checkGround;

    [Header("Variables de Movimiento:")]

    public bool saltoMejorado;

    public float velocidadDeCaida = 0.5f;

    public float velocidadDeSubida = 1f;

    public float velocidad = 2.0f;

    public float velocidadDeAltura = 13.0f;

    [Header("Variables de Vida:")]

    public float maxVida = 100f;

    public float vidaActual;

    public GameObject barraVida;

    public GameObject campo;

    public Text texto;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();

        StartCoroutine("duracion");

        vidaActual = maxVida;

        //colliderTody.GetComponent<Collider2D>().enabled = false;

        barraVida.transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        texto.text = vidaActual.ToString();

        if (campo.GetComponent<Limite>().limiteJugador == true)
        {
            vidaActual = 0;

            StartCoroutine("Muerto");

            barraVida.transform.GetChild(0).gameObject.SetActive(false);
            barraVida.transform.GetChild(1).gameObject.SetActive(false);
            barraVida.transform.GetChild(2).gameObject.SetActive(false);
            barraVida.transform.GetChild(3).gameObject.SetActive(false);
            barraVida.transform.GetChild(4).gameObject.SetActive(false);
            barraVida.transform.GetChild(5).gameObject.SetActive(true);

            Debug.Log("El " + player.name + " ha muerto por caida.");
        }

        if (tecladoActivado == true)
        {
            //Correr o Caminar:

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                Rb.velocity = new Vector2(velocidad, Rb.velocity.y);
                spriteRenderer.flipX = false;
                animator.SetBool("Run", true);
            }

            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                Rb.velocity = new Vector2(-velocidad, Rb.velocity.y);
                spriteRenderer.flipX = true;
                animator.SetBool("Run", true);
            }

            else
            {
                Rb.velocity = new Vector2(0, Rb.velocity.y);
                animator.SetBool("Run", false);
            }

            //Salto

            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && (checkGround.GetComponent<Check_Ground>().estaEnElSuelo == true))
            {
                Rb.velocity = new Vector2(Rb.velocity.x, velocidadDeAltura);
            }

            if (checkGround.GetComponent<Check_Ground>().estaEnElSuelo == false)
            {
                animator.SetBool("Jump", true);

                animator.SetBool("Run", false);
            }

            else if (checkGround.GetComponent<Check_Ground>().estaEnElSuelo) animator.SetBool("Jump", false);

            if (saltoMejorado == true)
            {
                if (Rb.velocity.y < 0)
                {
                    Rb.velocity += Vector2.up * Physics2D.gravity.y * velocidadDeCaida * Time.deltaTime;
                }
                else if ((Rb.velocity.y > 0) && (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.Space)))
                {
                    Rb.velocity += Vector2.up * Physics2D.gravity.y * velocidadDeSubida * Time.deltaTime;
                }
            }

            if (enemigo.GetComponent<Enemy_Combat>().currentLife == 0)
            {
                enemigo.GetComponent<Enemy_Combat>().text.text = "0";
            }
        }
    }

    IEnumerator duracion()
    {
        tecladoActivado = false;

        yield return new WaitForSeconds(2f);

        tecladoActivado = true;
    }

    //public void DañoJugador(float damage)
    //{
    //    vidaActual -= damage;

    //    //animator.SetTrigger("Hurt");

    //    //if (vidaActual == 100)
    //    //{
    //    //    barraVida.transform.GetChild(0).gameObject.SetActive(true);
    //    //    barraVida.transform.GetChild(1).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(2).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(3).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(4).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(5).gameObject.SetActive(false);
    //    //}

    //    //else if (vidaActual == 80)
    //    //{
    //    //    barraVida.transform.GetChild(0).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(1).gameObject.SetActive(true);
    //    //    barraVida.transform.GetChild(2).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(3).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(4).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(5).gameObject.SetActive(false);
    //    //}

    //    //else if (vidaActual == 60)
    //    //{
    //    //    barraVida.transform.GetChild(0).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(1).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(2).gameObject.SetActive(true);
    //    //    barraVida.transform.GetChild(3).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(4).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(5).gameObject.SetActive(false);
    //    //}

    //    //else if (vidaActual == 40)
    //    //{
    //    //    barraVida.transform.GetChild(0).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(1).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(2).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(3).gameObject.SetActive(true);
    //    //    barraVida.transform.GetChild(4).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(5).gameObject.SetActive(false);
    //    //}

    //    //else if (vidaActual == 20)
    //    //{
    //    //    barraVida.transform.GetChild(0).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(1).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(2).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(3).gameObject.SetActive(false);
    //    //    barraVida.transform.GetChild(4).gameObject.SetActive(true);
    //    //    barraVida.transform.GetChild(5).gameObject.SetActive(false);
    //    //}

    //    if (vidaActual <= 0)
    //    {
    //        StartCoroutine("Muerto");

    //        //barraVida.transform.GetChild(0).gameObject.SetActive(false);
    //        //barraVida.transform.GetChild(1).gameObject.SetActive(false);
    //        //barraVida.transform.GetChild(2).gameObject.SetActive(false);
    //        //barraVida.transform.GetChild(3).gameObject.SetActive(false);
    //        //barraVida.transform.GetChild(4).gameObject.SetActive(false);
    //        //barraVida.transform.GetChild(5).gameObject.SetActive(true);
    //    }
    //}

    IEnumerator Muerto()
    {
        Debug.Log(gameObject.name + " ha fallecido :(");

        ////animator.SetBool("Death", true);

        //gameObject.GetComponent<Player_Controller>().enabled = false;
        ////gameObject.GetComponent<Player_Scale>().enabled = false;

        //gameObject.GetComponent<Collider2D>().enabled = false;
        ////Destroy(enemigo.transform.GetChild(1).gameObject);
        //colliderTody.GetComponent<Collider2D>().enabled = true;

        

        enemigo.GetComponent<Enemy_Scale>().enabled = false;

        enemigo.GetComponent<Animator>().SetBool("Jump", false);
        enemigo.GetComponent<Animator>().SetBool("Run", false);

        yield return new WaitForSeconds(3.8f);

        Destroy(gameObject);
        //Destroy(colliderTody.gameObject);
    }
}
