using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limite : MonoBehaviour
{
    [Header("Variables de Control:")]

    public bool limite;

    public bool limiteJugador;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    limite = true;
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemmy"))
        {
            limite = false;
        }

        else if (collision.CompareTag("Player"))
        {
            limiteJugador = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemmy"))
        {
            limite = true;
        }

        else if (collision.CompareTag("Player"))
        {
            limiteJugador = true;
        }
    }
}
