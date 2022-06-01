using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInk : EnemyBullet
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
        GetComponent<Rigidbody2D>().AddForce(new Vector2(fwd * 300.0f, 100.0f));
        /*_speed = 5.0f; 
        direction = new Vector2(50.0f, 10.0f);
        transform.localEulerAngles = new Vector2(0.0f, (dir < 0 ? 0.0f : 180.0f));
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-(transform.right.x) * direction.x * _speed, direction.y * _speed));*/
    }
}


