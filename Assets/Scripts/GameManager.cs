using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class GameManager : MonoBehaviour {

    private GameObject _container;

    [SerializeField]
    private GameObject _inkPot;


    void Awake(){
        _container = GameObject.FindObjectOfType<ElementsContainer>().gameObject;
    }

    private void Update() {
        
    }

    public void InstantiateInkPot(Vector2 pos) {
        Instantiate(_inkPot, pos, Quaternion.identity, _container.transform);
    }

}
