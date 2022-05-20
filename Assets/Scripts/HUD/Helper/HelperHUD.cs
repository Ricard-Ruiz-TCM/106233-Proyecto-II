using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelperHUD : MonoBehaviour {

    [SerializeField]
    private Image _portrait;
    [SerializeField]
    private TextMeshProUGUI _text;

    void Awake(){
        _portrait = GetComponentInChildren<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start(){
        Hide();
    }

    public void UpdateItem(HelperItem item){
        _portrait.sprite = item.Portrait;
        _text.text = item.Text;
        Show();
    }

    private void Show(){
        GetComponent<RectTransform>().localScale = new Vector2(1.0f, 1.0f);
        Invoke("Hide", 5.0f);
    }

    private void Hide(){
        GetComponent<RectTransform>().localScale = new Vector2(0.0f, 0.0f);
    }

}
