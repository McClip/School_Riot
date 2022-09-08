using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    [Header("Variables de Control:")]

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    Rigidbody2D rb;

    public Transform playerPos;

    [Header("Variables del Ataque:")]

    public float attackRange = 7f;

    public Transform puntoDeAtaque;

    public LayerMask LayerDeEnemigo;

    public float puntosDeDaño = 20f;

    public float tiempoDeAtaque = 2f;

    public float coolDown = 0f;

    public GameObject enemigo;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GetComponent<Enemy_scale>().StartCoroutine("activacion");
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Enemy_scale>().tecladoActivado == false)
        {
            if (Time.time >= coolDown)
            {
                if (Vector2.Distance(playerPos.position, rb.position) <= 3)
                {
                    Atack();

                    coolDown = Time.time + (1f / tiempoDeAtaque);
                }
            }

            if (spriteRenderer.flipX == true)
            {
                puntoDeAtaque.transform.position = new Vector2(rb.transform.position.x + 1.725f, puntoDeAtaque.transform.position.y);
            }

            else
            {
                puntoDeAtaque.transform.position = new Vector2(rb.transform.position.x - 1.725f, puntoDeAtaque.transform.position.y);
            }
        }
    }

    public void Atack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(puntoDeAtaque.position, attackRange, LayerDeEnemigo);

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

            Gizmos.DrawWireSphere(puntoDeAtaque.position, attackRange);
        }
    }
}
