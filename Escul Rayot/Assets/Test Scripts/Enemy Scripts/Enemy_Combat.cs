using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy_Combat : MonoBehaviour
{
    [Header("Variables de Control:")]

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    public Rigidbody2D rb;

    public Transform playerPos;

    [Header("Variables del Ataque:")]

    public float attackRange = 7f;

    public Transform puntoDeAtaque;

    public LayerMask LayerDeEnemigo;

    public float damagePoints = 20f;

    public float tiempoDeAtaque = 2f;

    public float coolDown = 0f;

    public GameObject enemy;

    public float maxLife = 100f;

    public float currentLife;

    public GameObject enemyCollider;

    public GameObject barLife;

    public GameObject limit;

    public Text text;

    public bool knockbackPlayer;

    public GameObject victoryPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        GetComponent<Enemy_scale>().StartCoroutine("activacion");

        currentLife = maxLife;

        barLife.transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        text.text = currentLife.ToString();

        if (limit.GetComponent<Limite>().limite == true)
        {
            currentLife = 0;

            StartCoroutine("Muerto");

            barLife.transform.GetChild(1).gameObject.SetActive(false);
            barLife.transform.GetChild(2).gameObject.SetActive(false);
            barLife.transform.GetChild(3).gameObject.SetActive(false);
            barLife.transform.GetChild(4).gameObject.SetActive(false);
            barLife.transform.GetChild(5).gameObject.SetActive(false);
            barLife.transform.GetChild(6).gameObject.SetActive(true);

            Debug.Log("El " + enemy.name + " ha muerto por caida.");
        }

        if (GetComponent<Enemy_scale>().tecladoActivado == false)
        {
            if (knockbackPlayer == false)
            {
                if (Time.time >= coolDown)
                {
                    if (Vector2.Distance(playerPos.position, rb.position) <= 3 && enemy.transform.GetChild(2).gameObject.GetComponent<Empuje>().empuje == false && playerPos.GetComponent<Player_Controller>().vidaActual > 0 && (Input.GetKey(KeyCode.H)) == false)
                    {
                        Atack();

                        coolDown = Time.time + (1f / tiempoDeAtaque);
                    }
                }
            }

            if (transform.localScale == new Vector3(10.0f, 10.0f, 1.0f))
            {
                puntoDeAtaque.transform.position = new Vector2(rb.transform.position.x + 1.725f, puntoDeAtaque.transform.position.y);
            }

            else if (transform.localScale == new Vector3(-10.0f, 10.0f, 1.0f))
            {
                puntoDeAtaque.transform.position = new Vector2(rb.transform.position.x - 1.725f, puntoDeAtaque.transform.position.y);
            }
        }

        if (playerPos.GetComponent<Player_Controller>().vidaActual <= 0)
        {
            playerPos.GetComponent<Player_Controller>().texto.text = "0";

            enemy.GetComponent<Animator>().SetBool("Jump", false);

            enemy.GetComponent<Animator>().SetBool("Run", true);

            enemy.GetComponent<Animator>().SetBool("Death", false);

            enemy.transform.localScale = new Vector3(10, 10, 1);

            enemy.GetComponent<Enemy_scale>().enabled = false;

            rb.position = Vector2.MoveTowards(rb.position, victoryPos.transform.position, 5 * Time.deltaTime);

            //rb.constraints = RigidbodyConstraints2D.FreezePositionY;

            if (Vector2.Distance(rb.position, victoryPos.transform.position) <= 1f)
            {
                Invoke(nameof(Giro), 1f);

                rb.position = new Vector2(rb.position.x, -2.692222f);
            }
        }
    }

    public void Atack()
    {
        animator.SetTrigger("Attack");

        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(puntoDeAtaque.position, attackRange, LayerDeEnemigo);

        foreach (Collider2D player in hitenemies)
        {
            Debug.Log("El enemigo " + player.name + " ha sido golpeado");

            player.GetComponent<Player_Controller>().DañoJugador(damagePoints);

            player.GetComponent<Animator>().SetTrigger("Hurt");

            Debug.Log("Le quedan " + player.GetComponent<Player_Controller>().vidaActual + " puntos de vida");

            if (player.GetComponent<Player_Controller>().vidaActual > 0 && player.transform.GetChild(1).gameObject.GetComponent<Identify_Player>().tangible == true)
            {
                StartCoroutine(KnockbackPlayer(player, 0.5f));
            }

            else if (player.GetComponent<Player_Controller>().vidaActual > 0 && player.transform.GetChild(1).gameObject.GetComponent<Identify_Player>().tangible == false)
            {
                //knockbackPlayer = false;

                StartCoroutine(KnockbackPlayer(player, 0.5f));
            }

            else if (player.GetComponent<Player_Controller>().vidaActual <= 0)
            {
                Debug.Log(enemy.name + " ha fallecido :(");

                knockbackPlayer = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Vector2.Distance(playerPos.position, rb.position) <= 3)
        {
            if (puntoDeAtaque == null) return;

            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(puntoDeAtaque.position, attackRange);
        }
    }

    public void Daño(float damage)
    {
        currentLife -= damage;

        animator.SetTrigger("Hurt");

        playerPos.GetComponent<Player_Combat>().knockBack = true;

        if (currentLife == 100)
        {
            barLife.transform.GetChild(1).gameObject.SetActive(true);
            barLife.transform.GetChild(2).gameObject.SetActive(false);
            barLife.transform.GetChild(3).gameObject.SetActive(false);
            barLife.transform.GetChild(4).gameObject.SetActive(false);
            barLife.transform.GetChild(5).gameObject.SetActive(false);
            barLife.transform.GetChild(6).gameObject.SetActive(false);
        }

        else if (currentLife >= 80)
        {
            barLife.transform.GetChild(1).gameObject.SetActive(false);
            barLife.transform.GetChild(2).gameObject.SetActive(true);
            barLife.transform.GetChild(3).gameObject.SetActive(false);
            barLife.transform.GetChild(4).gameObject.SetActive(false);
            barLife.transform.GetChild(5).gameObject.SetActive(false);
            barLife.transform.GetChild(6).gameObject.SetActive(false);
        }

        else if (currentLife >= 60)
        {
            barLife.transform.GetChild(1).gameObject.SetActive(false);
            barLife.transform.GetChild(2).gameObject.SetActive(false);
            barLife.transform.GetChild(3).gameObject.SetActive(true);
            barLife.transform.GetChild(4).gameObject.SetActive(false);
            barLife.transform.GetChild(5).gameObject.SetActive(false);
            barLife.transform.GetChild(6).gameObject.SetActive(false);
        }

        else if (currentLife >= 40)
        {
            barLife.transform.GetChild(1).gameObject.SetActive(false);
            barLife.transform.GetChild(2).gameObject.SetActive(false);
            barLife.transform.GetChild(3).gameObject.SetActive(false);
            barLife.transform.GetChild(4).gameObject.SetActive(true);
            barLife.transform.GetChild(5).gameObject.SetActive(false);
            barLife.transform.GetChild(6).gameObject.SetActive(false);
        }

        else if (currentLife >= 20)
        {
            barLife.transform.GetChild(1).gameObject.SetActive(false);
            barLife.transform.GetChild(2).gameObject.SetActive(false);
            barLife.transform.GetChild(3).gameObject.SetActive(false);
            barLife.transform.GetChild(4).gameObject.SetActive(false);
            barLife.transform.GetChild(5).gameObject.SetActive(true);
            barLife.transform.GetChild(6).gameObject.SetActive(false);
        }

        if (currentLife <= 0)
        {
            StartCoroutine("Muerto");

            Debug.Log("Ha muerto");

            barLife.transform.GetChild(1).gameObject.SetActive(false);
            barLife.transform.GetChild(2).gameObject.SetActive(false);
            barLife.transform.GetChild(3).gameObject.SetActive(false);
            barLife.transform.GetChild(4).gameObject.SetActive(false);
            barLife.transform.GetChild(5).gameObject.SetActive(false);
            barLife.transform.GetChild(6).gameObject.SetActive(true);
        }
    }

    IEnumerator Muerto()
    {
        Debug.Log(gameObject.name + " ha fallecido :(");

        animator.SetBool("Death", true);

        gameObject.GetComponent<Enemy_scale>().enabled = false;

        enemy.GetComponent<Enemy_Combat>().enabled = false;

        enemy.transform.GetChild(2).gameObject.SetActive(false);

        enemy.transform.GetChild(3).gameObject.SetActive(false);

        enemy.GetComponent<Animator>().SetBool("Jump", false);
        enemy.GetComponent<Animator>().SetBool("Run", false);

        yield return new WaitForSeconds(3.8f);

        Destroy(gameObject);
    }

    IEnumerator KnockbackPlayer(Collider2D player, float time)
    {
        yield return new WaitForSeconds(time);

        knockbackPlayer = false;
    }

    public void Giro()
    {
        enemy.transform.localScale = new Vector3(-10, 10, 1);

        enemy.GetComponent<Animator>().SetBool("Run", false);

        enemy.GetComponent<Animator>().SetBool("Jump", false);
    }
}
