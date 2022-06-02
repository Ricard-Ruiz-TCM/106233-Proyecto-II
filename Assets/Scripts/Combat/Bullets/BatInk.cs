using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatInk : EnemyBullet
{
    private float fwd;
    private Vector2 direction;

    private void Start()
    {
        LoadBullet("InkBullet");
    }

    protected new void Movement()
    {

    }

    public void Direction(float dir)
    {
        fwd = dir;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(fwd * 200f, fwd * 300.0f));

    }
}

