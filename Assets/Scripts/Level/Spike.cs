using UnityEngine;

public class Spike : MonoBehaviour {

    [SerializeField]
    private Attack _spike;

    // Unity
    void Awake(){
        _spike = Resources.Load<Attack>("ScriptableObjects/SpikeAttack");
    }

    // Unity
    void OnCollisionEnter2D(Collision2D collision){
        if (collision == null) return;
        if (collision.gameObject.tag != "Player") return;

        collision.gameObject.GetComponent<Player>().Die(DEATH_CAUSE.D_FALL);
    }

}
