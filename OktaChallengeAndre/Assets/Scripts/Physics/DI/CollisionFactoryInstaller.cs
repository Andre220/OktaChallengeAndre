using UnityEngine;
using Zenject;

public class CollisionFactoryInstaller : MonoInstaller<CollisionFactoryInstaller>
{
    public BasicPhysicsObject bulletPrefb;

    public override void InstallBindings()
    {
        Container.Bind<ICustomCollision>().To<CustomCollision>().AsSingle();
        Container.BindFactory<BasicPhysicsObject, BasicPhysicsObject.Factory>().FromComponentInNewPrefab(bulletPrefb);
    }
}