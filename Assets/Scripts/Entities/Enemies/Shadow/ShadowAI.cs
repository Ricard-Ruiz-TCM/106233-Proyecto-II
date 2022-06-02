using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShadowStates
{
    Idle,
    Disappearing,
    Appearing,
    Chasing,
    Attacking
}

public class ShadowAI : EnemyMovement
{
    public Transform player;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsWall;
    public LayerMask WhatIsDetected;
    public Transform PlayerDetectionPoint;
    public Rigidbody2D shadow;
    private float DetectionDistance = 2f;
    public float WallDetectionDistance = 0.2f;

    private float currentTime;
    private float maxTime;
    private float stopTime;
    private float Speed = 1f;
    private bool detected;
    private Animator animator;
    public ShadowStates shadowState;
    private BoxCollider2D boxColider;
    public bool Detected => detected;
    //private bool _slowMo = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        shadowState = ShadowStates.Idle;
        animator = gameObject.GetComponentInParent<Animator>();
        animator.SetBool("Disappearing", false);
        boxColider = GetComponentInParent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (shadowState == ShadowStates.Idle)
        {
            Move();
            currentTime += Time.deltaTime;

            if (EdgeDetected() || WallDetected())
            {
                Flip();
            }
            else if (PlayerBehind())
            {
                Flip();
            }
            else if (PlayerDetection())
            {
                shadowState = ShadowStates.Disappearing;
                animator.SetBool("Disappearing", true);
                animator.SetBool("Idle", false);
                detected = true;
                StartCoroutine(AnimationDelay(2f, "Underground"));
                StartCoroutine(StateDelay(1f, ShadowStates.Chasing));
                animator.SetBool("Appearing", false);
            }
             
            else if (GetComponent<ShadowAttack>().attacked == true)
            {
                stopTime += Time.deltaTime;
                if (stopTime > maxTime)
                {
                    shadowState = ShadowStates.Appearing;
                    stopTime = 0;
                    animator.SetBool("Appearing", true);
                    GetComponent<ShadowAttack>().attacked = false;
                }
            }
        }

        else if (shadowState == ShadowStates.Chasing)
        {
            Chasing();
            if (EdgeDetected() || WallDetected())
            {
                Flip();
            }
            animator.SetBool("Disappearing", false);
        }

        else if (shadowState == ShadowStates.Attacking)
        {
            stopTime += Time.deltaTime;
            animator.SetBool("Attacking", true);
            if (stopTime > 1)
            {
                animator.SetBool("Attacking", false);
                animator.SetBool("Appearing", true);
                shadowState = ShadowStates.Appearing;
                stopTime = 0;
                
            }
        }

        else if(shadowState == ShadowStates.Appearing)
        {
            stopTime += Time.deltaTime;
            if(stopTime > 2)
            {
                StartCoroutine(StateDelay(2f, ShadowStates.Idle));
                animator.SetBool("Idle", true);
                animator.SetBool("Appearing", false);
                animator.SetBool("Disappearing", false);
                stopTime = 0;
            }
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
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.position.x, transform.position.y), Speed * Time.deltaTime);
    }

    private bool PlayerDetection()
    {
        RaycastHit2D hit = Physics2D.Raycast(PlayerDetectionPoint.position, transform.right, DetectionDistance, WhatIsPlayer);
        return hit.collider != null;
    }

    private bool PlayerBehind()
    {
        RaycastHit2D hit = Physics2D.Raycast(PlayerDetectionPoint.position, -transform.right, DetectionDistance, WhatIsPlayer);
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

    private IEnumerator AnimationDelay(float time, string name)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool(name, true);
    }

    private IEnumerator StateDelay(float time, ShadowStates state)
    {
        yield return new WaitForSeconds(time);
        shadowState = state;
    }

    private IEnumerator MoveDelay(float time, Collider2D coll)
    {
        yield return new WaitForSeconds(time);
        var force = transform.position + coll.transform.position;
        var forceNew = new Vector2(-(transform.right.x) * 400.0f, 10.0f);
        force.Normalize();
        shadow.velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(forceNew);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            transform.position = new Vector2(collision.transform.position.x, transform.position.y);
            shadowState = ShadowStates.Attacking;
            animator.SetBool("Attacking", true);
            //StartCoroutine(MoveDelay(4f, collision));
        }
    }
}
