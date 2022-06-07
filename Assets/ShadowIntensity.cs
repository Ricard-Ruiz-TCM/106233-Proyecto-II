using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class ShadowIntensity : MonoBehaviour {

    private Light2D _light;

    private float _increasing;
    private int _times;

    void Start() {
        _light = GetComponent<Light2D>();
        InvokeRepeating("ChangeSI", 0.0f, 0.1f);
        _increasing = 0.005f;
    }

    private void ChangeSI()
    {
        _light.shadowIntensity += Random.Range(_increasing / 10.0f, _increasing);
        _light.shadowIntensity = Mathf.Clamp(_light.shadowIntensity, 0.005f, 0.15f);
        _times++;
        if (Random.Range(0, 200) < (_times / 2.5f)) { _increasing *= -1; _times = 0; }
    }

    public void StopSunshines()
    {
        CancelInvoke("ChangeSI");
        _light.shadowIntensity = 1.0f;
    }

}
