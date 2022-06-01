using UnityEngine;

public class HandAttack : MonoBehaviour {

    [SerializeField]
    private Attack _attack;
   
    private void Start() {
        // Cargamos el ataque para tener el daño
    }

    void Update(){
        // Hacemos crecer el puño :3 o lo elevamos
    }

    protected void OnCollisionEnter2D(Collision2D collision) {

    }

}
