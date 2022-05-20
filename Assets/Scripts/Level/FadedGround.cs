using UnityEngine;

public class FadedGround : MonoBehaviour {

    // Player Transform & Layer
    private Transform _player;
    private LayerMask _layer_Ground;

    // Components
    private CompositeCollider2D _collider;

    // Unity
    void OnEnable(){
        PlayerJump.OnJump += DisableCol;
        PlayerFall.OnFalling += EnableCol;
    }

    // Unity
    void OnDisable(){
        PlayerJump.OnJump -= DisableCol;
        PlayerFall.OnFalling -= EnableCol;
    }

    // Unity
    void Awake(){
        _player = GameObject.FindObjectOfType<Player>().transform;
        _layer_Ground = LayerMask.GetMask("Platform");

        _collider = GetComponent<CompositeCollider2D>();
    }

    // FadedGround.cs
    public void DisableCol(){
        _collider.isTrigger = true;
    }

    public void EnableCol(){
        var colliders = Physics2D.OverlapCircleAll(_player.transform.position, 0.25f, _layer_Ground);
        if (colliders.Length < 1) _collider.isTrigger = false;
    }

}
