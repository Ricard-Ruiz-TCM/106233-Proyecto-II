using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FireLight : MonoBehaviour {

    private float _fallOff;
    
    private Light2D _light;
    
    void Start() {
        _fallOff = 1.0f;
        _light = GetComponent<Light2D>();
        Invoke("ChangeFallOff", _fallOff);
    }

    private void ChangeFallOff(){
        _fallOff = Random.Range(85.0f, 100.0f) / 100.0f;
        _light.intensity = _fallOff;
        Invoke("ChangeFallOff", 0.05f);
    }

}
