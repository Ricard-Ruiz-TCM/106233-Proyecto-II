using UnityEngine;

public class FadedGround : MonoBehaviour {

    // Player Transform & Layer
    private Transform _topD;
    private Transform _bottomD;

    [SerializeField]
    private bool _inside;

    // Components
    private BoxCollider2D _collider;

    private float _time;

    private float _off;

    // Unity
    void Awake(){
        _topD = GameObject.FindObjectOfType<TopDetector>().transform;
        _bottomD = GameObject.FindObjectOfType<BottomDetector>().transform;
        _inside = false;
        _collider = GetComponent<BoxCollider2D>();

        _off = 0.15f;
    }

    private void FixedUpdate()
    {
        _time += Time.deltaTime;
        if (_time < 0.2f) return;

        // bottom arriba
        if (_bottomD.position.y - _off > (transform.position.y + _off)) {
            // top arriba
            if (_topD.position.y > (transform.position.y + _off)) {
                _collider.isTrigger = false;
            }
        }
        // bottom abajo
        if (_bottomD.position.y - _off < (transform.position.y - _off)) {
            // top arriba
            if (_topD.position.y > (transform.position.y + _off)) {
                _collider.isTrigger = true;
            }
            // top abajo
            if (_topD.position.y < (transform.position.y - _off)) {
                _collider.isTrigger = true;
            }
        }
    }

    // FadedGround.cs
    public void DisableCol(){
        _collider.isTrigger = true;
        _time = 0.0f;
    }

}
