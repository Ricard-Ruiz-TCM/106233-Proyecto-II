using UnityEngine;
using UnityEngine.InputSystem;

public enum INPUT_SCHEME {
    I_KEYBOARD, I_GAMEPAD
}

public class PlayerInput : MonoBehaviour {

    // Player State Machine
    private Player _player;

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
    }

    void OnTemplates(InputValue input){
        _player.ToggleDrawing();
    }

    void OnSwapTools(InputValue input){
        _player.SwapTools();
    }

    void OnPause(InputValue input){
        transform.Translate(new Vector2(0.0f, 1.0f));
        if (MainAction()) transform.Translate(new Vector2(1.0f, 0.0f));
    }

    void OnMainActiony(InputValue input){
        _mainAction = input.isPressed;
    }

    void OnMainDrawingActiony(InputValue input){
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
        _player.ToggleDash();
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

}
