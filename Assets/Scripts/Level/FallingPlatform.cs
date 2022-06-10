using UnityEngine;

public class FallingPlatform : MonoBehaviour {

    // Fall & Fade Control
    private bool _fall;
    [SerializeField]
    private float _fallTime;
    private bool IsFalling() { return _fall; }
    private bool _fade;
    [SerializeField]
    private float _fadeTime;
    private bool IsFaded() { return _fade; }
    private bool CanFade() { return !IsFaded(); }

    // InitPos
    private Vector2 _initialPos;

    private void OnEnable() {
        Player.OnRespawn += Restore;
    }

    private void OnDisable() {
        Player.OnRespawn -= Restore;
    }

    // Unity
    void Awake(){
        _fall = false;
        _fade = false;
        _initialPos = this.transform.position;
    }

    // Unity
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision == null) return;
        if (collision.gameObject.tag != "Player") return;

        if (CanFade()) Fade();
    }

    // FallingPlatform.cs
    private void Fade() {
        if (IsFaded()) return;
        _fade = true;
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        var fo = ps.forceOverLifetime;
        fo.y = 1.0f;
        Invoke("Fall", _fadeTime);
    }

    private void Fall() {
        if (IsFalling()) return;
        _fall = true;
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<BoxCollider2D>().isTrigger = true;
        MusicPlayer.Instance.PlayFX("Books_Falling/Books_Falling_" + ((int)Random.Range(1, 3)).ToString(), 0.5f);
        Invoke("Restore", _fallTime);
    }

    private void Restore(){
        _fall = false;
        _fade = false;
        GetComponent<BoxCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.5f);
        ParticleSystem ps = GetComponentInChildren<ParticleSystem>();
        var fo = ps.forceOverLifetime;
        fo.y = 0.0f;
        transform.position = _initialPos;
    }

}
