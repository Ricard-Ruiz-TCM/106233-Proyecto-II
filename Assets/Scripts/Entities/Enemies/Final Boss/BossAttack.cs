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
    
    public void HandAttack(Vector3 pos){
        _hand = Instantiate(_handPrefab, _elementsContainer.transform);
        if (_player.GetComponent<PlayerFall>().Grounded()) pos = _player.transform.position;
        _hand.transform.position = new Vector2(pos.x, pos.y - 0.30f);
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
