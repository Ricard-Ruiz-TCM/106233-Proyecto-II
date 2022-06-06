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
        Destroy(this.gameObject, 2.5f);
    }

    protected new void Movement()
    {

    }

    public void Direction(float dir)
    {
        fwd = dir;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(fwd * 200.0f, 125.0f));
        /*_speed = 5.0f; 
        direction = new Vector2(50.0f, 10.0f);
        transform.localEulerAngles = new Vector2(0.0f, (dir < 0 ? 0.0f : 180.0f));
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-(transform.right.x) * direction.x * _speed, direction.y * _speed));*/
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


