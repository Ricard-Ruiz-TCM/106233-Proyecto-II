using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAI : MonoBehaviour
{
    public Rigidbody2D player;
    public Rigidbody2D paintBucket;
    public LayerMask WhatIsPlayer;
    public Transform PlayerDetectionPoint;

    //private float speed = 2.0f;
    private float maxDist = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Move()
    {
        Vector2 minPos = new Vector2(paintBucket.position.x - maxDist, transform.position.y);
        Vector2 maxPos = new Vector2(paintBucket.position.x + maxDist, transform.position.y);
        Vector2 randPos = new Vector2((Random.Range(minPos.x, maxPos.x)), Random.Range(minPos.y, maxPos.y));

    }
}
