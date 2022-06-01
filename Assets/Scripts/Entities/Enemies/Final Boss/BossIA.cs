using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BOSS_STATES {
    B_INTRO,
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

    // Components
    private Animator _animator;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f);
        Gizmos.DrawWireSphere(transform.position, _meleeAttack.Range);
        Gizmos.color = new Color(0.0f, 1.0f, 0.0f);
        Gizmos.DrawWireSphere(transform.position, _handAttack.Range);
        Gizmos.color = new Color(0.0f, 0.0f, 1.0f);
        Gizmos.DrawWireSphere(transform.position, _spawnAttack.Range);
    }

    void Start(){
        _state = BOSS_STATES.B_MOVING;

        _combat = GetComponent<BossAttack>();

        _player = GameObject.FindObjectOfType<Player>().gameObject;

        _meleeAttackTimer = _meleeAttack.Cooldown;
        _handAttackTimer = _handAttack.Cooldown;
        _spawnAttackTimer = _spawnAttack.Cooldown;

        _animator = GetComponent<Animator>();

        ChangeState("", BOSS_STATES.B_INTRO, "Intro");
    }

    private void Update() {

        _meleeAttackTimer -= Time.deltaTime;
        _handAttackTimer -= Time.deltaTime;
        _spawnAttackTimer -= Time.deltaTime;

        switch(State()){
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
                    ChangeState("Hand", BOSS_STATES.B_MOVING, "Move");
                    _handAttackTimer = _handAttack.Cooldown;
                }
                break;
            case BOSS_STATES.B_SPAWN_ATTACK:
                if (CheckAnimationEnds()) {
                    ChangeState("Spawn", BOSS_STATES.B_MOVING, "Move");
                    _spawnAttackTimer = _spawnAttack.Cooldown;
                }
                break;
            case BOSS_STATES.B_TAKE_DAMAGE:
                if (CheckAnimationEnds()) {
                    ChangeState("Take", BOSS_STATES.B_MOVING, "Move");
                }
                break;
            case BOSS_STATES.B_MOVING:
                if ((_handAttackTimer <= 0.0f) && (CheckDistance(_handAttack.Range))) {
                    ChangeState("Move", BOSS_STATES.B_HAND_ATTACK, "Hand");
                    Invoke("HandAttack", 1.0f);
                } else if ((_spawnAttackTimer <= 0.0f) && (CheckDistance(_spawnAttack.Range))) {
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
        pos.y -= 1.0f;
        _combat.HandAttack(pos); 
    }
    public void SpawnAttack() {
        _combat.SpawnAttack((int)transform.right.x); 
    }

    private void Movement(){
        Vector2 x = (_player.transform.position - transform.position);
        x.Normalize();
        if (Vector2.Distance(transform.position, _player.transform.position) > 1.5f)
        {
            transform.Translate(new Vector3(x.x * Time.deltaTime * 1.0f, 0.0f, 0.0f));
        }
    }

    public bool CheckDistance(float range)
    {
        return (Vector2.Distance(transform.position, _player.transform.position) < range);
    }

    private bool CheckAnimationEnds() {
        return (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
    }

    private void ChangeState(string from, BOSS_STATES next, string to)  {
        _state = next;
        _meleeAttackTimer = 1.0f;
        if (from != "") _animator.SetBool(from, false);
        if (to != "") _animator.SetBool(to, true);
    }
    

}
