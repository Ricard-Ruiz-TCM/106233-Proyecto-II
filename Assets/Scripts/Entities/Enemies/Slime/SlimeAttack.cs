using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : EnemyCombat
{
    public float attackSpeed;
    public Rigidbody2D slime;
    public Attack currentAttack;

    private SlimeAI slimeAI;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        slimeAI = gameObject.GetComponent<SlimeAI>();
        animator = gameObject.GetComponentInParent<Animator>();
        _health = 15.0f;
    }

    public new void TakeDamage(Attack weapon)
    {
        base.TakeDamage(weapon);
        if (dying){
            animator.SetBool("Dying", true);
        }
    }

    public void SlimeAttacks()
    {
        slime.AddForce(new Vector2(transform.right.x * 800.0f, 1.0f));
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            var player = collision.gameObject.GetComponent<PlayerCombat>();
            slime.velocity = Vector2.zero;
            slime.angularVelocity = 0.0f;
            var force = transform.position + collision.transform.position;
            var forceNew = new Vector2(-(transform.right.x) * 400.0f, 10.0f);
            force.Normalize();
            slime.velocity = Vector2.zero;
            GetComponent<Rigidbody2D>().AddForce(forceNew);
            player.TakeDamage(currentAttack);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.5f);
        GameManager.Instance.InstantiateInkPot(transform.position);
        Destroy(this.gameObject);
    }
}
