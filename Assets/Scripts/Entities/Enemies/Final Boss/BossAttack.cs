using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : EnemyCombat {

    private Rigidbody2D _body;

    void Awake() {
        _health =  100.0f;

        _body = GetComponent<Rigidbody2D>();
    }

    public void MeleeAttack(){
        
    }
    
    public void HandAttack(Vector2 position){
        // Spawn de la mano (la mano funciona sola)
    }

    public void SpawnAttack(int dir){
        // Lanza el prefab de bote de tinta
    }

}
