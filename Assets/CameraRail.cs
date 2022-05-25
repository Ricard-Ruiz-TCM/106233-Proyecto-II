using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRail : MonoBehaviour {

    void OnDrawGizmos() {
        for(int i = 0; i < _nodes.Count -1 ; i++){
            Gizmos.DrawLine(_nodes[i].transform.position, _nodes[i + 1].transform.position);
        }
    }

    public List<GameObject> _nodes;

    public Camera _camera;

    public Transform _player;

    

}
