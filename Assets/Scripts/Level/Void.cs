using UnityEngine;

public class Void : MonoBehaviour {
    


    // Unity
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject == null) return;
        if (collision.gameObject.tag != "Player") return;

        if (collision.gameObject.GetComponent<Player>().State().Equals(PLAYER_STATE.PS_DIE)) return;
        collision.gameObject.GetComponent<Player>().TakeDamage(100, DEATH_CAUSE.D_FALL, true);
    }
}
