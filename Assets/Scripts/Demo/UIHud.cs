using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHud : MonoBehaviour
{
    [SerializeField] private TMP_Text stepsText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text shieldsText;
    [SerializeField] private EndLevelPanel endLevelPanel;

    [SerializeField] private Button RestartButton;
    [SerializeField] private Button MenuButton;

    private IPlayerModel _player;

    public void Init(IPlayerModel player)
    {
        _player = player;
    }

    public void ShowEndLevelPanel()
    {
        endLevelPanel.gameObject.SetActive(true);
        endLevelPanel.Init(_player.Steps, _player.Coins);
    }

    private void OnEnable()
    {
        RestartButton.onClick.AddListener(ReloadScene);
        MenuButton.onClick.AddListener(GoToMenu);
    }

    private void OnDisable()
    {
        RestartButton.onClick.RemoveListener(ReloadScene);
        MenuButton.onClick.RemoveListener(GoToMenu);
    }

    private void Update()
    {
        if (_player == null) return;

        stepsText.text = $"Steps: {_player.Steps}";
        coinsText.text = $"Coins: {_player.Coins}";
        shieldsText.text = $"Shields: {_player.Shields}";
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
