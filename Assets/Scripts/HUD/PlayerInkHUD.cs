using UnityEngine;

public class PlayerInkHUD : BarHUDElement {

    private Player _player;

    void OnEnable(){
        Player.OnInkChange += UpdateBar;
    }

    void OnDisable(){
        Player.OnInkChange -= UpdateBar;
    }

    void Start(){
        _player = GameObject.FindObjectOfType<Player>();
    }

    void UpdateBar(){
        fillBar(_player.Ink(), _player.MaxInk());
    }

}
