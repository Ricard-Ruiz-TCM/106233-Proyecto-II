using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelAftertime : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoaading;
    [SerializeField]
    private string sceneNameToLoad;
    private float timeElapsed;
    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed>delayBeforeLoaading)
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
     
    }
}
