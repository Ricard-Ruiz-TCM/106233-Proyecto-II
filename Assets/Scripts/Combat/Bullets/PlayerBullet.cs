using UnityEngine;

public class PlayerBullet : InkBullet {

    [SerializeField]
    private Vector2 _str;

    void Start() {
        LoadBullet("InkBullet");
    }

    protected new void Movement(){
        
    }

    public void Dir(float dir) {
        _speed = 5.0f; _str = new Vector2(50.0f, 10.0f);
        transform.localEulerAngles = new Vector2(0.0f, (dir < 0 ? 0.0f : 180.0f));
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-(transform.right.x) * _str.x * _speed, _str.y * _speed));
    }

}
