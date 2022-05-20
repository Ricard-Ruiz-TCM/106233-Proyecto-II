using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : EnemyCombat
{
    public float attackSpeed;
    public Rigidbody2D slime;
    public Attack currentAttack;

    private SlimeAI slimeAI;

    // Start is called before the first frame update
    void Start()
    {
        slimeAI = gameObject.GetComponent<SlimeAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(slimeAI.Detected)
        {
            SlimeAttacks();          
        }
    }

    void SlimeAttacks()
    {
        slime.AddForce(new Vector2(transform.right.x * 10.0f, 1.0f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerCombat>();
        if(collision.collider.gameObject.tag == "Player")
        {
            slime.velocity = Vector2.zero;
            slime.angularVelocity = 0.0f;
            //slime.constraints = RigidbodyConstraints2D.FreezePositionX;
            
            if(transform.right.x == -1.0f)
            {
                var magnitude = 500;
                var force = transform.position + collision.transform.position;
                var forceNew = new Vector2(-5.1f, 10.0f);
                force.Normalize();
                GetComponent<Rigidbody2D>().AddForce(-forceNew * magnitude);
                player.TakeDamage(currentAttack);
            }
            else if(transform.right.x == 1.0f)
            {
                var magnitude = 500;
                var force = transform.position - collision.transform.position;
                var forceNew = new Vector2(5.1f, 10.0f);
                force.Normalize();
                GetComponent<Rigidbody2D>().AddForce(-forceNew * magnitude);
                player.TakeDamage(currentAttack);
            }
           
        }
    }

    /*private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            slime.constraints = RigidbodyConstraints2D.None;
            slime.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }*/
}
