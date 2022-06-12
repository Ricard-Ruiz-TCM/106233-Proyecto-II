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
                GetComponent<ShadowAttack>().CanTakeDamage = true;
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
                GetComponent<ShadowAttack>().CanTakeDamage = false;
                if (currentTime >= 1.25f) {
                    currentTime = 0.0f;
                    animator.SetBool("Disappearing", false);
                    animator.SetBool("Underground", true);
                    shadowState = ShadowStates.MoveUnderground;
                }
                break;

            case ShadowStates.MoveUnderground:
                GetComponent<ShadowAttack>().CanTakeDamage = false;
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
                if (Vector2.Distance(transform.position, player.transform.position) < 0.3f) {
                    if (attackCD >= 6.0f) {
                        shadowState = ShadowStates.Attacking;
                        animator.SetBool("Underground", false);
                        animator.SetBool("Attacking", true);
                        Invoke("Attack", 0.50f);
                        attackCD = 0.0f;
                        currentTime = 0.0f;
                    }
                }
                break;

            case ShadowStates.Attacking:
                GetComponent<ShadowAttack>().CanTakeDamage = false;
                if (currentTime > 1.25f) {
                    animator.SetBool("Attacking", false);
                    animator.SetBool("Appearing", true);
                    shadowState = ShadowStates.Appearing;
                    currentTime = 0.0f;
                }
                break;

            case ShadowStates.Appearing:
                GetComponent<ShadowAttack>().CanTakeDamage = true;
                if (currentTime >= 1.25f) {
                    animator.SetBool("Idle", true);
                    animator.SetBool("Appearing", false);
                    animator.SetBool("Disappearing", false);
                    currentTime = 0.0f;
                    shadowState = ShadowStates.Move;
                }
                break;
            case ShadowStates.Dying:
                animator.SetBool("Idle", false);
                animator.SetBool("Appearing", false);
                animator.SetBool("Dying", true);
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
        if (Vector2.Distance(transform.position, player.transform.position) > 0.15f) { 
            transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(player.transform.position.x, transform.position.y), (2.0f * Speed) * Time.deltaTime);
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
