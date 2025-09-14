using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _exitPanelButton;
    [SerializeField] private GameObject _exitPanel;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(OnStartButtonClicked);
        _exitButton.onClick.AddListener(OnExitButtonClicked);
        _exitPanelButton.onClick.AddListener(OnExitPanelButtonClicked);

    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(OnStartButtonClicked);
        _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        _exitPanelButton.onClick.RemoveListener(OnExitPanelButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        SceneManager.LoadScene(1);
    }

    private void OnExitPanelButtonClicked()
    {
        _exitPanel.SetActive(true);
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
