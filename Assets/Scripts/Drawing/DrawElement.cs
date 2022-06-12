using UnityEngine;

public abstract class DrawElement : MonoBehaviour {

    protected Camera _camera;
    protected Vector2 _position;

    [SerializeField]
    protected Vector2 _offset;
    public Vector2 GetOffset() { return _offset; }

    public abstract void MainAction(bool action);

    public void Screen2WorldPosition(Vector2 pos) {
        _position = _camera.ScreenToWorldPoint(pos); Place();
    }

    public void Move(Vector2 dir) {
        _position += dir * Time.deltaTime; Place();
    }

    public void SetPosition(Vector2 position) {
        _position = position; Place();
    }

    public void Place() {
        transform.position = _position + _offset;
    }

    public void Show(float alpha = 1.0f) {
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }

    public void Hide() {
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

}
