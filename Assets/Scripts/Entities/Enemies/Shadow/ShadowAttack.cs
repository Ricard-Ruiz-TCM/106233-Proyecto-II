using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAttack : EnemyCombat
{
    [SerializeField]
    public Attack currentAttack;
    public Rigidbody2D shadow;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsVisible;
    public float DetectionRange;

    private ShadowAI shadowAI;
    private Animator animator;
    private PlayerCombat _playerCombat;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        shadowAI = GetComponentInParent<ShadowAI>();
        animator = gameObject.GetComponentInParent<Animator>();
        _health = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if(shadowAI.Detected)
        {
            ShadowAttacks();
        }

        if (_health <= 0f)
        {
            animator.SetBool("Dying", true);       
        }
    }

    bool IsInRange()
    {
        float dist = Vector2.Distance(player.position, transform.position);
        return dist < DetectionRange;
    }

    void ShadowAttacks()
    {
        shadow.AddForce(new Vector2(0, transform.up.y * 10.0f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerCombat>();
        if (collision.collider.gameObject.tag == "Player")
        {
            shadow.velocity = Vector2.zero;
            shadow.angularVelocity = 0.0f;

            if (transform.right.x == -1.0f)
            {
                var magnitude = 500;
                var force = transform.position + collision.transform.position;
                var forceNew = new Vector2(-5.1f, 10.0f);
                force.Normalize();
                GetComponent<Rigidbody2D>().AddForce(-forceNew * magnitude);
                player.TakeDamage(currentAttack);
            }
            else if (transform.right.x == 1.0f)
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
}
