using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BottomDetector : MonoBehaviour {

    private float _checkRadius;

    [SerializeField]
    LayerMask _groundLayer;

    [SerializeField]
    LayerMask _platofrmLayer;

    public static event Action<bool> OnGround;
    public static event Action<GameObject> OnPlatform;

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _checkRadius);
    }

    void Start(){
        _checkRadius = 0.1f;
    }

    void FixedUpdate() {
        CheckGround();
        CheckPlatform();
    }

    void CheckGround() {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _checkRadius, _groundLayer);
        OnGround?.Invoke(colliders.Length > 0);
    }



    private void CheckPlatform() {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _checkRadius, _platofrmLayer);
        if (colliders.Length > 0) OnPlatform?.Invoke(colliders[0].gameObject);
    }

}

