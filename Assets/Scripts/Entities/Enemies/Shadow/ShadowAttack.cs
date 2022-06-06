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
    private Transform player;
    public bool attacked = false;
    private bool canBeAttacked = false;
    // Start is called before the first frame update
    void Start()
    {
        shadowAI = GetComponentInParent<ShadowAI>();
        animator = gameObject.GetComponentInParent<Animator>();
        player = GameObject.FindObjectOfType<Player>().transform;
        _health = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (dying)
        {
            dying = false;
            animator.SetBool("Dying", true);
            shadowAI.shadowState = ShadowStates.Dying;
            StartCoroutine(DeathDelay(3f));
        }
    }

    public void ShadowAttacks()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 0.3f)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(_weapon);
        }
    }

   
}
