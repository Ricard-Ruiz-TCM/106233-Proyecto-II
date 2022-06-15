using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barhide : MonoBehaviour
{
    private void OnEnable()
    {
        Player.OnPause += Puassee;
    }

    private void OnDisable()
    {
        Player.OnPause -= Puassee;
    }

    private void Puassee(bool isgamepaused)
    {
        if (isgamepaused) Hide();
        else Show();
    }

    private void Hide()
    {
        GetComponent<RectTransform>().anchoredPosition += new Vector2(0.0f, 500.0f);
    }

    private void Show()
    {
        GetComponent<RectTransform>().anchoredPosition -= new Vector2(0.0f, 500.0f);
    }
}
