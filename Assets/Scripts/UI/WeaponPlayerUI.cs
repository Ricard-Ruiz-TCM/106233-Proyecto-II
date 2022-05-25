using UnityEngine;
using UnityEngine.UI;

public class WeaponPlayerUI : MonoBehaviour {

    [SerializeField]
    private Sprite _brush;
    [SerializeField]
    private Sprite _eraser;

    // Sprite
    private SpriteRenderer _sprite;

    // Input System

    // Unity
    void OnEnable(){
        Player.OnChangeWeapon += ChangeWeapon;
    }

    // Unity
    void Awake(){
        _sprite = GetComponent<SpriteRenderer>();
        _brush = Resources.Load<Sprite>("Sprites/pincel2");
        _eraser = Resources.Load<Sprite>("Sprites/goma2");
    }

    // Unity
    void Start(){
        ChangeWeapon(COMBAT_STATE.C_MELEE);
    }

    void ChangeWeapon(COMBAT_STATE state){
        if (state.Equals(COMBAT_STATE.C_MELEE)) _sprite.sprite = _eraser;
        if (state.Equals(COMBAT_STATE.C_RANGED)) _sprite.sprite = _brush;
    }


}
