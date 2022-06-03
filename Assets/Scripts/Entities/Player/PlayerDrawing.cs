using UnityEngine;
using System.Collections.Generic;

public class PlayerDrawing : PlayerState, IHaveStates {

    // Drawing Tools
    private GameObject _brushTool;
    private GameObject _eraserTool;

    private bool _isDrawing;
    public bool IsDrawing() { return _isDrawing; }

    [SerializeField]
    private GameObject _HUDREMINDER;

    // Drawing Active Tool & Getters/Checkers
    [SerializeField]
    private GameObject _activeTool;
    private DrawElement ActiveTool(){ return _activeTool.GetComponent<DrawElement>(); }
    private bool UsingBrush(){ return (ActiveTool() is Brush); }
    private Brush Brush(){ return _activeTool.GetComponent<Brush>(); }
    private bool UsingEraser(){ return (ActiveTool() is Eraser); }
    private Eraser Eraser(){ return _activeTool.GetComponent<Eraser>(); }

    public bool CanStop() { return !Input().MainAction(); }

    // Drawing Tools Speed
    [SerializeField]
    private float _toolSpeed;
    private float ToolSpeed(){ return _toolSpeed; }

    // Strokes
    [SerializeField]
    private List<GameObject> _strokes;
    public List<GameObject> Strokes(){ return _strokes; }

    // Templates Guides
    [SerializeField]
    private List<GameObject> _templates;
    public GameObject CurrentTemplate() { return _templates[_templatesIndex]; }
    private TemplateGuide CurrentTemplateGuide(){ return CurrentTemplate().GetComponent<TemplateGuide>(); }

    // Templates Control
    private int _templatesIndex;
    private void SetTemplateIndex(int i){ _templatesIndex = i; }
    private bool _templateCompleted;
    public bool IsTemplateCompleted(){ return _templateCompleted; }

    // Templates Attributes
    [SerializeField]
    private Vector2 _templateOffset;
    private bool _checkTemplate;
    private GameObject _createdTemplate;
    public GameObject CreatedTemplate(){ return _createdTemplate; }

    // Unity
    void Awake(){
        LoadState();
        ////////////
        _brushTool = GameObject.FindObjectOfType<Brush>().gameObject;
        _eraserTool = GameObject.FindObjectOfType<Eraser>().gameObject;
        _activeTool = _brushTool;
        _toolSpeed = 0.025f;

        _strokes = new List<GameObject>();

        _templates = new List<GameObject>();
        _templatesIndex = 0;
        _templateOffset = new Vector2(-2.8f, -1.2f);
        _templateCompleted = false;
        _checkTemplate = false;
        _createdTemplate = null;
    }

    // Unity
    void Start(){
        _brushTool.GetComponent<DrawElement>().Hide();
        _eraserTool.GetComponent<DrawElement>().Hide();

        _strokes.Clear();

        _HUDREMINDER = GameObject.Find("BookHUD");

        _templates.Add(GameObject.FindObjectOfType<BoxTemplateID>().gameObject);
        _templates.Add(GameObject.FindObjectOfType<BombTemplateID>().gameObject);
        _templates.Add(GameObject.FindObjectOfType<BulbTemplateID>().gameObject);
        _templates.Add(GameObject.FindObjectOfType<KeyTemplateID>().gameObject);
        foreach (GameObject g in _templates) { g.SetActive(false); }
    }

    // PlayerDrawing.cs <Tools>
    public void PlaceTool(Vector2 position){
        ActiveTool().GetComponent<DrawElement>().SetPosition(position);
    }

    public void SwapTool(){
        ActiveTool().GetComponent<DrawElement>().Hide();
        Vector2 position = _activeTool.transform.position;

        if (UsingBrush()) _activeTool = _eraserTool;
                     else _activeTool = _brushTool;

        ActiveTool().SetPosition(position);
    }

    // PlayerDrawing.cs <Stokes>
    private void ClearStrokes(){
        foreach (GameObject go in _strokes){ Destroy(go); }
        _strokes.Clear();
    }

    private void AddStroke(GameObject stroke){
        Strokes().Add(stroke);
    }

    private void DestroyStroke(int pos){
        Destroy(_strokes[pos]);
        _strokes.RemoveAt(pos);
    }

    // PlayerDrawing.cs <Templates>   // "-1" => Next template on List    
    public void NextTemplate(int newPos = -1){ 
        if (newPos == -1) newPos = (_templatesIndex + 1) % _templates.Count;
     
        CurrentTemplate().SetActive(false);
        SetTemplateIndex(newPos);
        CurrentTemplate().SetActive(true);

        CurrentTemplate().gameObject.transform.position = (Vector2)Camera.main.transform.position + _templateOffset;

        ActiveTool().SetPosition(CurrentTemplateGuide().StartPoint());

        ClearStrokes();
    }

    public void LastTemplate(){
        int i = _templatesIndex;
        i = (i - 1) % _templates.Count; 
        if (i < 0) i = _templates.Count - 1;
        NextTemplate(i);
    }

    // IHaveStates
    public void OnEnterState(){
        EnableSystem();
        ///////////////
        _activeTool = _brushTool;
        ActiveTool().Show(0.5f);

        ClearStrokes();
        _isDrawing = true;

        GameObject.FindObjectOfType<HelperHUD>().Hide();

        _templateCompleted = false;
        _checkTemplate = false;
        _templatesIndex = 0;

        _HUDREMINDER.GetComponent<Animator>().SetBool("Show", true);

        CurrentTemplate().SetActive(true);
        ActiveTool().SetPosition(CurrentTemplateGuide().StartPoint());

        _animator.SetBool("Draw", true);
    }

    public void OnExitState(){
        _animator.SetBool("Draw", false);

        ActiveTool().MainAction(false);

        ActiveTool().Hide();
        _isDrawing = false;

        ClearStrokes();

        _HUDREMINDER.GetComponent<Animator>().SetBool("Show", false);

        _templateCompleted = false;
        CurrentTemplate().GetComponent<TemplateGuide>().Reset();
        CurrentTemplate().SetActive(false);
        ////////////////
        DisableSystem();
    }

    public void OnState(){
        if (!IsEnabled()) return;
        /////////////////////////

        if (!Input().MainAction()) ActiveTool().Show(0.5f);

        if (Input().Keyboard())
            ActiveTool().Screen2WorldPosition(Input().MousePos());

        if (Input().GamePad())
            ActiveTool().Move(Input().Joystick() * ToolSpeed());

        ActiveTool().MainAction(Input().MainAction());

        if (UsingBrush()){
            if (Brush().CanGetStroke()){
                AddStroke(Brush().GetStroke());
                _checkTemplate = true;
            }
        }

        if (UsingEraser()){
            if (Eraser().CanErase()){
                DestroyStroke(Eraser().GetStroke());
                _checkTemplate = true;
            }
        }

        CurrentTemplate().gameObject.transform.position = (Vector2)Camera.main.transform.position + _templateOffset;

        //CurrentTemplate().gameObject.transform.position = (Vector2)Camera.main.transform.position + _templateOffset;
        if (!Input().MainAction() && _checkTemplate){
            _checkTemplate = false;
            _createdTemplate = CurrentTemplate().GetComponent<TemplateGuide>().CheckTemplate();
            _templateCompleted = CurrentTemplate().GetComponent<TemplateGuide>().IsCompleted();
        }

    }

}
