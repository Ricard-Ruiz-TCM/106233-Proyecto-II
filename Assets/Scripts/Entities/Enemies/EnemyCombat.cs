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
    private bool dying = false;
    public bool Dying => dying;

    public void Attack(ICombat target)
    {
        Debug.Log("Enemy Attack()");
    }

    public void TakeDamage(Attack weapon)
    {
        _health -= weapon.Damage;
        if (_health <= 0.0f){
            dying = true;
            //GameManager.Instance.InstantiateInkPot(transform.position);
            //Destroy(this.gameObject);
            StartCoroutine(DeathDelay());
        }
    }

    protected IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.InstantiateInkPot(transform.position);
        Destroy(this.gameObject);
    }
}
