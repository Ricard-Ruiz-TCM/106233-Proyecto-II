using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombTemplate : Template {

    private List<GameObject> _objects;

    [SerializeField]
    private float _explosionRadius;

    [SerializeField]
    private float _explosionTime;

    [SerializeField]
    private Attack _attack;

    private void Start() {
        load();

        _explosionRadius = 5.0f;
        _explosionTime = 3.0f;

        _attack = Resources.Load<Attack>("ScriptableObjects/BoxTemplate");

        FindObjects();

        Invoke("Explode", _explosionTime);
    }

    public void Explode() {
        foreach(GameObject go in _objects){
            if (Vector2.Distance(transform.position, go.transform.position) < _explosionRadius) {
                Destroy(go);
            }
        }

        GameObject player = GameObject.FindObjectOfType<Player>().gameObject;
        if (Vector2.Distance(transform.position, player.transform.position) < _explosionRadius) player.GetComponent<PlayerCombat>().TakeDamage(_attack);
       
        Destroy(this.gameObject);
    }

    public void FindObjects() {
        _objects = new List<GameObject>(GameObject.FindGameObjectsWithTag("DestroyWall"));
    }

    public override void MainAction(bool action) {
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().isKinematic = false;
    }

}
