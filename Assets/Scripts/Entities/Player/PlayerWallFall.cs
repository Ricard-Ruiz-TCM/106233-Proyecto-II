using UnityEngine;

public class PlayerWallFall : PlayerState, IHaveStates {

    void Awake(){
        LoadState();
        ////////////
    }

    private void SetWallFallGravity(){
        _body.velocity = new Vector2(_body.velocity.x, _body.velocity.y / 10.0f);
        _body.gravityScale = 0.25f;
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