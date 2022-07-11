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

        if (vidaActual <= 0)
        {
            Muerto();
        }
    }

    public void Muerto()
    {
        Debug.Log(gameObject.name + " ha fallecido :(");

        //animator.SetBool();

        //Destroy(gameObject);
    }
}
