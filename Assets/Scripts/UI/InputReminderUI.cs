using UnityEngine;
using UnityEngine.UI;

public class InputReminderUI : MonoBehaviour {

    [SerializeField]
    private Sprite _gamepad;
    [SerializeField]
    private Sprite _keyboard;

    // Sprite
    private SpriteRenderer _sprite;
    private Image _image;

    // Input System
    private PlayerInput _input;

    // Unity
    void OnEnable(){
        PlayerInput.OnChangeInput += ChangeInputReminder;
    }

    // Unity
    void Awake(){
        _sprite = GetComponent<SpriteRenderer>();
        _image = GetComponent<Image>();
        _input = GameObject.FindObjectOfType<PlayerInput>();
    }

    // Unity
    void Start(){
        ChangeInputReminder(_input.Scheme());
    }

    // InputReminderUI.CS <swap>
    public void ChangeInputReminder(INPUT_SCHEME scheme){
        if (_image == null) {
            if (scheme == INPUT_SCHEME.I_KEYBOARD)  _sprite.sprite = _keyboard;
            if (scheme == INPUT_SCHEME.I_GAMEPAD) _sprite.sprite = _gamepad;
        } else {
            if (scheme == INPUT_SCHEME.I_KEYBOARD)  _image.sprite = _keyboard;
            if (scheme == INPUT_SCHEME.I_GAMEPAD) _image.sprite = _gamepad;
        }
    }

}
