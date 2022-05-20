using UnityEngine;
using System.Collections.Generic;

public class MusicPlayer : MonoBehaviour {

    /////////////////////////////////////////////////////////////////////
    // Singleton Instance
    public static MusicPlayer Instance { get; private set; }
    void Awake(){
        if (Instance != null && Instance != this) Destroy(this);
                                             else Instance = this;
    }
    /////////////////////////////////////////////////////////////////////

    public GameObject objectMusic;
    AudioSource audioSurce;
    private float MusicVolume = 0;

    // Diccionario de FX
    public List<AudioClip> _fx;
    public Dictionary<string, int> _fxDic;

    // Diccionario de Musica
    public List<AudioClip> _music;
    public Dictionary<string, int> _musicDic;

    // Unity
    void Start(){
        audioSurce.Play();
        audioSurce.volume = MusicVolume;
        audioSurce = objectMusic.GetComponent<AudioSource>();
    }

    // MusicPlayer.cs <Load & Store>
    private AudioClip Load(string audio){
        return null;
    }

    private bool Exists(string audio){
        return false;
    }


    // MusicPlayer.cs <Play>
    public void PlayFX(string audio, float volume, bool repeat){
        if (!Exists(audio)) Load(audio);
        ////////////////////////////////
        // Play del FX con el mixer del FX
    }

    public void PlayMusic(string audio, float volume, bool repeat){
        if (!Exists(audio)) Load(audio);
        ////////////////////////////////
        // Play de la Music con el mixer de la Music
    }

    public bool IsPlaying(string audio){
        return false;
    }

}
