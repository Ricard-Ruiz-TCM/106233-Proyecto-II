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
        _health = 45;
    }

    private void Update()
    {
        if (dying)
        {
            ParticleInstancer.Instance.StartParticles("EnemyDie_Particle", transform);
            MusicPlayer.Instance.PlayFX("Die_Slime",1.0f);
            dying = false;
            GetComponent<SlimeAI>().currentState = States.Dying;
            animator.SetBool("Dying", true);
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<CircleCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartCoroutine(DeathDelay(2.2f));

        }
    }

    public void SlimeAttacks()
    {
        slime.AddForce(new Vector2(transform.right.x * 800.0f, 1.0f));
        ParticleInstancer.Instance.StartParticles("DashSlime_Particle", transform);
        MusicPlayer.Instance.PlayFX("Enemy_SlimeAtk_1/Enemy_SlimeAtk_1", 0.5f);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.tag == "Player")
        {
            if (GetComponent<SlimeAI>().currentState == States.Dying) return;
            var player = collision.gameObject.GetComponent<PlayerCombat>();
            if (GetComponent<SlimeAI>().currentState == States.Patrolling) 
            {
                player.TakeDamage(currentAttack);
                
            }
            else
            {
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

        else if(collision.collider.gameObject.tag == "Spikes")
        {
            dying = true;
        }
    }

}
