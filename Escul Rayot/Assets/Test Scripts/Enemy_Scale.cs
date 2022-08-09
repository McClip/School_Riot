using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Scale : MonoBehaviour
{

    private Rigidbody2D rb2d;
    [SerializeField] public GameObject Jugador;
    [SerializeField] public float speed = 5.0f;
    public float salto = 13f;
    private bool tiezo;
    public bool saltoMejorado;

    public float velocidadDeCaida;
    public float velocidadDeSubida;

    private void Start() {

        rb2d = GetComponent<Rigidbody2D>();
        
        StartCoroutine("activacion");

    }

    private void Update() {
        
        Vector3 direccion = Jugador.transform.position - transform.position;
        
        if (direccion.x >= 0.0f) {

            transform.localScale = new Vector3(10.0f, 10.0f, 1.0f); // Salvando el script

        }

        else {

            transform.localScale = new Vector3(-10.0f, 10.0f, 1.0f);

        }

        if (!tiezo) {

            transform.position = Vector2.MoveTowards(transform.position, Jugador.transform.position, speed * Time.deltaTime);

            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && (Check_Ground.estaEnElSuelo == true))
            {
                StartCoroutine("delaySalto");
            }

            //else
            //{
            //    //transform.position = Vector2.MoveTowards(transform.position, Jugador.transform.position, speed * Time.deltaTime);

            //    rb2d.velocity = new Vector2(rb2d.velocity.x, velocidadDeCaida * Physics2D.gravity.y * Time.deltaTime);
            //}

            if (saltoMejorado == true)
            {
                if (rb2d.velocity.y < 0)
                {
                    rb2d.velocity += Vector2.up * Physics2D.gravity.y * velocidadDeCaida * Time.deltaTime;
                }
                else if ((rb2d.velocity.y > 0) /*&& (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.Space))*/)
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
    }

    IEnumerator delaySalto()
    {
        yield return new WaitForSeconds(0f);

        rb2d.velocity = new Vector2(rb2d.velocity.x, salto);
    }

}
