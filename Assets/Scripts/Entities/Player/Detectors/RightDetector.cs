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

    public static event Action<bool> OnSafeBoost;

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, _checkRadius);
    }

    void Start(){
        _checkRadius = 0.15f;

        _layer_WallFall = LayerMask.GetMask("WallFall");
    }

    void FixedUpdate() {
        CheckRight();
        CheckCheckPoint();
        CheckRightWall();
        CheckFacingWall();
        CheckSafeBoost();
    }

    private void CheckRight() {
        var colliders = Physics2D.OverlapCircleAll(transform.position, _checkRadius, _multipleRightLayer);
        CanMoveRight?.Invoke(!(colliders.Length > 0));
    }

    private void CheckSafeBoost() {
        Vector2 pos = transform.position; pos.y -= 0.125f;
        var colliders = Physics2D.OverlapCircleAll(pos, _checkRadius, _multipleRightLayer);
        OnSafeBoost?.Invoke(!(colliders.Length > 0));
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
