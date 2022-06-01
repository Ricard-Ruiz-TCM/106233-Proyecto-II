using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BOSS_STATES {
    B_MELEE_ATTACK, 
    B_HAND_ATTACK, 
    B_SPAWN_ATTACK, 
    B_TAKE_DAMAGE, 
    B_MOVING
}

public class BossIA : EnemyMovement {

    [SerializeField]
    private BOSS_STATES _state;
    private BOSS_STATES State() { return _state; }

    private GameObject _player;

    [SerializeField]
    private Attack _melee;
    private float _meleTimer;
    [SerializeField]
    private Attack _handAttack;
    private float _handAttackTimer;
    [SerializeField]
    private Attack _spawnAttack;
    private float _spawnAttackTimer;

    // Combat
    private BossAttack _combat;

    // Components
    private Animator _animator;

    void Start(){
        _state = BOSS_STATES.B_MOVING;

        _combat = GetComponent<BossAttack>();

        _player = GameObject.FindObjectOfType<Player>().gameObject;

        _animator = GetComponent<Animator>();
    }

    private void Update() {
        // reduce cooldown de todas las habilidades por Time.deltaTime
        switch(State()){
            case BOSS_STATES.B_MELEE_ATTACK:
                // Se le dice al BossAttack que realize el ataque
                // Animación acabada?
                    // Cmabio a takeDamage
                break;
            case BOSS_STATES.B_HAND_ATTACK: 
                // animación acabada?
                    // cambia a moving
                break;
            case BOSS_STATES.B_SPAWN_ATTACK: 
                // Animacion acabada
                    // Cambia am oving
                break;
            case BOSS_STATES.B_TAKE_DAMAGE: 
                // X Tiempo pasado
                    // Cambia a Moving
                break;
            case BOSS_STATES.B_MOVING: 
            if (_handAttackTimer <= 0.0f) 
                ChangeState("Move", BOSS_STATES.B_HAND_ATTACK, "Hand");
            else if (_spawnAttackTimer <= 0.0f) 
                ChangeState("Move", BOSS_STATES.B_SPAWN_ATTACK, "Spawn");
            else {
                Movement();
                if (Vector2.Distance(transform.position, _player.transform.position) < _melee.Range) 
                    ChangeState("Move", BOSS_STATES.B_MELEE_ATTACK, "Melee");
            }
                break;
            default: break;
        }
    }  

    private void Movement(){

    }

    private void ChangeState(string fromAnim, BOSS_STATES next, string to)  {
        _state = next;
        _animator.SetBool(fromAnim, false);
        _animator.SetBool(to, true);
    }
    

}
