using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RightDetector : MonoBehaviour {

    private float _checkRadius;

    [SerializeField]
    LayerMask _multipleRightLayer;

    [SerializeField]
    LayerMask _layer_WallFall;

    [SerializeField]
    LayerMask _checkPointLayer;

    public static event Action<bool> CanMoveRight;
    public static event Action OnCheckPoint;

    public static event Action<bool> OnWallWhileFalling;
    public static event Action<bool> onFacingWall;

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _checkRadius);
    }

    void Start(){
        _checkRadius = 0.2f;

        _layer_WallFall = LayerMask.GetMask("WallFall");
    }

    void FixedUpdate() {
        CheckRight();
        CheckCheckPoint();
        CheckRightWall();
        CheckFacingWall();
    }

    private void CheckRight() {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _checkRadius, _multipleRightLayer);
        CanMoveRight?.Invoke(!(colliders.Length > 0));
    }

    private void CheckCheckPoint() {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _checkRadius, _checkPointLayer);
        if (colliders.Length > 0) OnCheckPoint?.Invoke();
    }

    private void CheckRightWall() {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _checkRadius * 2.0f, _layer_WallFall);
        OnWallWhileFalling?.Invoke(colliders.Length > 0);
    }

    private void CheckFacingWall(){
        var colliders = Physics2D.OverlapCircleAll(transform.position, _checkRadius * 0.9f, _layer_WallFall);
        onFacingWall?.Invoke(colliders.Length > 0);
    }
    

}
