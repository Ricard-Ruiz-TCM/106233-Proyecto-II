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

    public AudioSource _fxAS;
    public AudioSource _musicAS;

    // Diccionario de FX
    public List<AudioClip> _fx;
    public Dictionary<string, int> _fxDic;

    // Diccionario de Musica
    public List<AudioClip> _music;
    public Dictionary<string, int> _musicDic;

    // Unity
    void Start(){
        _fx = new List<AudioClip>();
        _fxDic = new Dictionary<string, int>();

        _music = new List<AudioClip>();
        _musicDic = new Dictionary<string, int>();
    }

    // MusicPlayer.cs <Load>
    private void Load(List<AudioClip> list, Dictionary<string, int> dic, string path, string audio){
        list.Add(Resources.Load<AudioClip>("Audio/" + path + "/" + audio));
        dic.Add(audio, list.Count - 1);
    }

    private void LoadFX(string audio){
        Load(_fx, _fxDic, "FX", audio);
    }

    private void LoadMusic(string audio) {
        Load(_music, _musicDic, "Music", audio);
    }

    // MusicPlayer.cs <Exists>
    private bool Exists(Dictionary<string,int> dic, string key) {
        return dic.ContainsKey(key);
    }

    private bool ExistsFX(string audio){
        return Exists(_fxDic, audio);
    }

    private bool ExistsMusic(string audio){
        return Exists(_musicDic, audio);
    }

    // MusicPlayer.cs <Get>
    private AudioClip GetFX(string audio){
        return _fx[_fxDic[audio]];
    }

    private AudioClip GetMusic(string audio){
        return _fx[_fxDic[audio]];
    }

    // MusicPlayer.cs <Play>
    public void PlayMe(AudioSource audio){
        audio.PlayOneShot(audio.clip);
    }

    public void PlayFX(string audio, float volume = 1){
        if (!ExistsFX(audio)) LoadFX(audio);
        ////////////////////////////////
        // Play del FX con el mixer del FX
        _fxAS.PlayOneShot(GetFX(audio), volume);
    }

    public void PlaySpecialFX(string audio, float volume = 1, double time = 0.0f){
        if (!ExistsFX(audio)) LoadFX(audio);
        ////////////////////////////////
        // Play del FX con el mixer del FX
        _fxAS.clip = GetFX(audio);
        _fxAS.volume = volume;
        _fxAS.PlayScheduled(time);
    }

    public void StopFX(string audio){
        if (!ExistsFX(audio)) LoadFX(audio);
        ////////////////////////////////
        // Play del FX con el mixer del FX
        if (_fxAS.isPlaying) _fxAS.Stop();
        _fxAS.volume = 1.0f;
        _fxAS.clip = null;
    }

    public void PlayMusic(string audio, float volume = 1, bool repeat = false){
        if (!ExistsMusic(audio)) LoadMusic(audio);
        ////////////////////////////////
        // Play de la Music con el mixer de la Music
        _musicAS.clip = GetMusic(audio);
        _musicAS.Play();
    }

    // MusicPlayer.cs <Control>
    public bool IsPlaying(string audio){
        return false;
    }

}
