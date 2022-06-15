using UnityEngine;
using System.Collections.Generic;

public class MusicPlayer : MonoBehaviour {

    /////////////////////////////////////////////////////////////////////
    // Singleton Instance
    public static MusicPlayer Instance { get; private set; }
    /////////////////////////////////////////////////////////////////////

    public AudioSource _fxAS;
    public bool _fadeAS;
    public AudioSource _musicAS;
    public bool _fadeCM;
    public AudioSource _currentMusic;

    // Diccionario de FX
    public List<AudioClip> _fx;
    public Dictionary<string, int> _fxDic;

    // Diccionario de Musica
    public List<AudioClip> _music;
    public Dictionary<string, int> _musicDic;

    // Unity
    void Awake(){
        Instance = this;
        _fx = new List<AudioClip>();
        _fxDic = new Dictionary<string, int>();

        _music = new List<AudioClip>();
        _musicDic = new Dictionary<string, int>();

    }

    public void Update() {

        if (_fadeAS) _musicAS.volume -= 0.001f;
                else _musicAS.volume += 0.001f;
        _musicAS.volume = Mathf.Clamp(_musicAS.volume, 0.0f, 1.0f);

        if (_fadeCM) _currentMusic.volume -= 0.001f;
                else _currentMusic.volume += 0.001f;
        _currentMusic.volume = Mathf.Clamp(_currentMusic.volume, 0.0f, 1.0f);

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
        return _music[_musicDic[audio]];
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
        if (_currentMusic.isPlaying) {
            _musicAS.clip = GetMusic(audio);
            _musicAS.Play();
            _musicAS.loop = repeat;
            _musicAS.volume = 0.0f;
            _fadeCM = true; _fadeAS = false;
        } else if (_musicAS.isPlaying) {
            _currentMusic.clip = GetMusic(audio);
            _currentMusic.Play();
            _currentMusic.loop = repeat;
            _currentMusic.volume = 0.0f;
            _fadeAS = true; _fadeCM = false;
        } else {
            _currentMusic.clip = GetMusic(audio);
            _currentMusic.Play();
            _currentMusic.loop = repeat;
            _currentMusic.volume = 0.0f;
            _fadeAS = false;
        }
    }

    // MusicPlayer.cs <Control>
    public bool IsPlaying(string audio){
        return false;
    }

}
