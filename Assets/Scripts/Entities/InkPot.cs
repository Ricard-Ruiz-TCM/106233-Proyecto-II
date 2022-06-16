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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == null) return;

        if (collision.gameObject.gameObject.tag == "Player") {
            collision.gameObject.gameObject.GetComponent<Player>().AddInk(5);
            MusicPlayer.Instance.PlayFX("Pickin_Ink", 1.0f);
            Destroy(this.gameObject);
        }
    }
}
