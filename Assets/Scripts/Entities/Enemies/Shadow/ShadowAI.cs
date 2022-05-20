using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAI : EnemyMovement
{
    public Rigidbody2D player;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsWall;
    public LayerMask WhatIsDetected;
    public Transform PlayerDetectionPoint;
    public float DetectionDistance = 1.0f;
    public float WallDetectionDistance = 0.2f;

    private float currentTime;
    private float maxTime;
    private float Speed = 1.0f;
    //private bool _slowMo = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        maxTime = Random.Range(2.0f, 7.0f);
        currentTime += Time.deltaTime;
        if (currentTime > maxTime)
        {
            Flip();
            /*if (EdgeDetected() || WallDetected())
            {
                Flip();
            }*/
        }
       
        if (PlayerDetection())
        {
            Debug.Log("attack");
        }
    }

    void Move()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime, Space.World);
    }

    void Flip()
    {
        transform.Rotate(0, 180, 0);
        currentTime = 0;
    }

    private void Chasing()
    {
        transform.position = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
    }

    private bool PlayerDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(PlayerDetectionPoint.position, transform.right, DetectionDistance, WhatIsPlayer);
        return hit.collider != null;
    }

    private bool EdgeDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(PlayerDetectionPoint.position, Vector2.down, WallDetectionDistance, WhatIsDetected);
        return hit.collider == null;
    }

    private bool WallDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(PlayerDetectionPoint.position, transform.right, WallDetectionDistance, WhatIsWall);
        return hit.collider != null;
    }
}
