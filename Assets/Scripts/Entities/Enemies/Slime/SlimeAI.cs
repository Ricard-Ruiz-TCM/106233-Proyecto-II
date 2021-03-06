using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum States
{
    Patrolling,
    Chasing,
    Attacking,
    Dying
}

public class SlimeAI : MonoBehaviour
{
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsDetected;
    public LayerMask WhatIsWall;
    public Transform EdgeDetectionPoint;
    public float DetectionDistance = 5.0f;

    private float WallDetectionDistance = 0.25f;
    private float patrollingSpeed = 1.0f;
    private float stopTime;
    private float currentSpeed;
    public States currentState;

    public bool Attack() { return (currentState.Equals(States.Attacking)); }

    // Start is called before the first frame update
    void Start()
    {
        currentState = States.Patrolling;
        currentSpeed = patrollingSpeed;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        stopTime = 0.0f;
    }

    // Update is called once per frame
    void Update() {
        stopTime += Time.deltaTime;
        if(currentState == States.Patrolling) { 
            Patrol();
            if(EdgeDetected() || WallDetected()) Flip();
            if(DetectPlayer() && stopTime > 1.0f) {
                currentState = States.Chasing;
                stopTime = 0;
            }
        } else if(currentState == States.Chasing) {
            if(stopTime > 0.2f) {
            currentState = States.Attacking;
            GetComponent<SlimeAttack>().SlimeAttacks();
            stopTime = 0;
            }
        } else if (currentState == States.Attacking){
            RaycastHit2D hit = Physics2D.Raycast(EdgeDetectionPoint.position, transform.right, WallDetectionDistance / 2.0f, WhatIsWall);
            if (hit.collider != null)
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (stopTime > 3.0f) {
                currentState = States.Patrolling;
                stopTime = 0;
            }
        }
    }

    void Patrol() {
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
        RaycastHit2D hit = Physics2D.Raycast(EdgeDetectionPoint.position, transform.right, 3f, WhatIsPlayer);
        return hit.collider != null;
    }

    void Flip()
    {
        transform.Rotate(0, 180, 0);
    }

}
