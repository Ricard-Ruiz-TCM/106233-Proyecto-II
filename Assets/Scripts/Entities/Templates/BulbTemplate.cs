using UnityEngine;
using UnityEngine.InputSystem;

public class BulbTemplate : Template {
    
    private void Start() {
        load();
    }    

    public override void MainAction(bool action) {
        MusicPlayer.Instance.PlayFX("Light_Place/Light_Place_" + ((int)Random.Range(1, 3)).ToString(), 0.5f);
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

}
