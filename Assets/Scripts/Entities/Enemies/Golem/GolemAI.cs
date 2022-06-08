using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAI : MonoBehaviour {

    private GameObject player;
    private float _time;

    private void OnEnable()
    {
        Fader.CanBossRespawn += () => { Destroy(this.gameObject); };
    }

    void Start() {
        player = GameObject.FindObjectOfType<Player>().gameObject;
        _time = 5.0f;
    }

    void Update() {
        if (!GetComponent<GolemAttack>().Dying) FacePlayer();
        _time -= Time.deltaTime;
        if (_time <= 0.0f)
        {
            Invoke("Attack", 0.55f);
            GetComponent<Animator>().SetBool("Attack", true);
            Invoke("StopAnim", 1.5f);
            _time = 5.0f;
        }
    }

    private void StopAnim()
    {
        GetComponent<Animator>().SetBool("Attack", false);
    }


    private void Attack(){
        GetComponent<GolemAttack>().Attack();
    }

    public void FacePlayer() {
        Vector2 dir = (transform.position - player.transform.position);
        dir.Normalize();
        transform.localEulerAngles = new Vector3(0.0f, -180.0f * Mathf.Ceil(dir.x), 0.0f);
    }

}
