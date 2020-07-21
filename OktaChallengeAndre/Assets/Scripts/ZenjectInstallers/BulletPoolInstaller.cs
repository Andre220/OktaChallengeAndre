using UnityEngine;
using Zenject;

/// <summary>
/// Installer do Zenject responsavel por fazer o bidind da Factory de balas (BindFactory)
/// Gerenciar o pool de balas (FromPoolableMemoryPool) que contem 5 objetos (WithInitialSize)
/// criados a partir do prefab (FromComponentInNewPrefab(BulletPrefab)) abaixo do Transform (criado quando o jogo inicia)
/// chamado "Bullets".
/// </summary>

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