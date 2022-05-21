using UnityEngine;

public class InputReminderUI : MonoBehaviour {

    [SerializeField]
    private Sprite _gamepad;
    [SerializeField]
    private Sprite _keyboard;

    // Sprite
    private SpriteRenderer _image;

    // Input System
    private PlayerInput _input;

    // Unity
    void OnEnable(){
        PlayerInput.OnChangeInput += ChangeInputReminder;
    }

    // Unity
    void Awake(){
        _image = GetComponent<SpriteRenderer>();
        _input = GameObject.FindObjectOfType<PlayerInput>();
    }

    // Unity
    void Start(){
        ChangeInputReminder(_input.Scheme());
    }

    // InputReminderUI.CS <swap>
    public void ChangeInputReminder(INPUT_SCHEME scheme){
        if (scheme == INPUT_SCHEME.I_KEYBOARD) _image.sprite = _keyboard;
        if (scheme == INPUT_SCHEME.I_GAMEPAD) _image.sprite = _gamepad;
    }

}
