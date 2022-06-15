using UnityEngine;

public class HandShakerUI : MonoBehaviour {

    [SerializeField]
    private float _timeAlive;
    private float _time;

    public float _speed;
    public float _rack;

    void Awake(){
        _timeAlive = 1.5f;
        _time = 0.0f;
        _speed = 0.03f;
        _rack = 20.0f;

    }

    // Unity
    void Start() {
        Destroy(this.gameObject, _timeAlive); 
    }

    void Update() {
        _time += Time.deltaTime;
        transform.position += new Vector3(Mathf.Cos(_time * _rack) * _speed, -Time.deltaTime * 0.5f, 0.0f);
    }
}
