using GameEventBus;
using GameEventBus.Interfaces;
using UnityEngine;
using Zenject;

/// <summary>
/// Installer do "barramento" de eventos. Com ele, todo lugar que 
/// solicitar a injecao da classe que implementa a interface IEventBus
/// vai receber a classe EventBus.
/// </summary>

public class EventBusInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IEventBus>().To<EventBus>().AsSingle();
    }
}