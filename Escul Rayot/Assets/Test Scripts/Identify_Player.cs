using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identify_Player : MonoBehaviour
{
    public bool tangible;

    public GameObject enemy;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tangible == false && player.GetComponent<Player_Controller>().vidaActual <= 0)
        {
            player.GetComponent<Collider2D>().enabled = true;

            player.GetComponent<Collider2D>().enabled = false;
        }

        if (tangible == true && player.GetComponent<Player_Controller>().vidaActual <= 0)
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;

            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;

            player.GetComponent<Rigidbody2D>().isKinematic = true;

            player.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            tangible = true;
        }

        else
        {
            tangible = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            tangible = false;
        }

        else
        {
            tangible = false;
        }
    }
}
