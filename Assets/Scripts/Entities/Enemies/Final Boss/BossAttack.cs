using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : EnemyCombat {

    private Rigidbody2D _body;

    private Transform _player;

    public GameObject _handPrefab;
    public GameObject _spawner;

    private GameObject _elementsContainer;
    private GameObject _enemiesContainer;

    void Awake() {
        _health =  100.0f;

        _elementsContainer = GameObject.FindObjectOfType<ElementsContainer>().gameObject;
        _enemiesContainer = GameObject.FindObjectOfType<EnemyContainer>().gameObject;

        _player = GameObject.FindObjectOfType<Player>().transform;


        _body = GetComponent<Rigidbody2D>();
    }

    public void MeleeAttack(){
        if (Vector2.Distance(this.transform.position, _player.position) < 2.2f)
        {
            _player.GetComponent<PlayerCombat>().TakeDamage(_weapon);
        }
    }
    
    public void HandAttack(Vector2 position){
        Instantiate(_handPrefab, position, Quaternion.identity, _elementsContainer.transform);
    }

    public void SpawnAttack(float dir){
        GameObject sp = Instantiate(_spawner, transform);
        sp.GetComponent<GolemSpawner>().SetDir(dir);
    }

}
