using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAttack : MonoBehaviour
{
    [SerializeField]
    public Attack currentAttack;
    public Rigidbody2D shadow;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsVisible;
    public float DetectionRange;

    private PlayerCombat _playerCombat;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
