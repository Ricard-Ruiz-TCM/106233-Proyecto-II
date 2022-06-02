using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : EnemyCombat {

    private Rigidbody2D _body;

    public GameObject _handPrefab;
    public GameObject _spawner;

    private GameObject _elementsContainer;
    private GameObject _enemiesContainer;

    void Awake() {
        _health =  100.0f;

        _elementsContainer = GameObject.FindObjectOfType<ElementsContainer>().gameObject;
        _enemiesContainer = GameObject.FindObjectOfType<EnemyContainer>().gameObject;


        _body = GetComponent<Rigidbody2D>();
    }

    public void MeleeAttack(){
        Debug.Log("TE ATACO PUTA");
    }
    
    public void HandAttack(Vector2 position){
        Instantiate(_handPrefab, position, Quaternion.identity, _elementsContainer.transform);
    }

    public void SpawnAttack(float dir){
        GameObject sp = Instantiate(_spawner, transform.position, Quaternion.identity, _elementsContainer.transform);
        sp.GetComponent<GolemSpawner>().SetDir(dir);
    }

}
