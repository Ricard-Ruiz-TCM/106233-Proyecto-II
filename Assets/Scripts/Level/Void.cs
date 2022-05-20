using UnityEngine;

public class Void : MonoBehaviour {
    
    // Unity
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject == null) return;
        if (collision.gameObject.tag != "Player") return;

        collision.gameObject.GetComponent<Player>().Die(DEATH_CAUSE.D_FALL);
    }
}
