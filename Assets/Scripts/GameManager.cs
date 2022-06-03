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

    public GameObject LVL1;
    public GameObject LVL2;

    [SerializeField]
    private GameObject _inkPot;

    void Start(){
        _container = GameObject.FindObjectOfType<ElementsContainer>().gameObject;
        _inkPot = Resources.Load<GameObject>("Prefabs/Ink");
        LVL2.SetActive(false);
    }

    public void InstantiateInkPot(Vector2 pos) {
        Instantiate(_inkPot, pos, Quaternion.identity, _container.transform);
    }

    public void GOTOLVL2()
    {
        LVL1.SetActive(false);
        LVL2.SetActive(true);
    }


}
