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
    protected bool dying = false;
    public bool Dying => dying;

    public bool CanTakeDamage = true;

    public void Attack(ICombat target)
    {

    }

    public void TakeDamage(Attack weapon)
    {
        if (!CanTakeDamage) return;
        _health -= weapon.Damage;
        if (_health <= 0.0f){
            dying = true;
        }
        
    }

    protected IEnumerator DeathDelay(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.Instance.InstantiateInkPot(transform.position);
        Destroy(this.gameObject);
    }
}
