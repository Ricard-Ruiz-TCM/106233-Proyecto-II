using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInk : EnemyBullet
{
    private float fwd;
    private Vector2 direction;

    private void Start()
    {
        LoadBullet("PlantAttack");
        Destroy(this.gameObject, 2.5f);
    }

    public void Direction(float dir)
    {
        fwd = dir;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(fwd * 200.0f, 125.0f));
        transform.localEulerAngles = new Vector2(0.0f, 180.0f * (Mathf.Ceil(dir) < 0 ? 0 : 1));
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                ColPlayer(collision.gameObject);
            }
        }
    }
}


