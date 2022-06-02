using UnityEngine;
using System.Collections.Generic;

public class GolemSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject _enemy;

    private GameObject _enemyContainer;

    void Awake(){
        
        _enemyContainer = GameObject.FindObjectOfType<EnemyContainer>().gameObject;
    }

    public void SetDir(float dir){
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(100.0f * dir, 175.0f));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == null) return;
        if (other.gameObject.tag == "Ground") Spawn();
        if (other.gameObject.tag == "Wall") Spawn();
    }

    private void Spawn(){
        Vector2 pos = transform.position;
        pos.y += 1.0f;
        Instantiate(_enemy, pos, Quaternion.identity, _enemyContainer.transform);
        Destroy(this.gameObject);
    }




}
