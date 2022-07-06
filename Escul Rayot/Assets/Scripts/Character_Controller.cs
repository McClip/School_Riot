using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    //variables:
    Rigidbody2D Rb;
    public SpriteRenderer Sr;
    float velocidad = 2;
    // Start is called before the first frame update
    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) 
        {
            Rb.velocity = new Vector2(velocidad,Rb.velocity.y);
            Sr.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Rb.velocity = new Vector2(-velocidad, Rb.velocity.y);
            Sr.flipX = true;
        }
        else
        {
            Rb.velocity = new Vector2(0, Rb.velocity.y);
        }
    }
}
