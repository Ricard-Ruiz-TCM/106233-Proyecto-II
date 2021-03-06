using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatInk : EnemyBullet
{
    private float fwd;
    private Vector2 direction;

    private void Start()
    {
        LoadBullet("BatAttacks");
        Destroy(this.gameObject, 2.5f);
    }

    public void Direction(float dir)
    {
        fwd = dir;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(fwd * -300f, -100.0f));

    }
}

