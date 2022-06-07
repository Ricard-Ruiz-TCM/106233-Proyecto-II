using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUGING : MonoBehaviour {

    GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindObjectOfType<Player>().gameObject;
    }

    public void ToCubeTemplate() {
        _player.transform.position = new Vector2(90.6f, -18.8f);
    }

    public void ToBombTemplate() {
        _player.transform.position = new Vector2(175.7f, -18.5f);
    }

    public void ToLightTemplate() {
        _player.transform.position = new Vector2(-4.6f, -64.8f);
    }

    public void ToCave() {
        _player.transform.position = new Vector2(240.7f, -17.2f);
    }

    public void ToForest() {
        _player.transform.position = new Vector2(-15.3f, -18.8f);
    }

    public void Respawn() {
        _player.GetComponent<Player>().Respawn();
    }
    
    public void FixtLight()
    {
        GameObject.FindObjectOfType<ShadowIntensity>().Fix();
    }

    public void FullINK(){
        _player.GetComponent<Player>().FillInk();
    }

    public void FullHP(){
        _player.GetComponent<Player>().FillHealth();
    }

}
