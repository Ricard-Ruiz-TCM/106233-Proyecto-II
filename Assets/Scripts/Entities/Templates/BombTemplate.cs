using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombTemplate : Template {

    private List<GameObject> _objects;

    public float Radius => _explosionRadius;

    [SerializeField]
    private float _explosionRadius;

    private TextMesh _text;
    private Color _color;
    private SpriteRenderer _spritebg;

    [SerializeField]
    private float _explosionTime;

    private bool _Exploded;
    public bool Exploded() { return _Exploded; }

    [SerializeField]
    private Attack _attack;

    private int _particlesID;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    private void Start() {
        load();

        _explosionRadius = 5.0f;
        _explosionTime = -1.0f;
        _Exploded = false;
        _text = GetComponentInChildren<TextMesh>();
        _spritebg = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _color = _spritebg.color;

        _text.text = "";

        _attack = Resources.Load<Attack>("ScriptableObjects/Templates/BombTemplate");

        _particlesID = ParticleInstancer.Instance.StartSpecialParticles("MechaBomba_Particle", transform);

        FindObjects();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == null) return;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
        MusicPlayer.Instance.PlayFX("Bomb_place/Bomb_Place_" + ((int)Random.Range(1, 3)).ToString(), 0.5f);
    }

    private void Update(){
        if (_explosionTime > 0.0f)
        {
            _explosionTime -= Time.deltaTime;
            _text.text = ((int)_explosionTime).ToString();
            _color.a = 1.0f;
            _spritebg.color = _color;
            MusicPlayer.Instance.PlayFX("Bomb_Timer", 0.5f);
        }
        else
        {
            _text.text = "";
            _color.a = 0.0f;
            _spritebg.color = _color;
        }
    }

    public void Explode() {
        _Exploded = true;
        GetComponent<Animator>().SetBool("Explode", true);
        MusicPlayer.Instance.StopFX("Bomb_Timer");
        foreach (GameObject go in _objects){
            if (Vector2.Distance(transform.position, go.transform.position) < _explosionRadius) {
                ParticleInstancer.Instance.StartParticles("WallBreak_Particles", go.transform.position);
                Destroy(go);
            }
        }

        GameObject player = GameObject.FindObjectOfType<Player>().gameObject;
        if (Vector2.Distance(transform.position, player.transform.position) < _explosionRadius) player.GetComponent<PlayerCombat>().TakeDamage(_attack);

        List<GameObject> _trees = new List<GameObject>(GameObject.FindGameObjectsWithTag("FallTree"));

        foreach (GameObject go in _trees){
            if (Vector2.Distance(transform.position, go.transform.position) < _explosionRadius)
            {
                ParticleInstancer.Instance.StartParticles("BoxDestroy_Particle", go.transform);
                go.GetComponent<Animator>().SetBool("Fall", true);
            }
        }

        List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        foreach (GameObject go in enemies)
        {
            if (Vector2.Distance(transform.position, go.transform.position) < _explosionRadius)
            {
                if (go.GetComponent<ICombat>() == null) break;
                go.GetComponent<ICombat>().TakeDamage(_attack);
            }
        }

        List<GameObject> bosses = new List<GameObject>(GameObject.FindGameObjectsWithTag("Boss"));
        foreach (GameObject go in bosses) {
            if (Vector2.Distance(transform.position, go.transform.position) < _explosionRadius * 2.0f) {
                go.GetComponent<BossAttack>().TakeDamage(_attack);
            }
        }

        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;

        this.transform.localScale = new Vector2(3.0f, 3.0f);
        this.transform.Translate(new Vector3(0.0f, 0.35f, 0.0f));

        ParticleInstancer.Instance.StartParticles("BombExplosion_Particle", transform);

        Destroy(transform.Find("Circle").gameObject);

        MusicPlayer.Instance.PlayFX("Explosion_bomb", 1f);
        Destroy(this.gameObject, 1.3f);
    }

    public void FindObjects() {
        _objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("DestroyWall"));
    }

    public override void MainAction(bool action) {
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
        _explosionTime = 4.0f;
        Invoke("StopP", _explosionTime * 0.8f);
        Invoke("Explode", _explosionTime);
    }

    private void StopP(){
        ParticleInstancer.Instance.StopParticles(_particlesID);
    }

}
