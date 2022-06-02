using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BatStates
{
    Patrolling,
    Chasing
}
public class BatAI : MonoBehaviour
{
    private Player player;
    public Rigidbody2D fenix;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsDetected;
    public Transform PlayerDetectionPoint;
    public Transform EdgeDetectionPoint;
    public float DetectionDistance = 1.0f;

    private float DetectionRange = 3.5f;
    private float VisionAngle = 90f;
    public float FOV = 90f;
    private float WallDetectionDistance = 1.0f;
    private float currentTime;
    private float forceTime;
    private float maxTime;
    private float secondsForce;
    private float maxForceTime = 0.2f;
    private bool forceAdded = false;
    private float Speed = 2f;
    private float chasingSpeed = 0.8f;
    private bool detected;
    public BatStates state;

    public bool Detected => detected;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.red;
        var direction = Quaternion.AngleAxis(VisionAngle / 2, transform.forward)
            * -transform.up;
        Gizmos.DrawRay(transform.position, direction * DetectionRange);
        var direction2 = Quaternion.AngleAxis(-VisionAngle / 2, transform.forward)
            * -transform.up;
        Gizmos.DrawRay(transform.position, direction2 * DetectionRange);

        Gizmos.color = Color.white;
    }


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        fenix = GetComponent<Rigidbody2D>();
        secondsForce = 0;
        fenix.gravityScale = 0.4f;
        //forceAdded = true;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        state = BatStates.Patrolling;
    }

    // Update is called once per frame
    void Update()
    {
            // forceTime += Time.deltaTime;

            /* if(forceAdded)
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
             }*/
        if (state == BatStates.Patrolling)
        {
            Fly();
            maxTime = Random.Range(3.0f, 5.0f);
            currentTime += Time.deltaTime;
            if (currentTime > maxTime)
            {
                Turn();
            }
            else if (EdgeDetected())
            {
                Turn();
            }
            if (IsInRange() && IsInVisionAngle())
            {
                state = BatStates.Chasing;
                //detected = true;
            }

        }
        else if(state == BatStates.Chasing)
        {
            ChasePlayer();
            Flip();
            if (IsInRange() && IsInVisionAngle())
            {
                detected = true;
            }
            else
            {
                detected = false;
                state = BatStates.Patrolling;
            }
        }
        
    }

    void Fly()
    {
        transform.Translate(transform.right * Speed * Time.deltaTime, Space.World);
    }

    void Turn()
    {
        //transform.rotation = Quaternion.Euler(0, 180, 0);
        transform.Rotate(0, 180, 0);
        currentTime = 0;
    }

    void Flip()
    {
        if(player.transform.forward.z == 1 && transform.forward.z == -1)
        {
            transform.Rotate(0, 180, 0);
        }
        else if (player.transform.forward.z == -1 && transform.forward.z == 1)
        {
            transform.Rotate(0, 180, 0);
        }
    }

    /*private bool PlayerDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(PlayerDetectionPoint.position, -transform.up, DetectionDistance, WhatIsPlayer);
        return hit.collider != null;
    }*/

    bool IsInRange()
    {
        float dist = Vector2.Distance(player.transform.position, transform.position);
        return dist < DetectionRange;
    }

    private float GetAngle()
    {
        Vector2 v1 = -transform.up;
        Vector2 v2 = player.transform.position - transform.position;
        return Vector2.Angle(v1, v2);
    }

    private bool IsInVisionAngle()
    {
        float angle = GetAngle();
        return FOV >= 2 * angle;
    }

    private bool EdgeDetected()
    {
        RaycastHit2D hit = Physics2D.Raycast(EdgeDetectionPoint.position, transform.right, WallDetectionDistance, WhatIsDetected);
        return hit.collider != null;
    }

    void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, player.transform.position.y + 0.8f), chasingSpeed * Time.deltaTime);
    }
}
