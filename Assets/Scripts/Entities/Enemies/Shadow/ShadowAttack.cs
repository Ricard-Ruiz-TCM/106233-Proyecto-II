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
    public bool attacked = false;
    // Start is called before the first frame update
    void Start()
    {
        shadowAI = GetComponentInParent<ShadowAI>();
        animator = gameObject.GetComponentInParent<Animator>();
        _health = 20;
        animator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(shadowAI.Detected)
        {
            ShadowAttacks();
        }*/



        if (_health <= 0f)
        {
            animator.SetBool("Dying", true);
            StartCoroutine(DeathDelay(2.5f));
        }
    }

    bool IsInRange()
    {
        float dist = Vector2.Distance(player.position, transform.position);
        return dist < DetectionRange;
    }

    public void ShadowAttacks()
    {
        //shadow.AddForce(new Vector2(0, transform.up.y * 10.0f));
        Debug.Log("Shadow attacks");
    }

    private void OnTriggerEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerCombat>();
        if (collision.gameObject.tag == "Player")
        {
            player.TakeDamage(currentAttack);
            attacked = true;

        }
    }
}
