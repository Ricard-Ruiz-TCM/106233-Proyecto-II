using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InkButtonHight : MonoBehaviour, IPointerEnterHandler {


    [SerializeField]
    private GameObject _ink;
    [SerializeField]
    private GameObject _brush;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _ink.transform.SetParent(this.transform); 
        _ink.GetComponent<RectTransform>().anchoredPosition = new Vector2(-(_ink.transform.parent.GetComponent<RectTransform>().rect.width / 1.5f), 0.0f);

        _brush.transform.SetParent(this.transform); 
        _brush.GetComponent<RectTransform>().anchoredPosition = new Vector2((_brush.transform.parent.GetComponent<RectTransform>().rect.width / 1.5f), 0.0f);
    }
}
