using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInk : InkBullet
{
    private float fwd;

    private void Start()
    {
        LoadBullet("ScriptableObjects/PlayerBullet");
    }

    protected new void Movement()
    {

    }

    public void Direction(float dir)
    {
        fwd = dir;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(fwd * 300.0f, 100.0f));
    }

    private void Update()
    {
        Movement();
    }

}
