using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, ISlowMo
{

    protected Attack _attack;

    // ISlowMo
    [SerializeField]
    public bool _slowMo;
    protected float _speed;

    protected void LoadBullet(string attack)
    {
        _attack = Resources.Load<Attack>("ScriptableObjects/Attacks/" + attack);
        _slowMo = false;
        _speed = 5.0f;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            if (collision.collider.gameObject.tag == "Player")
            {
                ColPlayer(collision.collider.gameObject);
                
            }
            if(collision.collider.gameObject.tag != "Enemy")
            {
                Destroy(this.gameObject);
            }
           
        }
    }

    protected void ColPlayer(GameObject player)
    {
        player.GetComponent<PlayerCombat>().TakeDamage(_attack);
    }

    // ISlowMo
    public void DisableSlowMo() { _slowMo = false; _speed *= 2.0f; }
    public void EnableSlowMo() { _slowMo = true; _speed /= 2.0f; }
    public bool IsSlowTime() { return _slowMo; }

}
