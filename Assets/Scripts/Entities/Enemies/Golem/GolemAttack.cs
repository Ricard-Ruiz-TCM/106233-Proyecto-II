using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttack : EnemyCombat
{
    private GameObject player;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1.35f);
    }

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>().gameObject;
        _weapon = Resources.Load<Attack>("ScriptableObjects/Attacks/GolemAttack");
    }

    void Update()
    {
        if (dying)
        {
            dying = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<Animator>().SetBool("Die", true);
            StartCoroutine(DeathDelay(0.75f));
            ParticleInstancer.Instance.StartSpecialParticles("GolemDie_Particle", transform);

        }
    }

    public void Attack()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 1.35f)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(_weapon);
        }
    }
}
