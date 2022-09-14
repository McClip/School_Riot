using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empuje : MonoBehaviour
{
    public bool empuje;

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

    //IEnumerator esperar(bool estado)
    //{
    //    empuje = estado;

    //    yield return new WaitForSeconds(0.69f);
    //}
}
