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

    private GameObject _hand;

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
    
    public void HandAttack(){
        _hand = Instantiate(_handPrefab, _elementsContainer.transform);
        _hand.transform.position = new Vector2(_player.transform.position.x, _player.transform.position.y - 0.35f);
        Invoke("HandDamage", 1.55f);
    }

    private void HandDamage()
    {
        if (_hand == null) return;
        _hand.GetComponentInChildren<HandAttack>().MakeDamage();
    }

    public void SpawnAttack(float dir){
        GameObject sp = Instantiate(_spawner, transform);
        sp.GetComponent<GolemSpawner>().SetDir(dir);
    }

}
