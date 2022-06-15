using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    /////////////////////////////////////////////////////////////////////
    // Singleton Instance
    public static GameManager Instance { get; private set; }
    /////////////////////////////////////////////////////////////////////

    private GameObject _container;

    public int HighlightedTemplate = 0;

    public int REAL_PROGRESSION = 0;

    private GameObject LVL1;
    private GameObject LVL2;

    [SerializeField]
    private GameObject _inkPot;

    private GameObject _fader;

    private List<GameObject> _templateButtons;

    private void OnEnable()
    {
        Player.OnRespawn += RemoveInks;
    }

    private void OnDisable()
    {
        Player.OnRespawn += RemoveInks;
    }

    public void RemoveInks()
    {
        foreach (InkPot go in GameObject.FindObjectsOfType<InkPot>()) Destroy(go.gameObject);
    }

    public void Awake(){
        Instance = this;
        _container = GameObject.FindObjectOfType<ElementsContainer>().gameObject;
        _inkPot = Resources.Load<GameObject>("Prefabs/Ink");
        

        _fader = GameObject.FindObjectOfType<Fader>().gameObject;

        LVL1 = GameObject.FindObjectOfType<FORESTID>().gameObject;
        LVL2 = GameObject.FindObjectOfType<CAVEID>().gameObject;

        LVL2.SetActive(false);
        _templateButtons = new List<GameObject>();
        _templateButtons.Add(GameObject.FindObjectOfType<BOXTEMPLATEBUTTONID>().gameObject);
        _templateButtons.Add(GameObject.FindObjectOfType<BOMBTEMPLATEBUTTONID>().gameObject);
        _templateButtons.Add(GameObject.FindObjectOfType<BULBTEMPLATEBUTTONID>().gameObject);

        foreach (GameObject go in _templateButtons) go.SetActive(false);
    }

    void Start(){
        MusicPlayer.Instance.PlayMusic("Musica_bosque_V2/Musica_bosque", 1f, true);
    }

    public void InstantiateInkPot(Vector2 pos) {
        Instantiate(_inkPot, pos, Quaternion.identity, _container.transform);
    }

    public void GOTOLVL2(){
        LVL1.SetActive(false);
        LVL2.SetActive(true);
        MusicPlayer.Instance.PlayMusic("Musica_Cueva/Musica_Cueva", 1f, true);
        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy")) {
            Destroy(enemies);
        }
    }

    public void GOTOLVL1(){
        LVL1.SetActive(true);
        LVL2.SetActive(false);
        MusicPlayer.Instance.PlayMusic("Musica_bosque_V2/Musica_bosque", 1f, true);
        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemies);
        }
        foreach (GameObject enemies in GameObject.FindGameObjectsWithTag("Boss"))
        {
            Destroy(enemies);
        }
    }


    public void SetNewProgression (int templateID) {
        _templateButtons[REAL_PROGRESSION].SetActive(true);
        HighlightedTemplate = templateID;
        REAL_PROGRESSION += 1;
    }

    public void ResetProg() {
        HighlightedTemplate = 0;
    }


    public void Fade()
    {
        if (_fader.activeSelf) return;
        _fader.SetActive(true);
        _fader.GetComponent<Fader>().StartAlpha();
    }

}
