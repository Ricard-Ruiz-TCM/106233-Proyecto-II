using UnityEngine;

public class PlayerPlacing : PlayerState, IHaveStates {

    [SerializeField]
    private bool _placed;
    public bool IsPlaced() { return _placed; }

    // Place Speed
    [SerializeField]
    private float _placeSpeed;
    private float PlaceSpeed() { return _placeSpeed; }

    // Template que estamos colocando
    [SerializeField]
    private GameObject _templateElement;
    private Template Template() { return _templateElement.GetComponent<Template>(); }
    private DrawElement TemplateElement() { return _templateElement.GetComponent<DrawElement>(); }

    // Template de Box Activa    
    private GameObject _activeBox;

    private GameObject _box2Dstroy;

    // Drawing System
    private PlayerDrawing _drawing;

    // Unity
    void Awake(){
        LoadState();
        ////////////
        _placed = false;
        _placeSpeed = 0.05f;

        _activeBox = null;
        _templateElement = null;

        _drawing = GetComponent<PlayerDrawing>();   
    }

    // PlayerPlacing.cs <Place>
    private void StartPlacing(){
        _placed = false;
        _templateElement = _drawing.CreatedTemplate();
        _templateElement.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
    }

    private void EndPlacing(){
        DisableSystem();
    }

    private void SwapBox(GameObject box){
        if (_activeBox != null) {
            _box2Dstroy = _activeBox;
            Destroy(_box2Dstroy, 2.0f);
        }
        _activeBox = box;
    }

    public void StopPlacing(){
        EndPlacing();
        _placed = true;
        Destroy(_templateElement);
    }

    // IHaveStates
    public void OnEnterState(){
        EnableSystem();
        ///////////////
        StartPlacing();
        _animator.SetBool("Draw", true);
    }

    public void OnExitState(){
        _animator.SetBool("Draw", false);
        ////////////////
        DisableSystem();
    }

    public void OnState() {
        if (!IsEnabled()) return;
        /////////////////////////

        bool placeAction = false;

        if (Input().Keyboard()){
            TemplateElement().Screen2WorldPosition(Input().MousePos());
            placeAction = Input().MainAction();
        }

        if (Input().GamePad()){
            Template().Move(Input().Joystick() * PlaceSpeed());
            placeAction = Input().Jump() || Input().MainAction();
        }

        if (!Template().Placed()) {
            if (placeAction) {
                Template().TryToPlace();
                _placed = Template().Placed();
            }
        }

        if (IsPlaced()) {
            EndPlacing();
            Template().MainAction(placeAction);
            if (Template().IsBox()) SwapBox(_templateElement);
        }
    }

}
