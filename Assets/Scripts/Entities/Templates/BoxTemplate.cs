using UnityEngine;

public class BoxTemplate : Template {


    private bool _canKill;
    private bool CanKill() { return _canKill; }

    [SerializeField]
    private Attack _attack;

    // Components
    private Rigidbody2D _body;

    void Start() {
        load();
        _canKill = false;
        _isBox = true;
        _attack = Resources.Load<Attack>("ScriptableObjects/Templates/BoxTemplate");
        _body = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) > 2.0f) _canKill = true;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider == null) return;
        _body.velocity = Vector2.zero;
        if (collision.collider.gameObject.tag == "WallFall") GetComponent<Rigidbody2D>().isKinematic = true;

        if ((collision.collider.gameObject.tag == "Enemy") || (collision.collider.gameObject.tag == "Boss")) {
            if (CanKill())
            {
                if (collision.collider.gameObject.GetComponent<BoxTemplate>() != null) return;
                Destroy(collision.collider.gameObject);
                _body.velocity = Vector2.zero;
            }
        }

        if (collision.collider.gameObject.tag == "Player"){
            if (CanKill()) {
                ParticleInstancer.Instance.StartParticles("BoxDestroy_Particle", transform.position);
                collision.collider.GetComponent<PlayerCombat>().TakeDamage(_attack);
                Destroy(this.gameObject);
            }
        }

        _canKill = false;
    }

    public override void MainAction(bool action) {
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
    }

}
