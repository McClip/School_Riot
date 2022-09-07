using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    [Header("Variables de Control:")]

    public Animator animator;

    public SpriteRenderer spriteRenderer;

     public GameObject player;

    [Header("Variables del Ataque:")]

    public float rangoDeGolpe = 0.5f;

    public Transform puntoDeAtaque;

    public LayerMask LayerDeEnemigo;

    public float puntosDeDaño = 20f;

    public float tiempoDeAtaque = 2f;

    public float coolDown = 0f;

    public GameObject enemigo;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Player_Controller>().StartCoroutine("duracion");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<Player_Controller>().tecladoActivado == true)
        {
            if (Time.time >= coolDown)
            {
                if (Input.GetKey("h"))
                {
                    Atack();

                    coolDown = Time.time + (1f / tiempoDeAtaque);
                }
            }

            if (spriteRenderer.flipX == true)
            {
                puntoDeAtaque.transform.position = new Vector2(player.transform.position.x - 1.725f, puntoDeAtaque.transform.position.y);
            }

            else
            {
                puntoDeAtaque.transform.position = new Vector2(player.transform.position.x + 1.725f, puntoDeAtaque.transform.position.y);
            }
        }
    }

    public void Atack()
    {
        animator.SetTrigger("Punch");

        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(puntoDeAtaque.position, rangoDeGolpe, LayerDeEnemigo);

        foreach (Collider2D enemy in hitenemies)
        {
            Debug.Log("El enemigo " + enemy.name + " ha sido golpeado");

            //enemy.GetComponent<Enemy_Controller>().Daño(puntosDeDaño);

            //Debug.Log("Le quedan " + enemy.GetComponent<Enemy_Controller>().vidaActual + " puntos de vida");

            //if (enemigo.GetComponent<Enemy_Controller>().vidaActual >= 20)
            //{
            //    StartCoroutine(Knockback(enemy, 0.35f));
            //}

            //if (enemy.GetComponent<Enemy_Controller>().vidaActual <= 0)
            //{
            //    Debug.Log(enemy.name + " ha fallecido :(");
            //}
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Input.GetKey(KeyCode.H) == true)
        {
            if (puntoDeAtaque == null) return;

            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(puntoDeAtaque.position, rangoDeGolpe);
        }
    }

    IEnumerator Knockback(Collider2D enemy, float time)
    {
        enemy.gameObject.GetComponent<Enemy_Scale>().enabled = false;

        enemy.gameObject.GetComponent<Enemy_Controller>().enabled = false;

        yield return new WaitForSeconds(time);

        enemy.gameObject.GetComponent<Enemy_Scale>().enabled = true;

        enemy.gameObject.GetComponent<Enemy_Controller>().enabled = true;
    }
}
