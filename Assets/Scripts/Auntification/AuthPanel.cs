using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AuthPanel : MonoBehaviour
{
    [SerializeField] private List<SingPanel> _singPanels;

    private SingPanel _currentActivePanel;
    private ConnectionFirebase _connectionFirebase;

    [Inject]
    private void Construct(ConnectionFirebase connectionFirebase)
    {
        _connectionFirebase = connectionFirebase;
        _currentActivePanel = _singPanels[0];

        for (int i = 1; i < _singPanels.Count; i++)
        {
            _singPanels[i].gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (_connectionFirebase.IsLoggedIn)
        {
            OnUserLogin();
        }
        else
        {
            _connectionFirebase.OnUserLoggedIn += OnUserLogin;
        }
        
        foreach (var panel in _singPanels)
        {
            panel.NeedSwitchPanel += OnNeedSwitchPanel;
        }
    }

    private void OnDisable()
    {
        _connectionFirebase.OnUserLoggedIn -= OnUserLogin;
        foreach (var panel in _singPanels)
        {
            panel.NeedSwitchPanel -= OnNeedSwitchPanel;
        }
    }

    private void OnNeedSwitchPanel()
    {
        _currentActivePanel.gameObject.SetActive(false);
        _currentActivePanel = _singPanels.FirstOrDefault(panel => panel != _currentActivePanel);
        _currentActivePanel.gameObject.SetActive(_currentActivePanel != null);
    }

    private void OnUserLogin()
    {
        foreach (var panel in _singPanels)
        {
            panel.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
