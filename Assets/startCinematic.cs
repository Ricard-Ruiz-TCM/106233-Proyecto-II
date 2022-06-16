using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class startCinematic : MonoBehaviour
{

    public PlayableAsset _timeline;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PlayableDirector>().RebuildGraph();
        GetComponent<PlayableDirector>().Play();
    }


}
