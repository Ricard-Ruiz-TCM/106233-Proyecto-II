using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenixAttack : MonoBehaviour
{
    [SerializeField]
    public Attack currentAttack;
    private PlayerCombat _playerCombat;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsVisible;
    public float DetectionRange;
    public float VisionAngle;
    public float FOV = 90f;

    private float currentTime;
    private float maxTime = 2f;
    private Transform player;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, DetectionRange);

        Gizmos.color = Color.red;
        var direction = Quaternion.AngleAxis(VisionAngle/2, transform.forward)
            * -transform.up;
        Gizmos.DrawRay(transform.position, direction * DetectionRange);
        var direction2 = Quaternion.AngleAxis(-VisionAngle/2 , transform.forward)
            * -transform.up;
        Gizmos.DrawRay(transform.position, direction2 * DetectionRange);

        Gizmos.color = Color.white;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= maxTime)
        {
            currentTime = 0;

        }
    }

    bool IsInRange()
    {
        float dist = Vector2.Distance(player.position, transform.position);
        return dist < DetectionRange;
    }

    private float GetAngle()
    {
        Vector2 v1 = -transform.up;
        Vector2 v2 = player.position - transform.position;
        return Vector2.Angle(v1, v2);
    }

    private bool IsInVisionAngle()
    {
        float angle = GetAngle();
        return FOV >= 2 * angle;
    }

}
