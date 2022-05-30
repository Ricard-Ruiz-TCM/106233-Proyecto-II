using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagmite : MonoBehaviour {

    [SerializeField]
    private bool _falling;
    private bool IsFalling() { return _falling; }

    [SerializeField]
    private float _detectionDistance;

    [SerializeField]
    private Vector2 _position;

    private Transform _player;
    private Rigidbody2D _body;
    private PolygonCollider2D _col;

    
    [SerializeField]
    private Attack _stalagmite;

    // Unity
    void Awake(){
        _falling = false;
        _position = transform.position;
        _detectionDistance = 1.0f;
        _body = GetComponent<Rigidbody2D>();
        _col = GetComponent<PolygonCollider2D>();
        _player = GameObject.FindObjectOfType<Player>().transform;
    }

    // Unity
    void Update() {
        if (IsFalling()) return;
        if (Vector2.Distance(transform.position, _player.position) < _detectionDistance) Fall();
    }

    // Stalagmite.cs
    private void Fall(){
        _falling = true;
        _body.isKinematic = false;
        Invoke("Respawn", 5.0f);
    }

    private void Respawn(){
        _falling = false;
        _body.velocity = Vector2.zero;
        transform.position = _position;
        _body.isKinematic = true;
        _col.isTrigger = false;
    }
        
    // Unity
    void OnCollisionEnter2D(Collision2D collision){
        if (collision == null) return;
        if (collision.gameObject.tag != "Player") return;

        collision.gameObject.GetComponent<Player>().TakeDamage(100, DEATH_CAUSE.D_FALL);
    }

}
