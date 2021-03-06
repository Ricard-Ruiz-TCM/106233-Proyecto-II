using UnityEngine;

public class Spike : MonoBehaviour {

    [SerializeField]
    private Attack _spike;

    // Unity
    void Awake(){
        _spike = Resources.Load<Attack>("ScriptableObjects/Attacks/SpikeAttack");
    }

    // Unity
    void OnCollisionEnter2D(Collision2D collision){
        if (collision == null) return;
        if (collision.gameObject.tag != "Player") return;
        if (collision.gameObject.GetComponent<Player>().State().Equals(PLAYER_STATE.PS_DIE)) return;
        collision.gameObject.GetComponent<Player>().TakeDamage(100, DEATH_CAUSE.D_FALL, true);
        MusicPlayer.Instance.PlayFX("Player_death_Pinchos_1",1.0f);

    }

}
