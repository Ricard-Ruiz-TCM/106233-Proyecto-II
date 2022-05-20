using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkPot : MonoBehaviour
{

    private Vector2 _pos;

    public float _time;

    void Start() {
        _time = 0.0f;
        _pos = transform.position;
    }
    
    void Update() {
        _time += Time.deltaTime;
        transform.position = _pos + new Vector2(0.0f, Mathf.Sin(_time * 5.0f /*speed*/) * 0.2f /*disatnce*/);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject == null) return;

        if (collision.collider.gameObject.tag == "Player") {
            collision.collider.gameObject.GetComponent<Player>().AddInk(5);
            Destroy(this.gameObject);
        }
    }
}
