using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private bool _animation;

    [SerializeField]
    private float _spanDistance;

    private float _BspanDistance;


    GameObject spawned;

    [SerializeField]
    private GameObject _enemy;

    private GameObject _enemyContainer;
    private GameObject _spawnAnim;

    private GameObject _player;

    public Attack _destroyer;

    private bool _tryToSpawn;

    private void OnEnable()
    {
        Player.OnRespawn += ResSpawn;
    }

    private void OnDisable()
    {
        Player.OnRespawn -= ResSpawn;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _spanDistance);
    }

    public void ResSpawn()
{
        if (!_tryToSpawn) {
            _tryToSpawn = true;
            _animation = false;
            _spanDistance = _BspanDistance * 2.0f;
            if (spawned != null){
                spawned.GetComponent<ICombat>().TakeDamage(_destroyer);
                spawned = null;
            }
            if (Vector2.Distance(_player.transform.position, transform.position) < _spanDistance) _animation = true;
        }
    }

    void Awake() {
        _enemyContainer = GameObject.FindObjectOfType<EnemyContainer>().gameObject;
        _spawnAnim = Resources.Load<GameObject>("Prefabs/ENEMY_SPAWN_ANIM");

        _destroyer = Resources.Load<Attack>("ScriptableObjects/Attacks/Destroyer");

        _tryToSpawn = true;

        _player = GameObject.FindObjectOfType<Player>().gameObject;
    }

    private void Start()
    {
        _BspanDistance = _spanDistance;
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
            spawned = Instantiate(_enemy, transform.position, Quaternion.identity, _enemyContainer.transform);
            //Destroy(this.gameObject);
        }
    }

    void SpawnHand() {
        Vector2 pos = transform.position; pos.x += 1.2f;
        Instantiate(_spawnAnim, pos, Quaternion.identity);
        MusicPlayer.Instance.PlayFX("Dibujo");
        _animation = false;
        Invoke("Spawn", 1.5f);
    }



}
