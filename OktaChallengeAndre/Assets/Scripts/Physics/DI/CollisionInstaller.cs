using UnityEngine;
using Zenject;

public class CollisionInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.Bind<BasicPhysicsObject>().AsSingle();
        //Container.Bind<ICustomCollision>().To<CustomCollision>().AsSingle();
    }
}