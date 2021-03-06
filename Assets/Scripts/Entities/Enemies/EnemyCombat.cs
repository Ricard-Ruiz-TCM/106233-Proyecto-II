using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : Entity, ICombat {

    // ICombat
    public float Health => _health;
    // ICombat
    public Attack ActiveWeapon => _weapon;

    [SerializeField]
    protected float _health;
    [SerializeField]
    protected Attack _weapon;
    protected bool dying = false;
    public bool Dying => dying;

    public bool CanTakeDamage = true;

    private int _id;

    public void Attack(ICombat target)
    {

    }

    public void TakeDamage(Attack weapon)
    {
        if (!CanTakeDamage) return;
        _health -= weapon.Damage;

        //_id = ParticleInstancer.Instance.StartSpecialParticles("Particles_attack", transform);
        if (_health <= 0.0f){
            dying = true;
        }
        if(dying==false)
        {
            ParticleInstancer.Instance.StartParticles("TakeDamageEnemy_Particle", transform);
            MusicPlayer.Instance.PlayFX("Enemy_Hit_ByPlayer",1.0f);

        }

    }

    protected IEnumerator DeathDelay(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.Instance.InstantiateInkPot(transform.position);
        Destroy(this.gameObject);
        MusicPlayer.Instance.PlayFX("Enemy_Death/Enemy_Death", 0.5f);
    }
}
