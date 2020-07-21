using UnityEngine;
using Zenject;

public class BulletPoolInstaller : MonoInstaller
{
    public GameObject BulletPrefab;
    public override void InstallBindings()
    {
        Container.BindFactory<Bullet, Bullet.Factory>()
        .FromPoolableMemoryPool<Bullet, BulletPool>(poolBinder => poolBinder
        .WithInitialSize(5).FromComponentInNewPrefab(BulletPrefab).UnderTransformGroup("Bullets"));
    }

    class BulletPool : MonoPoolableMemoryPool<IMemoryPool, Bullet> 
    { }
}