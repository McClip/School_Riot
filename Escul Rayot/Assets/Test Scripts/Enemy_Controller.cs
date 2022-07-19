using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public Animator animator;

    public Rigidbody2D rb2d;

    public float maxVida = 100f;

    public float vidaActual;

    public GameObject colliderPinto;

    // Start is called before the first frame update
    void Start()
    {
        vidaActual = maxVida;
        colliderPinto.GetComponent<Collider2D>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Daño(float damage)
    {
        vidaActual -= damage;

        animator.SetTrigger("Hurt");

        if (vidaActual <= 0)
        {
            StartCoroutine("Muerto");
        }
    }

    IEnumerator Muerto()
    {
        Debug.Log(gameObject.name + " ha fallecido :(");

        animator.SetBool("Death", true);

        gameObject.GetComponent<Enemy_Controller>().enabled = false;
        gameObject.GetComponent<Enemy_Scale>().enabled = false;

        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        colliderPinto.GetComponent<Collider2D>().enabled = true;

        yield return new WaitForSeconds(3.2f);

        Destroy(gameObject);
        Destroy(colliderPinto.gameObject);
    }

    public void mirarJugador() {

        // if ((jugador.position.x > transform.position.x && !mirandoIzquierda) || (jugador.position.x < transform.position.x && mirandoIzquierda)) {

            //mirandoIzquierda = !mirandoIzquierda;
          //  transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);

        //}

    }

}
