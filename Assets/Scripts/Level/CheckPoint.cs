using UnityEngine;

public class CheckPoint : MonoBehaviour {

    private bool SpawnParticles = false;
    // Unity
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == null) return;
        if (collision.gameObject.tag != "Player") return;

        if(SpawnParticles==false)
        {
            ParticleInstancer.Instance.StartSpecialParticles("Fountain_Particle", transform);
            SpawnParticles = true;
            MusicPlayer.Instance.PlayFX("Player_checkpoint/Player_checkpoint", 0.5f);
        }
        collision.gameObject.GetComponent<Player>().SaveCheckPoint(transform.position);
    }

}
