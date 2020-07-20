using GameEventBus;
using GameEventBus.Interfaces;
using UnityEngine;
using Zenject;

public class EventBusInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IEventBus>().To<EventBus>().AsSingle();
    }
}