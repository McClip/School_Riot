using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinchar : MonoBehaviour
{
    public GameObject player;

    public GameObject Up;

    public float danio = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MuerteLentayDolorosa()
    {
        yield return new WaitForSeconds(0.5f);

        player.GetComponent<Player_Controller>().vidaActual -= danio;

        player.GetComponent<Animator>().SetTrigger("Hurt");

        Debug.Log(player.name + " Le quedan " + player.GetComponent<Player_Controller>().vidaActual + " puntos de vida.");

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(nameof(MuerteLentayDolorosa));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(nameof(MuerteLentayDolorosa));
        }
    }
}
