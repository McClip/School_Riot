using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Scale : MonoBehaviour
{

    private Rigidbody2D rb2d;
    [SerializeField] public GameObject Jugador;
    [SerializeField] public float speed = 5.0f;
    private bool tiezo;

    private void Start() {
        
        StartCoroutine("activacion");

    }

    private void Update() {
        
        Vector3 direccion = Jugador.transform.position - transform.position;
        
        if (direccion.x >= 0.0f) {

            transform.localScale = new Vector3(10.0f, 10.0f, 1.0f); // Salvando el script

        }

        else {

            transform.localScale = new Vector3(-10.0f, 10.0f, 1.0f);

        }

        if (!tiezo) {

            transform.position = Vector2.MoveTowards(transform.position, Jugador.transform.position, speed * Time.deltaTime);

        }

    }

    IEnumerator activacion() {
        tiezo = true;

        yield return new WaitForSeconds(2f);

        tiezo = false;
    }

}
