using System;
using UnityEngine;
using System.Collections;

public enum COMBAT_STATE {
    C_MELEE, C_RANGED
}

public class PlayerCombat : PlayerState, ICombat, IHaveStates {

    public static event Action OnAttack;
    public static event Action OnEndAttack;

    private bool _attacking;
    public bool IsAttacking() { return _attacking; }
    public bool AttackEnds() { return (!IsAttacking()); }
    
    // Attack Control
    protected float _attackTime;

    // Combat State rel. Weapon
    [SerializeField]
    private COMBAT_STATE _state;
    public COMBAT_STATE CombatState() { return _state; }
    private bool Melee() { return (CombatState() == COMBAT_STATE.C_MELEE); }
    private bool Ranged() { return (CombatState() == COMBAT_STATE.C_RANGED); }

    // Active Weapon
    [SerializeField]
    protected Attack _weapon;

    // Attacks
    private Attack _melee;
    private Attack _ranged;

    // Lyaer
    [SerializeField]
    private LayerMask _layer_Enemy;

    [SerializeField]
    private GameObject _inkBullet;
    private GameObject _container;

    // PlayerSateMachine
    protected Player _player;

    void Awake(){
        LoadState();
        /////////////
        _attacking = false;
        _attackTime = 0.0f;

        _state = COMBAT_STATE.C_MELEE;

        _melee = Resources.Load<Attack>("ScriptableObjects/Attacks/MeleeAttack");
        _ranged = Resources.Load<Attack>("ScriptableObjects/Attacks/RangedAttack");
        _weapon = _melee;

        _layer_Enemy = LayerMask.GetMask("Enemy", "Object");

        _inkBullet = Resources.Load<GameObject>("Prefabs/Player/PlayerBullet");
        _container = GameObject.FindObjectOfType<ElementsContainer>().gameObject;

        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    // PlayerCombat.cs <Combat>
    public void ChangeState() {
        if (CombatState() == COMBAT_STATE.C_MELEE) _state = COMBAT_STATE.C_RANGED;
                                              else _state = COMBAT_STATE.C_MELEE;

        if (Melee()) _weapon = _melee;
        if (Ranged()) _weapon = _ranged;
    }

    private void EndAttack() {
        _attacking = false;
        OnEndAttack?.Invoke();
    }

    private ICombat FindTarget(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, _weapon.Range, _layer_Enemy);
        if (hit.collider != null){
            if (hit.collider.gameObject.tag == "Enemy"){
                return hit.collider.gameObject.GetComponent<ICombat>();    
            }
        }
        return null;
    }

    // PlayerCombat.cs <Melee>
    private void MeleeAttack() {
        _body.velocity = Vector2.zero;
        _body.AddForce(new Vector2(transform.right.x * 150.0f, 75.0f));
        StartCoroutine(AttackDelay(0.3f));
    }

    // PlayerCombat.cs <Ranged>
    private void RangedAttack(){
        if (_player.Ink() < _weapon.InkCost) return;
        _player.UseInk(_weapon.InkCost);
        GameObject bullet = Instantiate(_inkBullet, new Vector2(this.transform.position.x + 0.005f, this.transform.position.y + 0.5f), Quaternion.identity, _container.transform);
        bullet.GetComponent<PlayerBullet>().Dir(this.transform.right.x);
    }

    // ICombat
    public void Attack(ICombat target){
        _attackTime = 0.0f;
        _attacking = true;
        OnAttack?.Invoke();
        if (Ranged()) RangedAttack();
        if (Melee()) MeleeAttack();
    }

    public void TakeDamage(Attack weapon){ _player.TakeDamage(weapon.Damage);
    }

    // IHaveStates
    public void OnEnterState(){
        EnableSystem();
        ///////////////
        Attack(null);
        if (Melee())
        {
            _animator.SetBool("Melee", true);
        }
        if (Ranged()) _animator.SetBool("Ranged", true);
    }

    public void OnExitState(){
        _animator.SetBool("Melee", false);
        _animator.SetBool("Ranged", false);
        ////////////////
        DisableSystem();
        OnEndAttack?.Invoke();
        //StopAllCoroutines();
    }

    public void OnState(){
        if (!IsEnabled()) return;

        if (Melee()) {
            ICombat target = FindTarget();
            if (target != null)
            {
                target.TakeDamage(_weapon);
            }
        }

        _attackTime += Time.deltaTime;
        if (_attackTime >= _weapon.Cooldown) EndAttack();
    }


    private IEnumerator AttackDelay(float time)
    {
        yield return new WaitForSeconds(time);
        ParticleInstancer.Instance.StartParticles("Particles_attack", new Vector2(transform.position.x + 0.8f, transform.position.y));
    }
}
