using UnityEngine;
using Zenject;

public class NotificationInstaller : MonoInstaller
{
    [SerializeField] private NotificationManager _notificationManager;
    public override void InstallBindings()
    {
        Container.Bind<NotificationManager>().FromInstance(_notificationManager).AsSingle().NonLazy();
    }
}
