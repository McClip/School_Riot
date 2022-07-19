using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Scale : MonoBehaviour
{

    private Rigidbody2D rb2d;
    [SerializeField] public GameObject Jugador;
    [SerializeField] public float speed = 5.0f;
    public float velocidad = 2;

    private void Update() {
        
        Vector3 direccion = Jugador.transform.position - transform.position;
        
        if (direccion.x >= 0.0f) {

            transform.localScale = new Vector3(10.0f, 10.0f, 1.0f);

        }

        else {

            transform.localScale = new Vector3(-10.0f, 10.0f, 1.0f);

        }

        transform.position = Vector2.MoveTowards(transform.position, Jugador.transform.position, speed * Time.deltaTime);

    }
}
