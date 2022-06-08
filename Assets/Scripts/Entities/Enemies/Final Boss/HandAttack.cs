using UnityEngine;

public class HandAttack : MonoBehaviour {

    [SerializeField]
    private Attack _attack;

    private bool _move;

    private GameObject _player;

    private void Start() {
        _move = false;

        Invoke("EnableMove", 1.2f);

        Destroy(this.gameObject, 2.5f);
    }

    private void OnDestroy() {
        transform.parent.position = new Vector3(-1000.0f, 1000.0f ,1000.0f);
    }

    public void EnableMove(){
        GetComponent<Animator>().SetBool("Hit", true);
    }

    public void MakeDamage(){
        _player.GetComponent<PlayerCombat>().TakeDamage(_attack);
        _player.GetComponent<PlayerMovement>().ExtraUpPush();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == null) return;
        if (collision.gameObject.tag == "Player")
        {
            _player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject == null) return;
        if (collision.gameObject.tag == "Player")
        {
            _player = null;
        }
    }


}
