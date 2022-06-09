using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOMBRADIUS : MonoBehaviour
{

    Transform player;

    BombTemplate bomb;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindObjectOfType<Player>().transform;
        bomb = transform.parent.GetComponent<BombTemplate>();
    }

    // Update is called once per frame
    void Update() {
        if (!bomb.Placed()) return;
        if (Vector2.Distance(this.transform.position, player.position) < bomb.Radius) {
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 0.1f);
        } else {
            GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 0.0f);
        }
    }
}
