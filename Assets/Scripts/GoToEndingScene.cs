using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToEndingScene : MonoBehaviour
{
    [SerializeField]
    private string sceneNameToLoad;
    private float timeElapsed;
    [SerializeField]
    private float delayBeforeLoaading;
    private bool startTimer;
    public ParticleSystem confetti1;
    public ParticleSystem confetti2;
    public ParticleSystem confetti3;
    public ParticleSystem confetti4;

    private void OnEnable()
    {
        BossAttack.OnBossDown += onNextScene;
    }
    private void OnDisable()
    {
        BossAttack.OnBossDown -= onNextScene;

    }
    private void Update()
    {
        if (startTimer == false) return;
        if (timeElapsed > delayBeforeLoaading)
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
        timeElapsed += Time.deltaTime;

    }
    private void Start()
    {
        startTimer = false;
        timeElapsed = 0;
    }
    private void onNextScene()
    {
        startTimer = true;
        confetti1.Play();
        confetti2.Play();
        confetti3.Play();
        confetti4.Play();
        MusicPlayer.Instance.PlayFX("Celebration", 0.65f);
        Invoke("Fader", 1.25f);

    }

    private void Fader()
    {
        GameManager.Instance.Fade();
    }
}

