using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject _notificationPanel;
    [SerializeField] private TMP_Text _notificationText;
    [SerializeField] private Button _closeButton;

    private void Awake()
    {
        _closeButton.onClick.AddListener(HideNotification);
        HideNotification();
    }

    public void ShowNotification(string message)
    {
        _notificationText.text = message;
        _notificationPanel.SetActive(true);
    }

    private void HideNotification()
    {
        _notificationPanel.SetActive(false);
    }
}