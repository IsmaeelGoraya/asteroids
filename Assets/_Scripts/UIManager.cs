using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainUI;
    [SerializeField]
    private GameObject _hudUI;

    public void HideMainUI()
    {
        _mainUI.gameObject.SetActive(false);
    }

    public void ShowMainUI()
    {
        _mainUI.gameObject.SetActive(true);
    }

    public void HideHudUI()
    {
        _hudUI.gameObject.SetActive(false);
    }

    public void ShowHudUI()
    {
        _hudUI.gameObject.SetActive(true);
    }
}
