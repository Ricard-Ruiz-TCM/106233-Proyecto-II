using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BOSS_STATES {
    B_INTRO,
    B_WAIT,
    B_MELEE_ATTACK, 
    B_HAND_ATTACK, 
    B_SPAWN_ATTACK, 
    B_TAKE_DAMAGE, 
    B_MOVING,
    B_DEATH
}

public class BossIA : EnemyMovement {

    [SerializeField]
    private BOSS_STATES _state;
    private BOSS_STATES State() { return _state; }

    private GameObject _player;


    [SerializeField]
    private Attack _meleeAttack;
    [SerializeField]
    private float _meleeAttackTimer;
    [SerializeField]
    private Attack _handAttack;
    [SerializeField]
    private float _handAttackTimer;
    [SerializeField]
    private Attack _spawnAttack;
    [SerializeField]
    private float _spawnAttackTimer;

    // Combat
    private BossAttack _combat;

    [SerializeField]
    private float _waitTime;

    // Components
    private Animator _animator;

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _meleeAttack.Range);
    }

    void Start(){
        _state = BOSS_STATES.B_MOVING;

        _combat = GetComponent<BossAttack>();

        _player = GameObject.FindObjectOfType<Player>().gameObject;

        _meleeAttackTimer = _meleeAttack.Cooldown;
        _handAttackTimer = _handAttack.Cooldown;
        _spawnAttackTimer = _spawnAttack.Cooldown;

        _animator = GetComponent<Animator>();

        _waitTime = 1.5f;

        ChangeState("", BOSS_STATES.B_INTRO, "Intro");
    }

    private void Update() {

        Vector2 x = (_player.transform.position - transform.position);
        x.Normalize();

        if ((x.x < 0) && (transform.right.x > 0)) transform.localEulerAngles = new Vector2(0.0f, 180.0f);
        if ((x.x > 0) && (transform.right.x < 0)) transform.localEulerAngles = new Vector2(0.0f, 0.0f);

        //Debug.Log("Dir: " + x.x + " Right: " + transform.right.x);

        _waitTime -= Time.deltaTime;
        _meleeAttackTimer -= Time.deltaTime;
        _handAttackTimer -= Time.deltaTime;
        _spawnAttackTimer -= Time.deltaTime;

        switch(State()){
            case BOSS_STATES.B_WAIT:
                if (_waitTime <= 0.0f) ChangeState("Wait", BOSS_STATES.B_MOVING, "Move");
                break;
            case BOSS_STATES.B_INTRO:
                if (CheckAnimationEnds()) {
                    ChangeState("Intro", BOSS_STATES.B_MOVING, "Move");
                }
                break;
            case BOSS_STATES.B_MELEE_ATTACK:
                if (CheckAnimationEnds()) {
                    ChangeState("Melee", BOSS_STATES.B_TAKE_DAMAGE, "Take");
                    _meleeAttackTimer = _meleeAttack.Cooldown;
                }
                break;
            case BOSS_STATES.B_HAND_ATTACK:
                if (CheckAnimationEnds()) {
                    ChangeState("Hand", BOSS_STATES.B_WAIT, "Wait");
                    _handAttackTimer = _handAttack.Cooldown;
                }
                break;
            case BOSS_STATES.B_SPAWN_ATTACK:
                if (CheckAnimationEnds()) {
                    ChangeState("Spawn", BOSS_STATES.B_WAIT, "Wait");
                    _spawnAttackTimer = _spawnAttack.Cooldown;
                }
                break;
            case BOSS_STATES.B_TAKE_DAMAGE:
                if (CheckAnimationEnds()) {
                    ChangeState("Take", BOSS_STATES.B_MOVING, "Move");
                }
                break;
            case BOSS_STATES.B_MOVING:
                if ((_handAttackTimer <= 0.0f) && (InDistance(_meleeAttack.Range, _handAttack.Range))) {
                    ChangeState("Move", BOSS_STATES.B_HAND_ATTACK, "Hand");
                    Invoke("HandAttack", 1.0f);
                } else if ((_spawnAttackTimer <= 0.0f) && (InDistance(0.0f, _spawnAttack.Range))) {
                    ChangeState("Move", BOSS_STATES.B_SPAWN_ATTACK, "Spawn");
                    Invoke("SpawnAttack", 1.0f);
                } else {
                    Movement();
                    if ((_meleeAttackTimer <= 0.0f) && CheckDistance(_meleeAttack.Range)) {
                        ChangeState("Move", BOSS_STATES.B_MELEE_ATTACK, "Melee");
                        Invoke("MeleeAttack", 1.0f);
                    }
                }
                break;
            case BOSS_STATES.B_DEATH:
                break;
            default: break;
        }
    }  

    public void MeleeAttack() { _combat.MeleeAttack(); }
    public void HandAttack() {
        Vector2 pos = _player.transform.position;
        pos.y = transform.position.y - 0.8f;
        _combat.HandAttack(pos); 
    }
    public void SpawnAttack() {
        Vector2 point = transform.position;
        point.x += transform.right.x * 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(point, transform.right, 3.0f, LayerMask.GetMask("Enemy"));
        if (hit.collider != null)  return;
        _combat.SpawnAttack(transform.right.x); 
    }

    private void Movement(){
        Vector2 point = transform.position;
        point.x += transform.right.x * 1.0f;
        RaycastHit2D hit = Physics2D.Raycast(point, transform.right, 1.0f, LayerMask.GetMask("Enemy"));
        if (hit.collider != null) {
            ChangeState("Move", BOSS_STATES.B_WAIT, "Wait");
        } else {
            if (Vector2.Distance(transform.position, _player.transform.position) > 0.9f) {
                transform.Translate(new Vector3(Time.deltaTime * 1.0f, 0.0f, 0.0f));
            }
        }
    }

    public bool CheckDistance(float range)
    {
        return (Vector2.Distance(transform.position, _player.transform.position) < range);
    }

    public bool InDistance(float range, float max)
    {
        return ((Vector2.Distance(transform.position, _player.transform.position) > range) && CheckDistance(max));
    }

    private bool CheckAnimationEnds() {
        return (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
    }

    private void ChangeState(string from, BOSS_STATES next, string to)  {
        _state = next;
        _meleeAttackTimer = 1.0f;
        _waitTime = 1.5f;
        if (from != "") _animator.SetBool(from, false);
        if (to != "") _animator.SetBool(to, true);
    }
    

}
