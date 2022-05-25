using UnityEngine;
using UnityEngine.UI;

public class WeaponPlayerUI : MonoBehaviour {

    [SerializeField]
    private Sprite _brush;
    [SerializeField]
    private Sprite _eraser;

    // Sprite
    private SpriteRenderer _sprite;

    // Unity
    void OnEnable(){
        Player.OnChangeWeapon += ChangeWeapon;
        PlayerCombat.OnAttack += Hide;
        PlayerCombat.OnEndAttack += Show;
    }

    // Unity
    void OnDisable() {
        Player.OnChangeWeapon -= ChangeWeapon;
        PlayerCombat.OnAttack -= Hide;
        PlayerCombat.OnEndAttack -= Show;
    }

    // Unity
    void Awake(){
        _sprite = GetComponent<SpriteRenderer>();
        _brush = Resources.LoadAll<Sprite>("Sprites/armas")[0];
        _eraser = Resources.LoadAll<Sprite>("Sprites/armas")[1];
    }

    // Unity
    void Start(){
        ChangeWeapon(COMBAT_STATE.C_MELEE);
    }

    void ChangeWeapon(COMBAT_STATE state){
        if (state.Equals(COMBAT_STATE.C_MELEE)) _sprite.sprite = _eraser;
        if (state.Equals(COMBAT_STATE.C_RANGED)) _sprite.sprite = _brush;
    }

    public void Hide(){
        _sprite.color -= new Color(0.0f, 0.0f, 0.0f, 1.0f);
    }

    public void Show(){
        _sprite.color += new Color(0.0f, 0.0f, 0.0f, 1.0f);
    }


}
