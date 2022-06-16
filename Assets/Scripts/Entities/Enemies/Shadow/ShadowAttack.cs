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
    private Transform player;
    public bool attacked = false;
    // Start is called before the first frame update
    void Start()
    {
        shadowAI = GetComponentInParent<ShadowAI>();
        player = GameObject.FindObjectOfType<Player>().transform;
        _health = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (dying)
        {
            dying = false;
            ParticleInstancer.Instance.StartParticles("SombraDie_Particle", transform);
            GetComponent<Rigidbody2D>().isKinematic = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
            shadowAI.shadowState = ShadowStates.Dying;
            StartCoroutine(DeathDelay(1.25f));
            MusicPlayer.Instance.PlayFX("Sombra_Die", 1.0f);
        }
    }

    public void ShadowAttacks()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 0.3f)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(currentAttack);
            MusicPlayer.Instance.PlayFX("Enemy_ShadowAtk/Enemy_ShadowAtk_" + ((int)Random.Range(1, 2)).ToString(), 0.5f);
        }
    }

   
}
