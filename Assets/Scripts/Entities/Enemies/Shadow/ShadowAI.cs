using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShadowStates
{
    Move,
    MoveUnderground,
    Disappearing,
    Appearing,
    Attacking,
    Dying,

}

public class ShadowAI : EnemyMovement
{
    private Player player;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsWall;
    public LayerMask WhatIsDetected;
    public Transform PlayerDetectionPoint;
    public Rigidbody2D shadow;
    private float DetectionDistance = 2f;
    public float WallDetectionDistance = 0.2f;
    private float currentTime;

    private float attackCD;
    private float Speed = 1f;
    private bool detected;
    private Animator animator;
    public ShadowStates shadowState;
    public bool Detected => detected;
    //private bool _slowMo = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        shadowState = ShadowStates.Move;
        animator = gameObject.GetComponentInParent<Animator>();
        animator.SetBool("Idle", true);
    }

    // Update is called once per frame
    void Update() {
        currentTime += Time.deltaTime;
        attackCD += Time.deltaTime;
        switch (shadowState) {
            case ShadowStates.Move:
                Move();
                if (EdgeDetected() || WallDetected()) {
                    Flip();
                } else if (PlayerDetection()) {
                    detected = true;
                    currentTime = 0.0f;
                    animator.SetBool("Idle", false);
                    animator.SetBool("Disappearing", true);
                    shadowState = ShadowStates.Disappearing;
                }
                break;

            case ShadowStates.Disappearing:
                if (currentTime >= 1.5f) {
                    currentTime = 0.0f;
                    animator.SetBool("Disappearing", false);
                    animator.SetBool("Underground", true);
                    shadowState = ShadowStates.MoveUnderground;
                }
                break;

            case ShadowStates.MoveUnderground:
                //if (PlayerBehind()) Flip();
                if (!EdgeDetected() && !WallDetected()) Chasing();
                else {
                    currentTime = 0.0f;
                    animator.SetBool("Underground", false);
                    animator.SetBool("Appearing", true);
                    shadowState = ShadowStates.Appearing;
                }
                if (Vector2.Distance(player.transform.position, transform.position) > 4.0f) {
                    currentTime = 0.0f;
                    animator.SetBool("Underground", false);
                    animator.SetBool("Appearing", true);
                    shadowState = ShadowStates.Appearing;
                }
                if (Vector2.Distance(transform.position, player.transform.position) < 0.2f) {
                    if (attackCD >= 6.0f) {
                        shadowState = ShadowStates.Attacking;
                        animator.SetBool("Underground", false);
                        animator.SetBool("Attacking", true);
                        Invoke("Attack", 1.35f);
                        attackCD = 0.0f;
                        currentTime = 0.0f;
                    }
                }
                break;

            case ShadowStates.Attacking:
                if (currentTime > 3.5f) {
                    animator.SetBool("Attacking", false);
                    animator.SetBool("Appearing", true);
                    shadowState = ShadowStates.Appearing;
                    currentTime = 0.0f;
                }
                break;

            case ShadowStates.Appearing:
                if (currentTime >= 3.2f) {
                    animator.SetBool("Idle", true);
                    animator.SetBool("Appearing", false);
                    animator.SetBool("Disappearing", false);
                    currentTime = 0.0f;
                    shadowState = ShadowStates.Move;
                }
                break;
        }
    }

    void Move() {
        transform.Translate(transform.right * Speed * Time.deltaTime, Space.World);
    }

    void Flip() {
        transform.Rotate(0, 180, 0);
    }

    private void Chasing() {
        if (Vector2.Distance(transform.position, player.transform.position) > 0.1f) { 
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, transform.position.y), Speed * Time.deltaTime);
            if (PlayerBehind()) Flip();
        }
    }

    private bool PlayerDetection() {
        RaycastHit2D hit = Physics2D.Raycast(PlayerDetectionPoint.position, transform.right, DetectionDistance, WhatIsPlayer);
        return hit.collider != null;
    }

    private bool PlayerBehind() {
        RaycastHit2D hit = Physics2D.Raycast(PlayerDetectionPoint.position, -transform.right, DetectionDistance, WhatIsPlayer);
        return hit.collider != null;
    }

    private bool EdgeDetected() {
        RaycastHit2D hit = Physics2D.Raycast(PlayerDetectionPoint.position, Vector2.down, WallDetectionDistance, WhatIsDetected);
        return hit.collider == null;
    }

    private bool WallDetected() {
        RaycastHit2D hit = Physics2D.Raycast(PlayerDetectionPoint.position, transform.right, WallDetectionDistance, WhatIsWall);
        return hit.collider != null;
    }

    private void Attack() {
        GetComponent<ShadowAttack>().ShadowAttacks();
    }
}
