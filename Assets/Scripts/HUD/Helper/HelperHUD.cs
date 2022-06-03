using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelperHUD : MonoBehaviour {

    [SerializeField]
    private Image _portrait;
    [SerializeField]
    private TextMeshProUGUI _text;

    HelperItem _item;

    void Awake(){
        _portrait = GetComponentInChildren<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    void Start(){
        Hide();
    }

    private void Update() {
        
    }

    public void UpdateItem(HelperItem item){
        _item = item;
        //_portrait.sprite = _item.Portrait;
        _text.text = _item.Text;
        Show();
    }

    private void Show(){
        GetComponent<Animator>().SetBool("Show", true);
        Invoke("Hide", _item.Duration);
    }

    public void Hide(){
        GetComponent<Animator>().SetBool("Show", false);
    }

}
