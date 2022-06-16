using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startlevelalpha : MonoBehaviour
{

    public Image CanvasAlpha;

    float _alhpa;

    private void Start()
    {
        _alhpa = 0.0f;
        Invoke("StartAlpha", 1.75f);
    }

    private void StartAlpha()
    {
        _alhpa = 1.25f;
    }

    // Update is called once per frame
    void Update() {
        CanvasAlpha.color -= new Color(0.0f, 0.0f, 0.0f, _alhpa * Time.deltaTime);
        if (CanvasAlpha.color.a <= 0.0f)
        {
            Destroy(CanvasAlpha.gameObject);
            Destroy(this.gameObject);
        }
    }
}
