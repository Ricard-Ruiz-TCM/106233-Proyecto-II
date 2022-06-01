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
    public Rigidbody2D player;
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
    private float Speed = 0.1f;
    private bool detected;
    private Animator animator;
    public ShadowStates shadowState;
    private BoxCollider2D boxColider;
    public bool Detected => detected;
    //private bool _slowMo = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        Vector3 Scaler = transform.localScale;
       /* Scaler.x *= -1;
        transform.localScale = Scaler;*/
        shadowState = ShadowStates.Idle;
        animator = gameObject.GetComponentInParent<Animator>();
        animator.SetBool("Idle", true);
        animator.SetBool("Disappearing", false);
        boxColider = GetComponentInParent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(shadowState == ShadowStates.Idle)
        {
            /*Move();
            maxTime = Random.Range(2.0f, 7.0f);
            currentTime += Time.deltaTime;

            if (currentTime > maxTime)
            {
                Flip();
            }*/
        /*if (EdgeDetected() || WallDetected())
        {
            Flip();
        }
    }*/


        if (shadowState == ShadowStates.Idle)
        {
            if (PlayerDetection())
            {
                shadowState = ShadowStates.Disappearing;
                animator.SetBool("Disappearing", true);
                detected = true;
                shadowState = ShadowStates.Chasing;
                StartCoroutine(AnimationDelay(2f, "Underground"));
                //StartCoroutine(StateDelay(1f, ShadowStates.Chasing));
                boxColider.size = new Vector2(0.3f, 0.1f);
                boxColider.offset = new Vector2(0.0f, -0.25f);

            }
            animator.SetBool("Appearing", false);
            if (GetComponent<ShadowAttack>().attacked == true)
            {
                stopTime += Time.deltaTime;
                if (stopTime > maxTime)
                {
                    shadowState = ShadowStates.Disappearing;
                    stopTime = 0;
                    animator.SetBool("Disappearing", true);
                    GetComponent<ShadowAttack>().attacked = false;
                }
            }
        }


        else if (shadowState == ShadowStates.Chasing)
        {
            Chasing();
            //Move();
            //animator.SetBool("Disappearing", false);
            if (transform.position.x - player.position.x < 0.01f)
            {
                shadowState = ShadowStates.Attacking;
            }
        }

        else if (shadowState == ShadowStates.Attacking)
        {
            //shadow.velocity = Vector2.zero;
            stopTime += Time.deltaTime;
            animator.SetBool("Attacking", true);
            GetComponent<ShadowAttack>().ShadowAttacks();
            if (stopTime > maxTime)
            {
                boxColider.size = new Vector2(0.3f, 0.6f);
                boxColider.offset = new Vector2(0.0f, 0.0f);
                animator.SetBool("Attacking", false);
                animator.SetBool("Appearing", true);
                shadowState = ShadowStates.Appearing;
                stopTime = 0;
                StartCoroutine(AnimationDelay(2f, "Idle"));
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
        Vector2 direction = new Vector2(player.position.x, 0);
        //direction.Normalize();
        //direction = direction.normalized;
        transform.Translate(-direction * Speed * Time.deltaTime, Space.World);
    }

    private bool PlayerDetection()
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
}
