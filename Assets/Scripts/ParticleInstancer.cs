using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleInstancer : MonoBehaviour
{

    /////////////////////////////////////////////////////////////////////
    // Singleton Instance
    public static ParticleInstancer Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        _particles = new Dictionary<string, GameObject>();
    }
    /////////////////////////////////////////////////////////////////////

    private Dictionary<string, GameObject> _particles;

    private void LoadParticles(string name)
    {
        _particles.Add(name, Resources.Load<GameObject>("Prefabs/Particles/" + name));
    }

    private bool Exists(string name)
    {
        return _particles.ContainsKey(name);
    }

    public void StopParticles(string name){
        if (!Exists(name)) return;
        _particles[name].GetComponent<ParticleSystem>().Stop();
    }

    public void StartParticles(string name, Transform parent)
    {
        if (!Exists(name)) LoadParticles(name);
        InstanceParticles(name, parent);
    }

    public void StartParticles(string name, Vector2 position) {
        if (!Exists(name)) LoadParticles(name);
        GameObject g = InstanceParticles(name, GameObject.FindObjectOfType<ElementsContainer>().transform);
        g.transform.SetPositionAndRotation(position, Quaternion.identity);
    }

    private GameObject InstanceParticles(string name, Transform parent)
    {
        return Instantiate(_particles[name], parent);
    }


}
