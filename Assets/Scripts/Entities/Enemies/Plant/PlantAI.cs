using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAI : EnemyMovement
{
    public GameObject player;
    public LayerMask WhatIsPlayer;
    public Transform EdgeDetectionPoint;
    public float DetectionDistance;

    public bool Detected => detected;

    private bool detected;

    private void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(transform.position, DetectionDistance);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>().gameObject;
        detected = false;
        DetectionDistance = 4.5f;
    }

    // Update is called once per frame
    void Update()
    {

        detected = PlayerDetection();

        if (!GetComponent<PlantAttack>().Dying) ApplyRotation();

    }


    private void ApplyRotation() {
        Vector2 dir = (transform.position - player.gameObject.transform.position);
        dir.Normalize();
        transform.localEulerAngles = new Vector3(0.0f, -180.0f * Mathf.Ceil(-dir.x), 0.0f);
    }

    private bool PlayerDetection() {
        return (Vector2.Distance(transform.position, player.gameObject.transform.position) < DetectionDistance);
    }

}
