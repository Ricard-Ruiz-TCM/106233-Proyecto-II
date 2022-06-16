using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cinematicchecker : MonoBehaviour
{
    /////////////////////////////////////////////////////////////////////
    // Singleton Instance
    public static cinematicchecker Instance { get; private set; }
    /////////////////////////////////////////////////////////////////////

    public string level;

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
        }

        level = "GameIntro";

        DontDestroyOnLoad(this.gameObject);
    }

    public string NextLevel() {
        string tmp = level;
        level = "Level";
        return tmp;
    }

}
