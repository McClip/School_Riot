using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    //variables:
    public Animator An;
    Rigidbody2D Rb;
    public bool Salto_mejorado;
    public float Velocidad_C=0.5f;
    public float Velocidad_S=1f;
    public SpriteRenderer Sr;
    public float velocidad = 2;
    public float velocidad_altura = 2;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
        {
            Rb.velocity = new Vector2(velocidad,Rb.velocity.y);
            Sr.flipX = false;
            An.SetBool("Run", true);

            if (Input.GetKey(KeyCode.H))
            {
                An.SetBool("Run", false);

                //An.SetBool("punch", true);

                StartCoroutine(duracion(0.47f));
            }
        }

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Rb.velocity = new Vector2(-velocidad, Rb.velocity.y);
            Sr.flipX = true;
            An.SetBool("Run", true);

            if (Input.GetKey(KeyCode.H))
            {
                An.SetBool("Run", false);

                //An.SetBool("punch", true);

                StartCoroutine(duracion(0.47f));
            }
        }

        else
        {
            Rb.velocity = new Vector2(0, Rb.velocity.y);
            An.SetBool("Run", false);

            if (Input.GetKey(KeyCode.H))
            {
                An.SetBool("Run", false);

                //An.SetBool("punch", true);

                StartCoroutine(duracion(0.47f));
            }
        }

        //Golpe

        if (Input.GetKey(KeyCode.H) /*&& ((CheckGround.Suelo == true)) || (CheckGround.Suelo == false)*/)
        {
            StartCoroutine(duracion(0.42f));

            //An.SetBool("Jump", false);
        }

        //Salto

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && (CheckGround.Suelo == true))
        {
            Rb.velocity = new Vector2(Rb.velocity.x, velocidad_altura);
        }

        if (CheckGround.Suelo == false)
        {
            An.SetBool("Jump", true);

            An.SetBool("Run", false);

            //An.SetBool("punch", true);

            if (Input.GetKey(KeyCode.H))
            {
                An.SetBool("Jump", false);

                //An.SetBool("punch", true);

                StartCoroutine(duracion(0.47f));
            }

            //else
            //{
            //    An.SetBool("Jump", true);

            //    An.SetBool("punch", false);
            //}
        }

        else if (CheckGround.Suelo)
        {
            An.SetBool("Jump", false);

            //An.SetBool("punch", true);
        }

        if (Salto_mejorado == true)
        {
            if (Rb.velocity.y < 0)
            {
                Rb.velocity += Vector2.up * Physics2D.gravity.y * Velocidad_C * Time.deltaTime;
            }
            else if ((Rb.velocity.y > 0) && (!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.Space)))
            {
                Rb.velocity += Vector2.up * Physics2D.gravity.y * Velocidad_S * Time.deltaTime;
            }
        }
    }

    IEnumerator duracion(float tiempo)
    {
        An.SetBool("punch", true);

        yield return new WaitForSeconds(tiempo);

        An.SetBool("punch", false);
    }
}
