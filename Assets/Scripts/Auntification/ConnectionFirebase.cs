using Firebase;
using Firebase.Auth;
using System;
using UnityEngine;
using Zenject;

public class ConnectionFirebase : IDisposable
{
    public FirebaseAuth AuthorizationPlayer { get; private set; }
    public FirebaseUser User { get; private set; }
    public bool IsLoggedIn { get; private set; }

    public event Action OnFirebaseInitialized;
    public event Action OnUserLoggedIn;
    public event Action OnUserLoggedOut;

    private AuthPanel _authPanel;

    [Inject]
    private void Construct(AuthPanel authPanel)
    {
        _authPanel = authPanel;
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                AuthorizationPlayer = FirebaseAuth.DefaultInstance;
                AuthorizationPlayer.StateChanged += AuthStateChanged;

                CheckForAutoLogin();
                OnFirebaseInitialized?.Invoke();

                _authPanel.gameObject.SetActive(!IsLoggedIn);
                
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        });
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (AuthorizationPlayer.CurrentUser != User)
        {
            bool signedIn = User != AuthorizationPlayer.CurrentUser && AuthorizationPlayer.CurrentUser != null;
            IsLoggedIn = signedIn;
            if (!signedIn && User != null)
            {
                Debug.Log("Signed out " + User.UserId);
                
                OnUserLoggedOut?.Invoke();
            }
            User = AuthorizationPlayer.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + User.UserId);
                OnUserLoggedIn?.Invoke();
            }
        }
    }

    private void CheckForAutoLogin()
    {
        if (AuthorizationPlayer.CurrentUser != null)
        {
            User = AuthorizationPlayer.CurrentUser;
            Debug.Log("Auto-logged in: " + User.Email);
            IsLoggedIn = true;
            OnUserLoggedIn?.Invoke();
        }
    }

    public void Dispose()
    {
        Debug.Log("Dispose");
        if (AuthorizationPlayer != null)
        {
            AuthorizationPlayer.StateChanged -= AuthStateChanged;
        }
    }
}