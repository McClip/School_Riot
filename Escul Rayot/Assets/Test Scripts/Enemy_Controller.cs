using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public Animator animator;

    public float maxVida = 100f;

    public float vidaActual;

    // Start is called before the first frame update
    void Start()
    {
        vidaActual = maxVida;
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

        gameObject.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(6.7f);

        Destroy(gameObject);
    }
}
