using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class ShadowIntensity : MonoBehaviour {

    private Light2D _light;

    void Start() {
        _light = GetComponent<Light2D>();
        InvokeRepeating("ChangeSI", 0.0f, 0.25f);
    }

    private void ChangeSI()
    {
        _light.shadowIntensity += Random.Range(-0.005f, 0.005f);
        _light.shadowIntensity = Mathf.Min(Mathf.Max(_light.shadowIntensity, 0.05f), 0.20f);
    }

    public void StopSunshines()
    {
        CancelInvoke("ChangeSI");
        _light.shadowIntensity = 1.0f;
    }

}
