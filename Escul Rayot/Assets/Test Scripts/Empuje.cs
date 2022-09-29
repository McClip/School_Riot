using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empuje : MonoBehaviour
{
    public bool empuje;

    public GameObject player;

    public GameObject pinchazo;

    public float cronometro = 0f;

    // Start is called before the first frame update
    void Start()
    {
        pinchazo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (empuje == true)
        {
            cronometro += 1;

            //Debug.Log(cronometro);
        }

        if (empuje == false)
        {
            cronometro = 0f;

            //Debug.Log(cronometro);
        }

        if (cronometro >= 15f)
        {
            pinchazo.gameObject.SetActive(true);  
        }

        else if (cronometro < 15f)
        {
            pinchazo.gameObject.SetActive(false);
        }

        if (player.GetComponent<Player_Controller>().vidaActual <= 0)
        {
            pinchazo.gameObject.SetActive(false);

            cronometro = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            empuje = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            empuje = false;
        }
    }
}
