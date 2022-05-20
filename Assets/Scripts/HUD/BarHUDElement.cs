using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarHUDElement : MonoBehaviour {

    protected void fillBar(float value, float maxValue){
        float per = value / maxValue;
        float w = transform.parent.GetComponent<RectTransform>().sizeDelta.x;
        float nextW = w * per;
        GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0.0f, nextW);
    }

}
