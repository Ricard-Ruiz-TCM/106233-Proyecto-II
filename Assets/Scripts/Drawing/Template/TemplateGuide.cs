using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateGuide : MonoBehaviour {

    private PlayerDrawing _player;

    // Contenedores de los "TemplatePoints"
    private GameObject _mandatoryContainer;
    private GameObject _neutralContainer;
    private GameObject _badContainer;

    [SerializeField]
    private GameObject _template;
    private Transform _container;

    // Listas de los "TemplatePoints"
    private List<TemplatePoint> _mandatory;
    private List<TemplatePoint> _neutral;
    private List<TemplatePoint> _bad;

    // Punto de "inicio" de la template
    public Vector2 StartPoint() { return (_mandatory.Count >= 1 ? _mandatory[0].gameObject.transform.position : transform.position); }

    // Referente a la performance de la template
    [SerializeField]
    private float _completation;
    [SerializeField]
    private float _accuracy;
    [SerializeField]
    private bool _failure;

    private bool _completed;
    public bool IsCompleted() { return _completed; }
    public void Reset() { _completed = false; }

    private float _fade;

    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _fade = 1.0f;
    }

    private void OnDisable()
    {
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    void Awake() {
        _completed = false;

        _player = GameObject.FindObjectOfType<PlayerDrawing>();
        _container = GameObject.FindObjectOfType<ElementsContainer>().transform;

        Fill();

        _completation = 0.0f;
        _accuracy = 0.0f;
        _failure = true;
    }

    private void Update(){
        Color c = GetComponent<SpriteRenderer>().color;
        c.a += _fade * Time.deltaTime;
        GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, c.a);
        if (c.a <= 0.001f) this.gameObject.SetActive(false);
    }

    private void Fill(){
        _mandatoryContainer = transform.GetChild(0).gameObject;
        _neutralContainer = transform.GetChild(1).gameObject;
        _badContainer = transform.GetChild(2).gameObject;
        
        _bad = FillList(_badContainer.transform);
        _neutral = FillList(_neutralContainer.transform);
        _mandatory = FillList(_mandatoryContainer.transform);
    }

    private List<TemplatePoint> FillList(Transform parent) {
        List<TemplatePoint> list = new List<TemplatePoint>();
        for (int i = 0; i < parent.childCount; i++) {
            list.Add(parent.GetChild(i).GetComponent<TemplatePoint>());
        }
        return list;
    }

    public GameObject CheckTemplate() {

        _failure = false;
        _completation = 0.0f;
        _accuracy = 0.0f;

        _completed = false;
        List<Vector2> allStokePoints = new List<Vector2>();
        if (_player == null) _player = GameObject.FindObjectOfType<PlayerDrawing>();
        List<GameObject> _strokes = _player.Strokes();
        foreach (GameObject stroke in _strokes) {
            foreach (Vector2 point in stroke.GetComponent<Stroke>().ColliderPoints()) {
                allStokePoints.Add(point);
            }
        }
        if ((_mandatory == null) || (_neutral == null) || (_bad == null)) Fill();
        _failure = (CheckPointList(_bad, allStokePoints) > 0 ? true : false);
        if (!_failure) {
            _accuracy = 100.0f - CheckPointList(_neutral, allStokePoints) * 100.0f;
            _completation = CheckPointList(_mandatory, allStokePoints) * 100.0f;
        }
        GameObject temp = null;
        if ((_completation >= 90.0f) && (_accuracy >= 80.0f)) {
            temp = Instantiate(_template, _container);
            _completed = true;
        }
        return temp;
    }

    private float CheckPointList(List<TemplatePoint> list, List<Vector2> points) {
        float active = 0;
        foreach (TemplatePoint point in list) {
            active += (point.CheckCollision(points) ? 1 : 0);
        }
        return (active / list.Count);
    }

    public void FadeOut() {
        _fade = -10.0f;
    }

    public void FullAlpha()
    {
        _fade = 1.0f;
    }

}
