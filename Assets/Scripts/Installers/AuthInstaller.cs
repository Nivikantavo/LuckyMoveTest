using UnityEngine;
using Zenject;

public class AuthInstaller : MonoInstaller
{
    [SerializeField] private AuthPanel _authPanel;
    public override void InstallBindings()
    {
        Container.Bind<ConnectionFirebase>().AsSingle().NonLazy();
        Container.Bind<AuthPanel>().FromInstance(_authPanel).AsSingle().NonLazy();
    }
}
