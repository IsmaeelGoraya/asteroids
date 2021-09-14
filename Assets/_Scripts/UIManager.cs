using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainUI;
    [SerializeField]
    private GameObject _hudUI;
    [SerializeField]
    private Text _scoreText;

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

    public void SetScore(int score)
    {
        _scoreText.text = score.ToString();
    }
}
