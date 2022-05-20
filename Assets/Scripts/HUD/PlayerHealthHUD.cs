using UnityEngine;

public class PlayerHealthHUD : BarHUDElement {

    private Player _player;

    void OnEnable(){
        Player.OnHealthChange += UpdateBar;
    }

    void OnDisable(){
        Player.OnHealthChange -= UpdateBar;
    }

    void Start(){
        _player = GameObject.FindObjectOfType<Player>();
    }

    void UpdateBar(){
        fillBar(_player.Health(), _player.MaxHealth());
    }

}
