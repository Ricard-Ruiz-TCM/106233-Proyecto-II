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

    // Unity
    void Awake(){
        _topD = GameObject.FindObjectOfType<TopDetector>().transform;
        _bottomD = GameObject.FindObjectOfType<BottomDetector>().transform;
        _inside = false;
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (_time < 0.2f) return;
        if (_bottomD.position.y > (transform.position.y + 0.125f)) {
            if (!_inside) _collider.isTrigger = false;
        } else {
            _collider.isTrigger = true;
        }
    }

    // FadedGround.cs
    public void DisableCol(){
        _collider.isTrigger = true;
        _time = 0.0f;
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player") return;
        _inside = true;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player") return;
        _inside = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag != "Player") return;
        _inside = false;
    }

}
