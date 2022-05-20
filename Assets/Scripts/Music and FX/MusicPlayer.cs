using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public GameObject objectMusic;
    AudioSource audioSurce;
    private float MusicVolume = 0;
    public List<AudioClip> songs;

    private void Awake()
    {
        audioSurce.Play();
        audioSurce.volume = MusicVolume;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSurce = objectMusic.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
