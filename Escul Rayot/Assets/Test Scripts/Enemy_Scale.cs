using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Scale : MonoBehaviour
{
    [Header("Variables de Control:")]

    public bool tiezo;

    public GameObject Jugador;

    public Animator animator;

    //public GameObject enemigo;

    private Rigidbody2D rb2d;

    public GameObject checkGround; 

    [Header("Variables de Movimiento:")]

    public float speed = 5.0f;

    public float salto = 13f;

    public bool saltoMejorado;

    public float velocidadDeCaida;

    public float velocidadDeSubida;

    //public GameObject empujar;

    private void Start() {

        rb2d = GetComponent<Rigidbody2D>();
        
        StartCoroutine("activacion");

    }

    private void FixedUpdate() {
        
        Vector3 direccion = Jugador.transform.position - transform.position;
        
        if (direccion.x >= 0.0f) {

            transform.localScale = new Vector3(10.0f, 10.0f, 1.0f); // Salvando el script

        }

        else {

            transform.localScale = new Vector3(-10.0f, 10.0f, 1.0f);

        }

        if (!tiezo) {

            transform.position = Vector2.MoveTowards(transform.position, Jugador.transform.position, speed * Time.deltaTime);

            animator.SetBool("Run", true);

            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && (checkGround.GetComponent<Check_Ground>().bustjump == true) && (checkGround.GetComponent<Check_Ground>().estaEnElSuelo==true))
            {
                StartCoroutine("delaySalto");
            }

            else if (checkGround.GetComponent<Check_Ground>().estaEnElSuelo == false && checkGround.GetComponent<Check_Ground>().bustjump == true)
            {
                StartCoroutine(delayAnimacion(0.4f, true, false));
            }

            else if (checkGround.GetComponent<Check_Ground>().estaEnElSuelo == true && checkGround.GetComponent<Check_Ground>().bustjump == true)
            {
                StartCoroutine(delayCaida(0.4f, false /*, true*/));
            }

            else if ((checkGround.GetComponent<Check_Ground>().estaEnElSuelo == true) && checkGround.GetComponent<Check_Ground>().bustjump == false)
            {
                animator.SetBool("Jump", false);
                animator.SetBool("Run", false);
                //StartCoroutine(delayAnimacion(0f, false, false));
            }

            else if (checkGround.GetComponent<Check_Ground>().bustjump == false && checkGround.GetComponent<Check_Ground>().estaEnElSuelo == false)
            {
                animator.SetBool("Jump", false);
                //StartCoroutine(delayAnimacion(0f, false, true));
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

        }
    }

    IEnumerator activacion() {

        tiezo = true;

        yield return new WaitForSeconds(2f);

        tiezo = false;

        //animator.SetBool("Run", true);
    }

    IEnumerator delaySalto()
    {
        yield return new WaitForSeconds(0.4f);

        rb2d.velocity = new Vector2(rb2d.velocity.x, salto);
    }

    public void Salto()
    {
        //yield return new WaitForSeconds(0.4f);

        rb2d.velocity = new Vector2(rb2d.velocity.x, Jugador.GetComponent<Player_Controller>().velocidadDeAltura);
    }

    IEnumerator delayCaida(float time, bool estate1/*, bool estate2*/)
    {
        yield return new WaitForSeconds(time); //0.4f

        animator.SetBool("Jump", estate1); //false

        //animator.SetBool("Run", estate2); //true
    }

    IEnumerator delayAnimacion(float time, bool estate1, bool estate2)
    {
        yield return new WaitForSeconds(time); //0.4f

        animator.SetBool("Jump", estate1); //true

        animator.SetBool("Run", estate2); //false
    }
}
