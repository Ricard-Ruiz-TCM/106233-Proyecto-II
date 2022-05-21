using UnityEngine;

public class PlayerWallFall : PlayerState, IHaveStates {

    [SerializeField]
    private float _gravity;

    void Awake(){
        LoadState();
        ////////////

        _gravity = 0.2f;
    }

    private void SetWallFallGravity(){
        _body.velocity = new Vector2(_body.velocity.x, _body.velocity.y / 10.0f);
        _body.gravityScale = _gravity;
    }

    // IHaveStates
    public void OnEnterState(){
        EnableSystem();
        ///////////////
        SetWallFallGravity();
        _animator.SetBool("FallWall", true);
    }

public void OnExitState(){
        _animator.SetBool("FallWall", false);
        ////////////////
        DisableSystem();
    }

    public void OnState(){
        if (!IsEnabled()) return;
        //////////////////////////
    }

}