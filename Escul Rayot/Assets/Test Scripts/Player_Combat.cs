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

    public bool knockBack;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Player_Controller>().StartCoroutine("duracion");
    }

    private void Update()
    {
        if (GetComponent<Player_Controller>().tecladoActivado == true)
        {
            if (Time.time >= coolDown)
            {
                if (Input.GetKeyDown("h"))
                {
                    Atack();

                    coolDown = Time.time + (1f / tiempoDeAtaque);
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GetComponent<Player_Controller>().tecladoActivado == true)
        {
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

            enemy.GetComponent<Enemy_Combat>().Daño(puntosDeDaño);

            Debug.Log("Le quedan " + enemy.GetComponent<Enemy_Combat>().currentLife + " puntos de vida");

            if (enemigo.GetComponent<Enemy_Combat>().currentLife > 0 /*&& enemigo.transform.GetChild(1).gameObject.GetComponent<Identify>().tangible == true*/)
            {
                StartCoroutine(Knockback(enemy, 1.35f));
            }

            if (/*enemy.GetComponent<Enemy_Combat>().currentLife <= 0*/ enemigo.transform.GetChild(1).gameObject.GetComponent<Identify>().tangible == false)
            {
                Debug.Log(enemy.name + " ha fallecido :(");

                knockBack = true;
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

    IEnumerator Knockback(Collider2D enemy, float time)
    {
        enemy.gameObject.GetComponent<Enemy_scale>().enabled = false;

        enemy.gameObject.GetComponent<Enemy_Combat>().enabled = false;

        //enemigo.GetComponent<Animator>().SetTrigger("Hurt");

        yield return new WaitForSeconds(time);

        enemy.gameObject.GetComponent<Enemy_scale>().enabled = true;

        enemy.gameObject.GetComponent<Enemy_Combat>().enabled = true;

        knockBack = false;
    }
}
