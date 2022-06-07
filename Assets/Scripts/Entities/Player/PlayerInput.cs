using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum INPUT_SCHEME {
    I_KEYBOARD, I_GAMEPAD
}

public class PlayerInput : MonoBehaviour {

    // Observer para saber caundo se cambia de Scheme
    public static event Action<INPUT_SCHEME> OnChangeInput;

    // Player State Machine
    private Player _player;

    // Player Input
    private UnityEngine.InputSystem.PlayerInput _input;

    [SerializeField]
    private INPUT_SCHEME _currentScheme;
    public INPUT_SCHEME Scheme() { return _currentScheme; }
    public bool GamePad() { return (Scheme().Equals(INPUT_SCHEME.I_GAMEPAD)); }
    public bool Keyboard() { return (Scheme().Equals(INPUT_SCHEME.I_KEYBOARD)); }

    // Input Attributes
    private bool _left;
    private bool _right;
    private bool _jump;
    private bool _mainAction;
    private Vector2 _mousePos;
    private Vector2 _joystickDir;

    // PlayerInput.cs
    public bool Left() { return _left; }
    public bool Right() { return _right; }
    public bool Jump() { return _jump; }
    public bool MainAction() { return _mainAction; }
    public Vector2 MousePos() { return _mousePos; }
    public Vector2 Joystick() { return _joystickDir; }

    // Unity
    void Awake() {
        _player = GetComponent<Player>();
        _input = GetComponent<UnityEngine.InputSystem.PlayerInput>();

        _currentScheme = INPUT_SCHEME.I_KEYBOARD;

        _left = _right = _jump = _mainAction = false;
        _mousePos = _joystickDir = new Vector2(0.0f, 0.0f);
    }

    // Input
    void OnControlsChanged() {
        if (_input.currentControlScheme.Equals("Gamepad")) _currentScheme = INPUT_SCHEME.I_GAMEPAD;
        if (_input.currentControlScheme.Equals("Keyboard&Mouse")) _currentScheme = INPUT_SCHEME.I_KEYBOARD;
        OnChangeInput?.Invoke(Scheme());
    }

    void OnTemplates(InputValue input){
        _player.ToggleDrawing();
    }

    void OnSwapTools(InputValue input){
        _player.SwapTools();
    }

    void OnPause(InputValue input){
        bool pause = false;
        pause |= _player.StopDrawing();
        pause |= _player.StopPlacing();
        if (pause) _player.Pause();
    }

    void OnBug(){
        transform.Translate(new Vector2(0.0f, 1.0f));
        if (MainAction()) transform.Translate(new Vector2(1.0f, 0.0f));
    }

    void OnMainActiony(InputValue input){
        _mainAction = input.isPressed;
    }

    void OnMainMoving(InputValue input){
        _mousePos = input.Get<Vector2>();
    }

    void OnMainJoystickMoving(InputValue input){
        _joystickDir = input.Get<Vector2>();
    }

    void OnJumpy(InputValue input){
        _jump = input.isPressed;
    }

    void OnDashy(InputValue input){
        _player.DashTime();
    }

    void OnLefty(InputValue input){
        _left = input.isPressed;
    }

    void OnRighty(InputValue input){
        _right = input.isPressed;
    }

    void OnDowny(InputValue input){
        _player.TryFallGround();
    }

    void OnTempNext(InputValue input){
        _player.LastTemplate(); 
    }

    void OnTempLast(InputValue input){
        _player.NextTemplate();
    }

}
