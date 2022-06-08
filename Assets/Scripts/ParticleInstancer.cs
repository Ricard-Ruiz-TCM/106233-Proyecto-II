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
        _particlesInGame = new List<GameObject>();
    }
    /////////////////////////////////////////////////////////////////////

    private Dictionary<string, GameObject> _particles;
    private List<GameObject> _particlesInGame;

    private void LoadParticles(string name){
        _particles.Add(name, Resources.Load<GameObject>("Prefabs/Particles/" + name));
    }

    private bool Exists(string name){
        return _particles.ContainsKey(name);
    }

    public void StopParticles(string name){
        if (!Exists(name)) return;
        _particles[name].GetComponent<ParticleSystem>().Stop();
    }

    public void StopParticles(int id){
        if ((_particlesInGame.Count < id) || (id < 0)) return;
        if (_particlesInGame[id] == null) return;
        _particlesInGame[id].GetComponent<ParticleSystem>().Stop();
        Destroy(_particlesInGame[id], _particlesInGame[id].GetComponent<ParticleSystem>().main.duration);
    }

    public int StartSpecialParticles(string name, Transform parent) {
        if (!Exists(name)) LoadParticles(name);
        _particlesInGame.Add(InstanceParticles(name, parent));
        return _particlesInGame.Count - 1;
    }

    public void StartParticles(string name, Transform parent){
        if (!Exists(name)) LoadParticles(name);
        GameObject g = InstanceParticles(name, parent);
        Destroy(g.gameObject, g.GetComponent<ParticleSystem>().main.duration*3.0f);
    }

    public void StartParticles(string name, Vector2 position) {
        if (!Exists(name)) LoadParticles(name);
        GameObject g = InstanceParticles(name, GameObject.FindObjectOfType<ElementsContainer>().transform);
        g.transform.SetPositionAndRotation(position, Quaternion.identity);
        Destroy(g.gameObject, g.GetComponent<ParticleSystem>().main.duration*3.0f);
    }

    private GameObject InstanceParticles(string name, Transform parent){
        return Instantiate(_particles[name], parent);
    }


}
