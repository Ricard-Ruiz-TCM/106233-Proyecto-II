using UnityEngine;

public class PlayerState : MonoBehaviour, ISystem {

    //ISystem
    protected bool _enable;

    // Input
    private PlayerInput _input;
    protected PlayerInput Input() { return _input; }

    // Animator
    protected Rigidbody2D _body;
    protected Animator _animator;

    protected void LoadState(){
        _enable = false;
        // Components
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        // Input
        _input = GetComponent<PlayerInput>();
    }

    // ISystem
    public void ToggleSystem() { _enable = !_enable; }
    public void DisableSystem(){ _enable = false; }
    public void EnableSystem(){ _enable = true; }
    public bool IsEnabled(){ return _enable; }

}
