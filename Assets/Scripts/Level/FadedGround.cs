using UnityEngine;

public class FadedGround : MonoBehaviour {

    // Player Transform & Layer
    private Transform _player;
    private LayerMask _layer_Ground;

    private bool _want2Enable;

    // Components
    private CompositeCollider2D _collider;

    // Unity
    void Awake(){
        _player = GameObject.FindObjectOfType<Player>().transform;
        _layer_Ground = LayerMask.GetMask("Platform");

        _want2Enable = false;

        _collider = GetComponent<CompositeCollider2D>();
    }

    // FadedGround.cs
    public void DisableCol(){
        _collider.isTrigger = true;
    }

    public void EnableCol(){
        _want2Enable = true;
        var colliders = Physics2D.OverlapCircleAll(_player.transform.position, 0.25f, _layer_Ground);
        if (colliders.Length < 1)
        {
            _collider.isTrigger = false;
            _want2Enable = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (_want2Enable)
        {
            _collider.isTrigger = false;
            _want2Enable = false;
        }
    }

}
