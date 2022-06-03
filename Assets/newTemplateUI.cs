using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newTemplateUI : MonoBehaviour
{

    [SerializeField]
    private GameObject _HUDREMINDER;

    // Start is called before the first frame update

    public void Show()
    {
        _HUDREMINDER.GetComponent<Animator>().SetBool("Show", true);
    }

}
