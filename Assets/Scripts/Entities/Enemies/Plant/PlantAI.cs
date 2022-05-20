using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAI : EnemyMovement
{
    public Rigidbody2D player;
    public LayerMask WhatIsPlayer;
    public Transform EdgeDetectionPoint;
    public float DetectionDistance;

    public bool Detected => detected;

    private bool detected;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInChildren<Rigidbody2D>();
        detected = false;
        DetectionDistance = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
       
        if(PlayerDetection())
        {
            //Flip();
            detected = true;
        }

        if (!CheckRotation())
        {
            Flip();
            detected = true;
        }
        if(IsInRange() == false)
        {
            detected = false;
        }

    }

    void Flip()
    {
        transform.Rotate(0, 180, 0);
    }

    private bool CheckRotation()
    {
        RaycastHit2D hit = Physics2D.Raycast(EdgeDetectionPoint.position, transform.right, DetectionDistance, WhatIsPlayer);
        return hit.collider == null;
    }

    private bool PlayerDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(EdgeDetectionPoint.position, transform.right, DetectionDistance, WhatIsPlayer);
        return hit.collider != null;
    }

    bool IsInRange()
    {
        float dist = Vector2.Distance(player.position, transform.position);
        return dist < DetectionDistance;
    }
}
