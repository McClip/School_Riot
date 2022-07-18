using System.Collections;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    //variables:

    [Header("Variables de Control:")]

    //Movimiento:

    public Animator animator;

    private Rigidbody2D Rb;

    public SpriteRenderer spriteRenderer;

    public bool tecladoActivado;

    [Header("Variables de Movimiento:")]

    public bool saltoMejorado;

    public float velocidadDeCaida = 0.5f;

    public float velocidadDeSubida = 1f;

    public float velocidad = 2.0f;

    public float velocidadDeAltura = 2.0f;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();

        StartCoroutine("duracion");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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

            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && (Check_Ground.estaEnElSuelo == true))
            {
                Rb.velocity = new Vector2(Rb.velocity.x, velocidadDeAltura);
            }

            if (Check_Ground.estaEnElSuelo == false)
            {
                animator.SetBool("Jump", true);

                animator.SetBool("Run", false);
            }

            else if (Check_Ground.estaEnElSuelo) animator.SetBool("Jump", false);

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
        }
    }

    IEnumerator duracion() {
        tecladoActivado = false;

        yield return new WaitForSeconds(2f);

        tecladoActivado = true;
    }
}
