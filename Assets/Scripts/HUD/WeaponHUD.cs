using UnityEngine;
using UnityEngine.UI;

public class WeaponHUD : MonoBehaviour {

    private Image _image;

    // Sprites
    private Sprite _melee;
    private Sprite _ranged;

    void OnEnable(){
        Player.OnChangeWeapon += Show;
    }

    void OnDisable(){
        Player.OnChangeWeapon -= Show;
    }

    void Awake(){
        _image = GetComponent<Image>();

        _melee = Resources.LoadAll<Sprite>("Sprites/HUD/sprite_sheet_UI (1)")[17];
        _ranged = Resources.LoadAll<Sprite>("Sprites/HUD/sprite_sheet_UI (1)")[18];
    }

    void Start(){
        Show(COMBAT_STATE.C_MELEE);
    }

    public void Show(COMBAT_STATE state) {
        if (state == COMBAT_STATE.C_MELEE) _image.sprite = _melee;
        if (state == COMBAT_STATE.C_RANGED) _image.sprite = _ranged;
    }

}
