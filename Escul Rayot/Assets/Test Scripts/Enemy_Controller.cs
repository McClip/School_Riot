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

    public GameObject barraVida;

    public GameObject campo;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<Limite>();

        vidaActual = maxVida;
        colliderPinto.GetComponent<Collider2D>().enabled = false;

        barraVida.transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (campo.GetComponent<Limite>().limite == true)
        {
            //vidaActual = 0;

            StartCoroutine("Muerto");

            barraVida.transform.GetChild(0).gameObject.SetActive(false);
            barraVida.transform.GetChild(1).gameObject.SetActive(false);
            barraVida.transform.GetChild(2).gameObject.SetActive(false);
            barraVida.transform.GetChild(3).gameObject.SetActive(false);
            barraVida.transform.GetChild(4).gameObject.SetActive(false);
            barraVida.transform.GetChild(5).gameObject.SetActive(true);
        }

    }

    public void Daño(float damage)
    {
        vidaActual -= damage;

        animator.SetTrigger("Hurt");

        if (vidaActual == 100)
        {
            barraVida.transform.GetChild(0).gameObject.SetActive(true);
            barraVida.transform.GetChild(1).gameObject.SetActive(false);
            barraVida.transform.GetChild(2).gameObject.SetActive(false);
            barraVida.transform.GetChild(3).gameObject.SetActive(false);
            barraVida.transform.GetChild(4).gameObject.SetActive(false);
            barraVida.transform.GetChild(5).gameObject.SetActive(false);
        }

        else if (vidaActual == 80)
        {
            barraVida.transform.GetChild(0).gameObject.SetActive(false);
            barraVida.transform.GetChild(1).gameObject.SetActive(true);
            barraVida.transform.GetChild(2).gameObject.SetActive(false);
            barraVida.transform.GetChild(3).gameObject.SetActive(false);
            barraVida.transform.GetChild(4).gameObject.SetActive(false);
            barraVida.transform.GetChild(5).gameObject.SetActive(false);
        }

        else if (vidaActual == 60)
        {
            barraVida.transform.GetChild(0).gameObject.SetActive(false);
            barraVida.transform.GetChild(1).gameObject.SetActive(false);
            barraVida.transform.GetChild(2).gameObject.SetActive(true);
            barraVida.transform.GetChild(3).gameObject.SetActive(false);
            barraVida.transform.GetChild(4).gameObject.SetActive(false);
            barraVida.transform.GetChild(5).gameObject.SetActive(false);
        }

        else if (vidaActual == 40)
        {
            barraVida.transform.GetChild(0).gameObject.SetActive(false);
            barraVida.transform.GetChild(1).gameObject.SetActive(false);
            barraVida.transform.GetChild(2).gameObject.SetActive(false);
            barraVida.transform.GetChild(3).gameObject.SetActive(true);
            barraVida.transform.GetChild(4).gameObject.SetActive(false);
            barraVida.transform.GetChild(5).gameObject.SetActive(false);
        }

        else if (vidaActual == 20)
        {
            barraVida.transform.GetChild(0).gameObject.SetActive(false);
            barraVida.transform.GetChild(1).gameObject.SetActive(false);
            barraVida.transform.GetChild(2).gameObject.SetActive(false);
            barraVida.transform.GetChild(3).gameObject.SetActive(false);
            barraVida.transform.GetChild(4).gameObject.SetActive(true);
            barraVida.transform.GetChild(5).gameObject.SetActive(false);
        }

        if (vidaActual <= 0)
        {
            StartCoroutine("Muerto");

            barraVida.transform.GetChild(0).gameObject.SetActive(false);
            barraVida.transform.GetChild(1).gameObject.SetActive(false);
            barraVida.transform.GetChild(2).gameObject.SetActive(false);
            barraVida.transform.GetChild(3).gameObject.SetActive(false);
            barraVida.transform.GetChild(4).gameObject.SetActive(false);
            barraVida.transform.GetChild(5).gameObject.SetActive(true);
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

        yield return new WaitForSeconds(3.8f);

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
