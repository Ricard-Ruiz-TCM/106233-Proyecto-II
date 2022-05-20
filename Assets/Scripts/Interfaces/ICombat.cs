using UnityEngine;

public interface ICombat {

    public void Attack(ICombat target);
    public void TakeDamage(Attack weapon);

}