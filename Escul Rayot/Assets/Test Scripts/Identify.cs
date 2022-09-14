using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Identify : MonoBehaviour
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
        if (tangible == false && enemy.GetComponent<Enemy_Combat>().currentLife <= 0)
        {
            enemy.GetComponent<Collider2D>().enabled = true;

            enemy.GetComponent<Collider2D>().enabled = false;
        }

        if (tangible == true && enemy.GetComponent<Enemy_Combat>().currentLife <= 0)
        {
            enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;

            enemy.GetComponent<Rigidbody2D>().isKinematic = true;

            enemy.GetComponent<Collider2D>().enabled = false;
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
