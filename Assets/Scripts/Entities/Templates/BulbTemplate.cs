using UnityEngine;
using UnityEngine.InputSystem;

public class BulbTemplate : Template {
    
    private void Start() {
        load();
    }    

    public override void MainAction(bool action) {
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

}
