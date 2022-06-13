using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHelper : MonoBehaviour
{

    private float alpha;
    private bool fade;

    private void Start()
    {
        alpha = 0.0f;
        fade = true;
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    private void Update()
    {
        if (fade) return;
        alpha += 1.0f * Time.deltaTime;
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        fade = false;
    }
}
