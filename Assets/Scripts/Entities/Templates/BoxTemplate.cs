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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == null) return;
        if (collision.gameObject.tag == "Enemy") {
            if (CanKill()) {
                if (collision.gameObject.GetComponent<BoxTemplate>() != null) return;
                collision.gameObject.GetComponent<ICombat>().TakeDamage(_attack);
                _body.velocity = Vector2.zero;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider == null) return;

        _body.velocity = Vector2.zero;
        MusicPlayer.Instance.PlayFX("Box_place/Box_place_" + ((int)Random.Range(1, 3)).ToString(), 0.5f);
        if (collision.collider.gameObject.tag == "FaddedGround") GetComponent<Rigidbody2D>().isKinematic = true;

        if (collision.collider.gameObject.tag == "Enemy") {
            if (CanKill()){
                if (collision.collider.gameObject.GetComponent<BoxTemplate>() != null) return;
                collision.collider.gameObject.GetComponent<ICombat>().TakeDamage(_attack);
                _body.velocity = Vector2.zero;
            }
        }

        if (collision.collider.gameObject.tag == "Spikes") {
            ParticleInstancer.Instance.StartParticles("BoxDestroy_Particle", transform.position);
            MusicPlayer.Instance.PlayFX("Player_destroy_box", 0.5f);
            Destroy(this.gameObject);
        }

        if (collision.collider.gameObject.tag == "Boss")
        {
            if (collision.collider.gameObject.GetComponent<BoxTemplate>() != null) return;
            if (CanKill()) collision.collider.gameObject.GetComponent<BossAttack>().TakeDamage(_attack);
            ParticleInstancer.Instance.StartParticles("BoxDestroy_Particle", transform.position);
            MusicPlayer.Instance.PlayFX("Player_destroy_box", 0.5f);
            Destroy(this.gameObject);
        }

        if (collision.collider.gameObject.tag == "Player"){
            if (CanKill()) {
                ParticleInstancer.Instance.StartParticles("BoxDestroy_Particle", transform.position);
                collision.collider.GetComponent<PlayerCombat>().TakeDamage(_attack);
                MusicPlayer.Instance.PlayFX("Player_destroy_box", 0.5f);
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
