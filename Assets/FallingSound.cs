using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSound : MonoBehaviour
{
    private bool touch;
    private float tiempo;
    private void Start()
    {
        touch = false;
        tiempo = 0;

    }
    private void OnEnable()
    {
        Player.OnRespawn += () => { touch = false; };

    }
    private void OnDisable()
    {
        Player.OnRespawn -= () => { touch = false; };

    }

    private void Update()
    {
        tiempo += Time.deltaTime;
        if (touch==true&&tiempo>=6)
        {
            MusicPlayer.Instance.PlayFX("PlayerVoice_Dead_fall",1.0f);
            touch = false;
            tiempo = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        if (touch == false) { touch = true;}
    }
}
