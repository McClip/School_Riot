using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Ground : MonoBehaviour
{
    //variables 
    public static bool estaEnElSuelo;

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
        estaEnElSuelo = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        estaEnElSuelo = false;
    }
}
