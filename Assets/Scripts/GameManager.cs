using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    /////////////////////////////////////////////////////////////////////
    // Singleton Instance
    public static GameManager Instance { get; private set; }
    void Awake(){
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    /////////////////////////////////////////////////////////////////////

    private GameObject _container;

    [SerializeField]
    private GameObject _inkPot;

    void Start(){
        _container = GameObject.FindObjectOfType<ElementsContainer>().gameObject;
        _inkPot = Resources.Load<GameObject>("Prefabs/Ink");
    }

    public void InstantiateInkPot(Vector2 pos) {
        Instantiate(_inkPot, pos, Quaternion.identity, _container.transform);
    }

}
