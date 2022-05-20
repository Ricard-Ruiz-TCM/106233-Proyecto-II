using UnityEngine;

public class Door : MonoBehaviour {

    // Door.cs
    public void OpenDoor() {
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

}
