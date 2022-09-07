using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Ground : MonoBehaviour
{
    [Header("Varables de Control")]

    //variables 
    public bool estaEnElSuelo;

    public bool bustjump;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") /*|| collision.CompareTag("Enemmy")*/) {

            estaEnElSuelo = true;

            bustjump = true;

        }

        else if (collision.CompareTag("Enemmy"))
        {
            estaEnElSuelo = true;

            bustjump = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") /*|| collision.CompareTag("Enemmy")*/) {

            estaEnElSuelo = false;

            bustjump = true;

        }

        else if (collision.CompareTag("Enemmy"))
        {
            estaEnElSuelo = false;

            bustjump = false;
        }
    }
}
