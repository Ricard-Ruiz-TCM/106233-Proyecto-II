using UnityEngine;

public class FadedGround : MonoBehaviour {

    // Player Transform & Layer
    private Transform _player;
    private LayerMask _layer_Player;

    // Components
    private CompositeCollider2D _collider;

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(_player.transform.position, 0.35f);
    }

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
        _layer_Player = LayerMask.GetMask("Player");

        _collider = GetComponent<CompositeCollider2D>();
    }

    // FadedGround.cs
    public void DisableCol(){
        _collider.isTrigger = true;
    }

    public void EnableCol(){
        var colliders = Physics2D.OverlapCircleAll(_player.transform.position, 0.35f, _layer_Player);
        if (colliders.Length <= 0) _collider.isTrigger = false;
    }

}
