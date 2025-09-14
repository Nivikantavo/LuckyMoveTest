using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SingPanel : MonoBehaviour
{
    public Action NeedSwitchPanel;

    [SerializeField] protected TMP_InputField EmailInput;
    [SerializeField] protected TMP_InputField PasswordInput;
    [SerializeField] protected Button SingButton;
    [SerializeField] protected Button SwitchSingButton;
    [SerializeField] protected TMP_Text ErrorText;
    [SerializeField] private GameObject _loader;

    protected ConnectionFirebase ConnectionFirebase;
    protected NotificationManager NotificationManager;

    [Inject]
    private void Construct(ConnectionFirebase connectionFirebase, NotificationManager notificationManager)
    {
        ConnectionFirebase = connectionFirebase;
        NotificationManager = notificationManager;
    }

    protected virtual void OnEnable()
    {
        SingButton.onClick.AddListener(OnSingButtonClicked);
        SwitchSingButton.onClick.AddListener(OnSwitchSingButtonClicked);
        ClearErrorMessage();
    }

    protected virtual void OnDisable()
    {
        SingButton.onClick.RemoveListener(OnSingButtonClicked);
        SwitchSingButton.onClick.RemoveListener(OnSwitchSingButtonClicked);
    }

    protected virtual void OnSingButtonClicked() { }

    protected virtual void OnSwitchSingButtonClicked()
    {
        ClearErrorMessage();
        NeedSwitchPanel?.Invoke();
    }

    protected void SetInteractable(bool interactable)
    {
        EmailInput.interactable = interactable;
        PasswordInput.interactable = interactable;
        SingButton.interactable = interactable;
        SwitchSingButton.interactable = interactable;

        _loader.gameObject.SetActive(!interactable);
    }

    protected void ShowErrorMessage(string message)
    {
        ErrorText.text = message;
        ErrorText.gameObject.SetActive(true);
    }

    protected void ClearErrorMessage()
    {
        ErrorText.text = "";
        ErrorText.gameObject.SetActive(false);
    }

    protected bool ValidateInputs(string email, string password)
    {
        ClearErrorMessage();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ShowErrorMessage("Please fill in all fields.");
            return false;
        }

        if (!email.Contains("@"))
        {
            ShowErrorMessage("Please enter a valid email address.");
            return false;
        }

        if (password.Length < 6)
        {
            ShowErrorMessage("Password must be at least 6 characters long.");
            return false;
        }

        return true;
    }
}