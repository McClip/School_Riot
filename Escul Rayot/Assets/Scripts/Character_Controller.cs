using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    //variables:
    public Animator An;
    Rigidbody2D Rb;
    public SpriteRenderer Sr;
    public float velocidad = 2;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) 
        {
            Rb.velocity = new Vector2(velocidad,Rb.velocity.y);
            Sr.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Rb.velocity = new Vector2(-velocidad, Rb.velocity.y);
            Sr.flipX = true;
        }
        else
        {
            Rb.velocity = new Vector2(0, Rb.velocity.y);
        }

        //golpe

        if (Input.GetKeyDown(KeyCode.H))
        {
            StartCoroutine("duracion");
        }
    }

    IEnumerator duracion()
    {
        An.SetBool("punch", true);

        yield return new WaitForSeconds(0.96f);

        An.SetBool("punch", false);
    }
}
