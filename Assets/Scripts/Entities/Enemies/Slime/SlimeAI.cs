using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum States
{
    Patrolling,
    Chasing
}

public class SlimeAI : MonoBehaviour
{
    public Rigidbody2D player;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsDetected;
    public LayerMask WhatIsWall;
    public Transform EdgeDetectionPoint;
    public float DetectionDistance = 1.0f;

    private float WallDetectionDistance = 0.2f;
    private float patrollingSpeed = 1.0f;
    private float currentTime;
    private float stopTime;
    private float maxTime = 4.0f;
    private float currentSpeed;
    private States currentState;

    public bool Detected => detected;

    private bool detected;
    // Start is called before the first frame update
    void Start()
    {
        currentState = States.Patrolling;
        currentSpeed = patrollingSpeed;
        player = GetComponent<Rigidbody2D>();
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    // Update is called once per frame
    void Update()
    {
        if(DetectPlayer())
        {
            currentState = States.Chasing;
            detected = true;
        }
        else
        {
            detected = false;
        }

        if(currentState == States.Patrolling)
        { 
            Patrol();
            currentTime += Time.deltaTime;
            if(currentTime > maxTime)
            {
                Flip();
            }
            if(EdgeDetected() || WallDetected())
            {
                Flip();
            }
        }
        else if(currentState == States.Chasing)
        {
            stopTime += Time.deltaTime;
            if(stopTime > 4f)
            {
                currentState = States.Patrolling;
                stopTime = 0;
            }
        }

    }

    void Patrol()
    {
        transform.Translate(transform.right * currentSpeed * Time.deltaTime, Space.World);
    }

    private bool EdgeDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(EdgeDetectionPoint.position, Vector2.down, WallDetectionDistance, WhatIsDetected);
        return hit.collider == null;
    }

    private bool WallDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(EdgeDetectionPoint.position, transform.right, WallDetectionDistance, WhatIsWall);
        return hit.collider != null;
    }

    private bool DetectPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(EdgeDetectionPoint.position, transform.right, DetectionDistance, WhatIsPlayer);
        return hit.collider != null;
    }

    void Flip()
    {
        transform.Rotate(0, 180, 0);
        currentTime = 0;
    }

}
