using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class ButtonHigh : MonoBehaviour, IPointerEnterHandler {


    [SerializeField]
    private GameObject _selector;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _selector.transform.SetParent(this.transform); 
        _selector.GetComponent<RectTransform>().anchoredPosition = new Vector2(500.0f, -60.0f);
    }
}
