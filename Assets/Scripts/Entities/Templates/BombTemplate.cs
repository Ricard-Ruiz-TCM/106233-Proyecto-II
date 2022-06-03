using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombTemplate : Template {

    private List<GameObject> _objects;

    [SerializeField]
    private float _explosionRadius;

    private TextMesh _text;
    private Color _color;
    private SpriteRenderer _spritebg;

    [SerializeField]
    private float _explosionTime;

    [SerializeField]
    private Attack _attack;

    private void Start() {
        load();

        _explosionRadius = 5.0f;
        _explosionTime = -1.0f;

        _text = GetComponentInChildren<TextMesh>();
        _spritebg = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _color = _spritebg.color;

        _text.text = "";

        _attack = Resources.Load<Attack>("ScriptableObjects/Templates/BoxTemplate");

        FindObjects();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == null) return;
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
    }

    private void Update(){
        if (_explosionTime > 0.0f)
        {
            _explosionTime -= Time.deltaTime;
            _text.text = ((int)_explosionTime).ToString();
            _color.a = 1.0f;
            _spritebg.color = _color;
        }
        else
        {
            _text.text = "";
            _color.a = 0.0f;
            _spritebg.color = _color;
        }
    }

    public void Explode() {
        GetComponent<Animator>().SetBool("Explode", true);
        foreach (GameObject go in _objects){
            if (Vector2.Distance(transform.position, go.transform.position) < _explosionRadius) {
                Destroy(go);
            }
        }

        GameObject player = GameObject.FindObjectOfType<Player>().gameObject;
        if (Vector2.Distance(transform.position, player.transform.position) < _explosionRadius) player.GetComponent<PlayerCombat>().TakeDamage(_attack);

        List<GameObject> _trees = new List<GameObject>(GameObject.FindGameObjectsWithTag("FallTree"));

        foreach (GameObject go in _trees){
            if (Vector2.Distance(transform.position, go.transform.position) < _explosionRadius)
            {
                go.GetComponent<Animator>().SetBool("Fall", true);
            }
        }

        List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        foreach (GameObject go in enemies)
        {
            if (Vector2.Distance(transform.position, go.transform.position) < _explosionRadius)
            {
                go.GetComponent<ICombat>().TakeDamage(_attack);
            }
        }

        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<Rigidbody2D>().isKinematic = true;


        Destroy(this.gameObject, 1.3f);
    }

    public void FindObjects() {
        _objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("DestroyWall"));
    }

    public override void MainAction(bool action) {
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
        _explosionTime = 4.0f;
        Invoke("Explode", _explosionTime);
    }

}
