using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAttack : EnemyCombat
{
    [SerializeField]
    public Attack currentAttack;
    [SerializeField]
    private GameObject _bullet;
    private PlayerCombat _playerCombat;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsVisible;
    public GameObject container;
    private BatAI batAI;
    public float DetectionRange;
    public float VisionAngle;
    public float FOV = 90f;

    private float currentTime;
    private float maxTime = 2f;
    private Transform player;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        batAI = GetComponent<BatAI>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > maxTime)
        {
            if (batAI.state == BatStates.Chasing)
            {
                InkAttack();
                base.Attack(_playerCombat);
            }
        }

        if (dying)
        {
            //animator.SetBool("Dying", true);
            StartCoroutine(DeathDelay(0.5f));
        }

    }

    void InkAttack()
    {
        Vector2 init = new Vector2(transform.position.x + 0.05f, transform.position.y - 0.1f);
        GameObject bullet = Instantiate(_bullet, init, Quaternion.identity, container.transform);
        bullet.GetComponent<BatInk>().Direction(player.transform.position.y + 0.5f);
        currentTime = 0;
    }
}
