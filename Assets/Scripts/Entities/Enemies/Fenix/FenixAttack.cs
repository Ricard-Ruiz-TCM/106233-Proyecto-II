using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenixAttack : MonoBehaviour
{
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsVisible;
    public float DetectionRange;
    public float VisionAngle;
    public float FOV = 90f;

    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool IsInRange()
    {
        float dist = Vector2.Distance(player.position, transform.position);
        return dist < DetectionRange;
    }

    private float GetAngle()
    {
        Vector2 v1 = transform.right;
        Vector2 v2 = player.position - transform.position;
        return Vector2.Angle(v1, v2);
    }

    private bool IsInVisionAngle()
    {
        float angle = GetAngle();
        return FOV >= 2 * angle;
    }
}
