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

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Player_Controller>().tecladoActivado == true)
        {
            if (knockBack == false)
            {
                if (Time.time >= coolDown)
                {
                    if (Input.GetKeyDown(KeyCode.H))
                    {
                        Attack();

                        coolDown = Time.time + (1f / tiempoDeAtaque);
                    }
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

    public void Attack()
    {
        animator.SetTrigger("Punch");

        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(puntoDeAtaque.position, rangoDeGolpe, LayerDeEnemigo);

        foreach (Collider2D enemy in hitenemies)
        {
            Debug.Log("El enemigo " + enemy.name + " ha sido golpeado");

            enemy.GetComponent<Enemy_Combat>().Daño(puntosDeDaño);

            enemy.GetComponent<Animator>().SetTrigger("Hurt");

            Debug.Log("Le quedan " + enemy.GetComponent<Enemy_Combat>().currentLife + " puntos de vida");

            if (enemy.GetComponent<Enemy_Combat>().currentLife > 0 && enemy.transform.GetChild(1).gameObject.GetComponent<Identify>().tangible == true)
            {
                StartCoroutine(Knockback(enemy, 1.3f));
            }

            else if (enemy.GetComponent<Enemy_Combat>().currentLife > 0 && enemy.transform.GetChild(1).gameObject.GetComponent<Identify>().tangible == false)
            {
                StartCoroutine(Knockback(enemy, 0.75f));
            }

            else if (enemy.GetComponent<Enemy_Combat>().currentLife <= 0)
            {
                Debug.Log(enemy.name + " ha fallecido :(");

                knockBack = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Input.GetKey(KeyCode.H) == true && knockBack == false)
        {
            if (puntoDeAtaque == null) return;

            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(puntoDeAtaque.position, rangoDeGolpe);
        }
    }

    IEnumerator Knockback(Collider2D enemy, float time)
    {
        enemy.gameObject.GetComponent<Enemy_scale>().tecladoActivado = true;

        //enemy.gameObject.GetComponent<Enemy_Combat>().enabled = false;

        //enemy.gameObject.GetComponent<Enemy_scale>().enabled = false;

        enemy.gameObject.GetComponent<Animator>().SetBool("Run", false);

        enemy.gameObject.GetComponent<Animator>().SetBool("Jump", false);

        //puntosDeDaño = 0f;

        yield return new WaitForSeconds(time);

        enemy.gameObject.GetComponent<Enemy_scale>().tecladoActivado = false;

        //enemy.gameObject.GetComponent<Enemy_Combat>().enabled = true;

        //enemy.gameObject.GetComponent<Enemy_scale>().enabled = true;

        enemy.gameObject.GetComponent<Animator>().SetBool("Run", true);

        enemy.gameObject.GetComponent<Animator>().SetBool("Jump", false);

        //puntosDeDaño = 5f;

        knockBack = false;
    }
}
