using UnityEngine;

public class HandAttack : MonoBehaviour {

    [SerializeField]
    private Attack _attack;

    private bool _move;
   
    private void Start() {
        _move = false;

        Invoke("EnableMove", 0.5f);

        Destroy(this.gameObject, 2.0f);
    }

    private void OnDestroy() {
        transform.parent.position = new Vector3(-1000.0f, 1000.0f ,1000.0f);
    }

    public void EnableMove(){
        GetComponent<Animator>().SetBool("Hit", true);
    }


    protected void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider == null) return;
        if (collision.collider.gameObject.tag == "Player"){
            Debug.Log("DAÑO AL PLAYER JEJE");
        }
    }

}
