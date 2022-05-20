using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class GameManager : MonoBehaviour {

    public static event Action ChangeToKeyboard;
    public static event Action ChangeToGamepad;

    private GameObject _container;

    PlayerInput _input;

    [SerializeField]
    private GameObject _inkPot;

    void OnEnable() {
        InputUser.onChange += Swap;
    }

    public void Swap(InputUser user, InputUserChange input, InputDevice device){
        
    }

    void Awake(){
        _container = GameObject.FindObjectOfType<ElementsContainer>().gameObject;
        _input = GameObject.FindObjectOfType<PlayerInput>();
    }

    private void Update() {
        
    }

    public void InstantiateInkPot(Vector2 pos) {
        Instantiate(_inkPot, pos, Quaternion.identity, _container.transform);
    }

}
