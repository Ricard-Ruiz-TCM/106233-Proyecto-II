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
        _light.color = new Color(1.0f, (Random.Range(90.0f, 110.0f) / 255.0f), 0.0f, 1.0f);
        Invoke("ChangeFallOff", 0.1f);
    }

}
