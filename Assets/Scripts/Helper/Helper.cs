using UnityEngine;

public class Helper : MonoBehaviour {
    
    // Scritpable Object con la info
    [SerializeField]
    private HelperItem _item;

    // HUD
    private HelperHUD _hud;

    void Awake(){
        _hud = GameObject.FindObjectOfType<HelperHUD>();
    }

    // Unity
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject == null) return;
        if (collision.gameObject.tag != "Player") return;
        _hud.UpdateItem(_item);
        if (GetComponent<newTemplateUI>() != null) GetComponent<newTemplateUI>().Show();
        if (_item.ID != -1) GameManager.Instance.SetNewProgression(_item.ID);
        MusicPlayer.Instance.PlayFX("Profesor_Mumble", 1.0f);
        Destroy(this.gameObject);
    }

}
