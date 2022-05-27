using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatAI : MonoBehaviour
{
    public Rigidbody2D player;
    public Rigidbody2D fenix;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsDetected;
    public Transform PlayerDetectionPoint;
    public Transform EdgeDetectionPoint;
    public float DetectionDistance = 1.0f;

    private float WallDetectionDistance = 1.0f;
    private float currentTime;
    private float forceTime;
    private float maxTime;
    private float secondsForce;
    private float maxForceTime = 0.2f;
    private bool forceAdded = false;
    private float Speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        fenix = GetComponent<Rigidbody2D>();
        secondsForce = 0;
        fenix.gravityScale = 0.4f;
        //forceAdded = true;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    // Update is called once per frame
    void Update()
    {
        Fly();
        maxTime = Random.Range(5.0f, 10.0f);
        currentTime += Time.deltaTime;
        forceTime += Time.deltaTime;

        if(forceAdded)
        {
            secondsForce += Time.deltaTime;
            if (secondsForce < maxForceTime)
            {
                fenix.AddForce(Vector2.up);
            }
            else
            {
                secondsForce = 0;
                forceAdded = false;
                fenix.velocity = new Vector2(0, 0);
            }
        }
        if(forceTime > 0.6f)
        {
            forceAdded = true;
            forceTime = 0;
        }
        if (currentTime > maxTime)
        {
            Turn();
        }
        else if(EdgeDetected())
        {
            Turn();
        }
        if (PlayerDetection())
        {
            //ChasePlayer();
        }

        
    }

    void Fly()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime, Space.World);
    }

    void Turn()
    {
        transform.Rotate(0, 180, 0);
        currentTime = 0;
        //fenix.AddForce(Vector2.up);
        //fenix.AddForce(Vector2.right);
    }

    private bool PlayerDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(PlayerDetectionPoint.position, transform.right, DetectionDistance, WhatIsPlayer);
        return hit.collider != null;
    }

    private bool EdgeDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(EdgeDetectionPoint.position, transform.right, WallDetectionDistance, WhatIsDetected);
        return hit.collider != null;
    }

    void ChasePlayer()
    {
        transform.position = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
    }
}
