using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine;

public class SingUpPanel : SingPanel
{
    [Header("Success Message")]
    [SerializeField] private string _successMessage = "Registration successful! Welcome!";

    protected override async void OnSingButtonClicked()
    {
        var email = EmailInput.text;
        var password = PasswordInput.text;

        if (!ValidateInputs(email, password)) return;

        SetInteractable(false);
        ClearErrorMessage();

        try
        {
            await RegisterUser(email, password);
            NotificationManager.ShowNotification(_successMessage);
            //todo: переход к меню
        }
        catch (System.Exception e)
        {
            HandleFirebaseError(e);
        }
        finally
        {
            SetInteractable(true);
        }
    }

    private async UniTask RegisterUser(string email, string password)
    {
        var registerTask = ConnectionFirebase.AuthorizationPlayer.CreateUserWithEmailAndPasswordAsync(email, password);

        await registerTask.AsUniTask().SuppressCancellationThrow();

        if (registerTask.IsCanceled)
        {
            Debug.LogWarning("Registration was canceled.");
            return;
        }

        if (registerTask.Exception != null)
        {
            throw registerTask.Exception;
        }

        Debug.Log($"User registered successfully: {registerTask.Result.User.Email}");
    }

    private void HandleFirebaseError(System.Exception exception)
    {
        string message = "An unknown error occurred.";

        if (exception is FirebaseException firebaseEx)
        {
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            message = errorCode switch
            {
                AuthError.EmailAlreadyInUse => "This email is already registered. Please try to sign in.",
                AuthError.WeakPassword => "The password is too weak. Please choose a stronger password.",
                AuthError.InvalidEmail => "The email address is badly formatted.",
                _ => $"Registration failed: {errorCode.ToString()}"
            };
        }
        else
        {
            message = $"Registration failed: {exception.Message}";
        }

        ShowErrorMessage(message);
    }
}