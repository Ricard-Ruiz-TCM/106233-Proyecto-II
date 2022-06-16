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
        GetComponent<Rigidbody2D>().AddForce(new Vector2(150.0f * dir, 0.0f));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject == null) return;
        if (other.gameObject.tag == "Ground") Spawn();
        if (other.gameObject.tag == "Wall") Spawn();
    }

    private void Spawn(){
        Instantiate(_enemy, transform.position, Quaternion.identity, _enemyContainer.transform);
        MusicPlayer.Instance.PlayFX("Spawn_Golem",1.0f);
        Destroy(this.gameObject);
    }




}
