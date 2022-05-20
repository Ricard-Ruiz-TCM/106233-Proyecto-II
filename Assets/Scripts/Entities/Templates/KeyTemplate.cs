using UnityEngine;
using UnityEngine.InputSystem;

public class KeyTemplate : Template {

    private GameObject _door;

    private void Start() {
        load();
        HighlightRed();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject != null) {
            if (other.gameObject.tag == "Door") {
                _door = other.gameObject;
                HighlightGreen();
            } else {
                HighlightRed();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject != null) {
            if (other.gameObject.tag == "Door") {
                _door = other.gameObject;
                HighlightGreen();
            } else {
                HighlightRed();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        HighlightRed();
    }

    public override void MainAction(bool action) {
        _door.GetComponent<Door>().OpenDoor();
        Destroy(this.gameObject);
    }

}
