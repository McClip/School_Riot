using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    //variables:

    //Movimiento:

    public Animator An;

    Rigidbody2D Rb;

    public bool Salto_mejorado;

    public float Velocidad_C = 0.5f;

    public float Velocidad_S = 1f;

    public SpriteRenderer Sr;

    public float velocidad = 2;

    public float velocidad_altura = 2;

    //Ataque:

    public GameObject player;

    public float rangogolpe = 0.5f;

    public Transform ataque;

    public LayerMask enemigos;

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
                Combate("Run", false, 0.47f);
            }
        }

        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Rb.velocity = new Vector2(-velocidad, Rb.velocity.y);
            Sr.flipX = true;
            An.SetBool("Run", true);

            if (Input.GetKey(KeyCode.H))
            {
                Combate("Run", false, 0.47f);
            }
        }

        else
        {
            Rb.velocity = new Vector2(0, Rb.velocity.y);
            An.SetBool("Run", false);

            if (Input.GetKey(KeyCode.H))
            {
                Combate("Run", false, 0.47f);
            }
        }

        if (Sr.flipX == true)
        {
            ataque.transform.position = new Vector2(player.transform.position.x - 1.725f, ataque.transform.position.y);
        }

        else
        {
            ataque.transform.position = new Vector2(player.transform.position.x + 1.725f, ataque.transform.position.y);
        }

        //Golpe

        if (Input.GetKey(KeyCode.H))
        {
            StartCoroutine(duracion(0.42f));
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

            if (Input.GetKey(KeyCode.H))
            {
                Combate("Jump", false, 0.47f);
            }
        }

        else if (CheckGround.Suelo)
        {
            An.SetBool("Jump", false);
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

    public void Combate(string parametro, bool estado, float temp)
    {
        //Animacion:

        An.SetBool(parametro, estado);

        StartCoroutine(duracion(temp));

        //Golpe:

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(ataque.position, rangogolpe, enemigos);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Golpeé al " + enemy.name);
        }
    }

    IEnumerator duracion(float tiempo)
    {
        An.SetBool("punch", true);

        yield return new WaitForSeconds(tiempo);

        An.SetBool("punch", false);
    }

    private void OnDrawGizmosSelected()
    {
        if (Input.GetKey(KeyCode.H) == true)
        {
            if (ataque == null)
            {
                return;
            }

            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(ataque.position, rangogolpe);
        }
    }
}
