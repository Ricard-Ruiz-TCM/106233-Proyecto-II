using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplatePoint : MonoBehaviour {

    [SerializeField]
    private float _radius;

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(this.transform.position, _radius);
    }

    private void Start() {
        _radius = 0.25f;
    }

    public bool CheckCollision(List<Vector2> points) {
        foreach (Vector2 it in points) {
            if (Vector2.Distance(it, this.transform.position) < _radius) {
                return true;
            }
        }
        return false;
    }

}
