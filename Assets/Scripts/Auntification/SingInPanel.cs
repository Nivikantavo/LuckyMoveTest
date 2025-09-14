using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine;
using Zenject;

public class SingInPanel : SingPanel
{
    protected override async void OnSingButtonClicked()
    {
        var email = EmailInput.text;
        var password = PasswordInput.text;

        if (!ValidateInputs(email, password)) return;

        SetInteractable(false);
        ClearErrorMessage();

        try
        {
            await LoginUser(email, password);
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

    private async UniTask LoginUser(string email, string password)
    {
        var loginTask = ConnectionFirebase.AuthorizationPlayer.SignInWithEmailAndPasswordAsync(email, password);
        await loginTask.AsUniTask().SuppressCancellationThrow();

        if (loginTask.IsCanceled) return;

        if (loginTask.Exception != null)
        {
            throw loginTask.Exception;
        }

        Debug.Log($"User logged in successfully: {loginTask.Result.User.Email}");
    }

    private void HandleFirebaseError(System.Exception exception)
    {
        string message = "An unknown error occurred.";

        if (exception is FirebaseException firebaseEx)
        {
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            message = errorCode switch
            {
                AuthError.WrongPassword => "Incorrect password. Please try again.",
                AuthError.UserNotFound => "No account found with this email. Please sign up.",
                AuthError.InvalidEmail => "The email address is badly formatted.",
                AuthError.TooManyRequests => "Too many unsuccessful login attempts. Please try again later.",
                _ => $"Login failed: {errorCode.ToString()}"
            };
        }
        else
        {
            message = $"Login failed: {exception.Message}";
        }

        ShowErrorMessage(message);
    }
}