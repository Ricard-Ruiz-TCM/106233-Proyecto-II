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
    private float _detectionDistance;

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

    private void OnEnable()
    {
        Fader.OnFullAlpha += Respawn;
    }

    private void OnDisable()
    {
        Fader.OnFullAlpha -= Respawn;
    }

    public GameObject BossSpawner;

    private void Respawn(){
        GameObject g = Instantiate(BossSpawner, GameObject.FindObjectOfType<ElementsContainer>().transform);
        g.transform.position = new Vector3(94.482f, -70.544f, 0.0f);
        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy")){
            Destroy(enemies);
        }
        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Boss")) Destroy(enemies);
    }

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

        _detectionDistance = 2.1f;

        _animator = GetComponent<Animator>();

        _waitTime = 2.5f;

        ChangeState("", BOSS_STATES.B_INTRO, "Intro");
    }

    private Vector3 _handPos;

    private void Update() {

        _waitTime -= Time.deltaTime;
        _meleeAttackTimer -= Time.deltaTime;
        _handAttackTimer -= Time.deltaTime;
        _spawnAttackTimer -= Time.deltaTime;

        switch(State()){
            case BOSS_STATES.B_INTRO:
                rotation();
                if (GetComponent<BossAttack>().Dying) ChangeState("Intro", BOSS_STATES.B_DEATH, "Die");
                else if (_waitTime <= 0.0f) {
                    ChangeState("Intro", BOSS_STATES.B_MOVING, "Move");
                }
                break;
            case BOSS_STATES.B_MELEE_ATTACK:
                rotation();
                if (GetComponent<BossAttack>().Dying) ChangeState("Melee", BOSS_STATES.B_DEATH, "Die");
                else if (_waitTime <= 0.0f) {
                    ChangeState("Melee", BOSS_STATES.B_TAKE_DAMAGE, "Take");
                    _meleeAttackTimer = _meleeAttack.Cooldown;
                    _waitTime = 2.5f;
                }
                break;
            case BOSS_STATES.B_HAND_ATTACK:
                rotation();
                if (GetComponent<BossAttack>().Dying) ChangeState("Hand", BOSS_STATES.B_DEATH, "Die");
                else if (_waitTime <= 0.0f) {
                    ChangeState("Hand", BOSS_STATES.B_MOVING, "Move");
                    _handAttackTimer = _handAttack.Cooldown;
                }
                break;
            case BOSS_STATES.B_SPAWN_ATTACK:
                rotation();
                if (GetComponent<BossAttack>().Dying) ChangeState("Spawn", BOSS_STATES.B_DEATH, "Die");
                else if (_waitTime <= 0.0f) {
                    ChangeState("Spawn", BOSS_STATES.B_MOVING, "Move");
                    _spawnAttackTimer = _spawnAttack.Cooldown;
                }
                break;
            case BOSS_STATES.B_TAKE_DAMAGE:
                rotation();
                if (GetComponent<BossAttack>().Dying) ChangeState("Take", BOSS_STATES.B_DEATH, "Die");
                else if (_waitTime <= 0.0f) {
                    ChangeState("Take", BOSS_STATES.B_MOVING, "Move");
                }
                break;
            case BOSS_STATES.B_MOVING:
                rotation();
                if (GetComponent<BossAttack>().Dying) ChangeState("Move", BOSS_STATES.B_DEATH, "Die");
                else if ((_handAttackTimer <= 0.0f) && (InDistance(_meleeAttack.Range, _handAttack.Range) && (_player.GetComponent<PlayerFall>().Grounded()))) {
                    _handPos = _player.transform.position;
                    ChangeState("Move", BOSS_STATES.B_HAND_ATTACK, "Hand");
                    _waitTime = 2.5f;
                    Invoke("HandAttack", 1.0f);
                } else if ((_spawnAttackTimer <= 0.0f) && (InDistance(0.0f, _spawnAttack.Range) && (CanSpawnAttack()))) {
                    ChangeState("Move", BOSS_STATES.B_SPAWN_ATTACK, "Spawn");
                    _waitTime = 1.25f;
                    Invoke("SpawnAttack", 1.0f);
                } else {
                    Movement();
                    if ((_meleeAttackTimer <= 0.0f) && CheckDistance(_meleeAttack.Range)) {
                        ChangeState("Move", BOSS_STATES.B_MELEE_ATTACK, "Melee");
                        _waitTime = 1.2f;
                        Invoke("MeleeAttack", 0.8f);
                    }
                }
                break;
            case BOSS_STATES.B_DEATH:
                if (toDestroy) { toDestroy = false; GetComponent<PolygonCollider2D>().isTrigger = true; GetComponent<Rigidbody2D>().isKinematic = true; }
                break;
            default: break;
        }
    }

    private bool toDestroy = true;

    public void MeleeAttack() { _combat.MeleeAttack(); }
    public void HandAttack() { _combat.HandAttack(_handPos);  }

    public bool CanSpawnAttack(){
        Vector2 point = transform.position;
        point.x += transform.right.x * 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(point, transform.right, 1.55f, LayerMask.GetMask("Enemy", "Wall"));
        return (hit.collider == null);
    }

    private void rotation()
    {
        Vector2 x = (_player.transform.position - transform.position);
        x.Normalize();
        if ((x.x < 0) && (transform.right.x > 0)) transform.localEulerAngles = new Vector2(0.0f, 180.0f);
        if ((x.x > 0) && (transform.right.x < 0)) transform.localEulerAngles = new Vector2(0.0f, 0.0f);
    }

    public void SpawnAttack() {
        _combat.SpawnAttack(transform.right.x); 
    }

    private void Movement(){
        Vector2 point = transform.position;
        point.x += transform.right.x * 1.5f;
        RaycastHit2D hit = Physics2D.Raycast(point, transform.right, 1.5f, LayerMask.GetMask("Enemy", "Wall"));
        if (hit.collider == null) {
            if (Vector2.Distance(transform.position, _player.transform.position) > _detectionDistance) {
                transform.Translate(new Vector3(Time.deltaTime * 1.0f, 0.0f, 0.0f));
            }
        }
    }

    public bool CheckDistance(float range) {
        return (Vector2.Distance(transform.position, _player.transform.position) < range);
    }

    public bool InDistance(float range, float max) {
        return ((Vector2.Distance(transform.position, _player.transform.position) > range) && CheckDistance(max));
    }

    private void ChangeState(string from, BOSS_STATES next, string to)  {
        _state = next;
        _meleeAttackTimer = 1.0f;
        if (from != "") _animator.SetBool(from, false);
        if (to != "") _animator.SetBool(to, true);
    }
    

}
