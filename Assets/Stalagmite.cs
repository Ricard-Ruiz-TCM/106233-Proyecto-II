using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalagmite : MonoBehaviour {

    private bool _falling;
    private bool IsFalling() { return _falling; }

    private bool _canFall;
    private bool CanFall() { return _canFall; }

    [SerializeField]
    private float _fallDistance;

    [SerializeField]
    private float _detectionDistance;

    private LayerMask _playerLayer;

    private Vector2 _position;

    private Transform _player;
    private Rigidbody2D _body;
    private PolygonCollider2D _col;

    
    [SerializeField]
    private Attack _stalagmite;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _detectionDistance);
        Gizmos.DrawRay(new Ray(transform.position, -transform.up));
    }

    // Unity
    void Awake(){
        _falling = false;
        _canFall = false;
        _position = transform.position;
        _body = GetComponent<Rigidbody2D>();
        _col = GetComponent<PolygonCollider2D>();
        _player = GameObject.FindObjectOfType<Player>().transform;

        _playerLayer = LayerMask.GetMask("Player");
    }

    // Unity
    void Update() {
        if (IsFalling()) return;
        if (!CanFall() && Vector2.Distance(transform.position, _player.position) < _detectionDistance) Shake();
        if (CanFall()) {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, _fallDistance, _playerLayer);
            if (hit.collider != null) {
                  Fall();
            }
        }
    }

    // Stalagmite.cs
    private void Fall(){
        _falling = true;
        _body.isKinematic = false;
        _col.isTrigger = true;
        Invoke("Respawn", 5.0f);
        GetComponent<Animator>().SetBool("Shake", false);
    }

    private void Shake() {
        _canFall = true;
        GetComponent<Animator>().SetBool("Shake", true);
        ParticleInstancer.Instance.StartParticles("Particula_Estalactita", transform);

    }

    private void Respawn(){
        _falling = false;
        _canFall = false;
        _body.velocity = Vector2.zero;
        transform.position = _position;
        _body.isKinematic = true;
        _col.isTrigger = false;
        GetComponent<Animator>().SetBool("Shake", false);
    }

    // Unity
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision == null) return;
        if (collision.gameObject.tag != "Player") return;

        collision.gameObject.GetComponent<Player>().TakeDamage(100, DEATH_CAUSE.D_FALL);
    }

    // Unity
    void OnCollisionEnter2D(Collision2D collision){
        if (collision == null) return;
        if (collision.gameObject.tag != "Player") return;

        collision.gameObject.GetComponent<Player>().TakeDamage(100, DEATH_CAUSE.D_FALL);
    }

}
