using UnityEngine;

public class Brush : DrawElement {

    [SerializeField]
    private bool _drawing;
    private bool IsDrawing() { return _drawing; }
    private void StartDrawing() { _drawing = true; }
    private void StopDrawing() { _drawing = false; }

    // Strokes
    private GameObject _stroke;
    private GameObject _strokePrefab;
    private LineRenderer _strokeLR;
    private Vector2 _strokeLP;
    private GameObject _strokeContainer;

    // Strokes Control
    private bool _canGetStroke;
    public bool CanGetStroke() { return ((_stroke != null) && (_canGetStroke)); }

    // Unity
    void Awake() {
        _camera = Camera.main;
        _position = new Vector2(0.0f, 0.0f);
        _offset = new Vector2(0.25f, 1.1f);

        _drawing = false;

        _stroke = null;
        _strokePrefab = Resources.Load<GameObject>("Prefabs/Drawing/Stroke");
        _strokeLR = null;
        _strokeLP = Vector2.zero;
        _strokeContainer = GameObject.FindObjectOfType<StrokesContainer>().gameObject;
        _canGetStroke = false;
    }

    public GameObject GetStroke(){
        GameObject tmp = _stroke;
        _stroke = null;
        _canGetStroke = false;
        return tmp;
    }

    // Drawing Action
    public override void MainAction(bool action){
        if (!IsDrawing() && action) {
            Show(0.9f);
            CreateStroke();
            StartDrawing();
        } else if (IsDrawing() && action) {
            Drawing();
        } else if (IsDrawing() && !action) {
            EndDraw();
        }
    }

    void CreateStroke() {
        _stroke = null;
        _canGetStroke = false;
        _stroke = Instantiate(_strokePrefab);
        _strokeLR = _stroke.GetComponent<LineRenderer>();

        _strokeLR.SetPosition(0, _position);
        _strokeLR.SetPosition(1, _position);

        _stroke.transform.SetParent(_strokeContainer.transform);
    }

    public void Drawing() {
        if (_strokeLP != _position) {
            AddStrokePoint(_position);
            _strokeLP = _position;
        }
    }

    public void EndDraw() {
        StopDrawing();
        _stroke.GetComponent<Stroke>().CreateCollier();
        _canGetStroke = true;
    }

    void AddStrokePoint(Vector2 pointPos) {
        _strokeLR.positionCount++;
        int positionIndex = _strokeLR.positionCount - 1;
        _strokeLR.SetPosition(positionIndex, pointPos);
    }

}
