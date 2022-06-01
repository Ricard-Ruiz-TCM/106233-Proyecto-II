using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private bool _animation;

    [SerializeField]
    private float _spanDistance;

    [SerializeField]
    private GameObject _enemy;

    private GameObject _enemyContainer;
    private GameObject _spawnAnim;

    private GameObject _player;

    private bool _tryToSpawn;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _spanDistance);
    }

    void Awake() {
        _enemyContainer = GameObject.FindObjectOfType<EnemyContainer>().gameObject;
        _spawnAnim = Resources.Load<GameObject>("Prefabs/ENEMY_SPAWN_ANIM");

        _tryToSpawn = true;

        _player = GameObject.FindObjectOfType<Player>().gameObject;

    }

    private void Update() {
        if (_tryToSpawn && Vector2.Distance(_player.transform.position, transform.position) < _spanDistance) {
            Spawn();
        }
    }

    void Spawn() {
        _tryToSpawn = false;
        if (_animation) SpawnHand();
        else {
            Instantiate(_enemy, transform.position, Quaternion.identity, _enemyContainer.transform);
            Destroy(this.gameObject);
        }
    }

    void SpawnHand() {
        Vector2 pos = transform.position; pos.x += 1.2f;
        Instantiate(_spawnAnim, pos, Quaternion.identity);
        _animation = false;
        Invoke("Spawn", 1.5f);
    }



}
