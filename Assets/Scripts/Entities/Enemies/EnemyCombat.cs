using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : Entity, ICombat {

    // ICombat
    public float Health => _health;
    // ICombat
    public Attack ActiveWeapon => _weapon;

    protected float _health;
    protected Attack _weapon;
    

    public void Attack(ICombat target)
    {
        Debug.Log("Enemy Attack()");
    }

    public void TakeDamage(Attack weapon)
    {
        _health -= weapon.Damage;
        if (_health <= 0.0f){
            GameManager.Instance.InstantiateInkPot(transform.position);
            Destroy(this.gameObject);
        }
    }

}
