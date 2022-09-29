using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : MonoBehaviour
{
    public int attackDamage = 20;

    public int enrangedAttackDamage = 40;

    public Vector3 attackOffset;

    public float attackRange = 1f;

    public LayerMask attackMask;

    public GameObject player;

    public void Attack()
    {
        Vector3 pos = transform.position;

        pos += transform.right * attackOffset.x;

        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);

        if (colInfo != null)
        {
            colInfo.GetComponent<Player_Controller>().DañoJugador(attackDamage);
        }

        if (player.GetComponent<Player_Controller>().vidaActual <= 0)
        {
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
