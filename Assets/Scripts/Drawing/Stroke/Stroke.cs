using System.Collections.Generic;
using UnityEngine;

public class Stroke : MonoBehaviour {

    // Puntos de collision de la Stroke
    private List<Vector2> _colliderPoints;
    public List<Vector2> ColliderPoints() { return _colliderPoints; }

    // Referente a los puntos de collision
    private float _pointsDistance;
    private float _pointsRadius;

    [SerializeField]
    private bool _completed;
    public bool IsCompleted() { return _completed; }

    // Componente LineRenderer para el dibujado
    private LineRenderer _lineRenderer;

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        foreach(Vector2 pt in _colliderPoints) {
            Gizmos.DrawWireSphere(pt, _pointsRadius);
        }
        Gizmos.color = Color.white;
    }

    void Start() {
        _colliderPoints = new List<Vector2>();
        _pointsDistance = 0.30f;
        _pointsRadius = 0.10f;
        _completed = false;

        _lineRenderer = GetComponent<LineRenderer>();
    }

    public void CreateCollier(){
        if (_lineRenderer.positionCount == 0) return;
        // Punto inicial
        AddPoint(_lineRenderer.GetPosition(0));
        // Cadena de puntos de colision
        CreateChainPoints(0, _lineRenderer.positionCount, _lineRenderer.GetPosition(1));
        // Punto Final
        AddPoint(_lineRenderer.GetPosition(_lineRenderer.positionCount - 1));
        _completed = true;
    }

    private void CreateChainPoints(int index, int total, Vector2 currentPos) {
        if ((index + 1) >= total) return;
        Vector2 nextPos = _lineRenderer.GetPosition(index + 1);
        if (Vector2.Distance(currentPos, nextPos) > _pointsDistance) {
            AddPoint(nextPos);
            currentPos = nextPos;
        }
        CreateChainPoints(index + 1, total, currentPos);
    }

    private void AddPoint(Vector2 pos) {
        _colliderPoints.Add(pos);
    }

}
