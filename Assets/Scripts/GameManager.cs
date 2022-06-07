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

    public int ProgressionTemplate = 0;

    public int REAL_PROGRESSION = 0;

    public GameObject LVL1;
    public GameObject LVL2;

    [SerializeField]
    private GameObject _inkPot;

    [SerializeField]
    private List<GameObject> _templateButtons;

    void Start(){
        _container = GameObject.FindObjectOfType<ElementsContainer>().gameObject;
        _inkPot = Resources.Load<GameObject>("Prefabs/Ink");
        //LVL2.SetActive(false);
        MusicPlayer.Instance.PlayMusic("forest");

        foreach (GameObject go in _templateButtons) go.SetActive(false);
    }

    public void InstantiateInkPot(Vector2 pos) {
        Instantiate(_inkPot, pos, Quaternion.identity, _container.transform);
    }

    public void GOTOLVL2()
    {
        //LVL1.SetActive(false);
        //LVL2.SetActive(true);
        MusicPlayer.Instance.PlayMusic("cave");
        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy")) {
            Destroy(enemies);
        }
    }

    public void SetNewProgression (int templateID) {
        _templateButtons[REAL_PROGRESSION].SetActive(true);
        ProgressionTemplate = templateID;
        REAL_PROGRESSION += 1;
    }

    public void ResetProg() {
        ProgressionTemplate = 0;
    }


}
