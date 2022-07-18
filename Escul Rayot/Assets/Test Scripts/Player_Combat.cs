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

    public float puntosDeDa�o = 20f;

    public float tiempoDeAtaque = 2f;

    public float coolDown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Player_Controller>().StartCoroutine("duracion");
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Player_Controller>().tecladoActivado == true)
        {
            if (Time.time >= coolDown)
            {
                if (Input.GetKeyDown(KeyCode.H))
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

            enemy.GetComponent<Enemy_Controller>().Da�o(puntosDeDa�o);

            Debug.Log("Le quedan " + enemy.GetComponent<Enemy_Controller>().vidaActual + " puntos de vida");

            if (enemy.GetComponent<Enemy_Controller>().vidaActual <= 0)
            {
                Debug.Log(enemy.name + " ha fallecido :(");
            }
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
}
