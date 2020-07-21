using GameEventBus.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// 
/// Monobehaviour anexado ao prefabs das bullets, por onde fazemos as operacoes 
/// de Spawnar(Ativar) e Despawnar(Desativar) as balas do pool de balas.
/// 
/// tambem armazena quem disparou a bala (Shooter), 
/// quantos ricochetes ja fez (bounceCount), 
/// calcula a quantidade de ricochetes restantes e destroi a bala caso tenha ricocheteado o maximo de vezes. 
/// 
/// Invoca o evento de quando a bala é destriuda, para liberar o proximo jogador.
/// 
/// </summary>

public class Bullet : MonoBehaviour, IPoolable<IMemoryPool>
{
    public GameObject Shooter;

    public BulletStats bulletInfo;

    IMemoryPool _pool;

    private int bounceCount;
    
    private SpriteRenderer SR;
    
    [Inject]
    public ICustomCollision customCollision;


    void Start()
    {
        customCollision.eventBus.Subscribe<OnPhysicsObjectCollide>(OnCollision);
        gameObject.GetComponent<BasicPhysicsObject>().PhysicsProperties = bulletInfo.CustomPhysicsPropertys;
    }

    void OnEnable()
    {
        this.bounceCount = 0;
        StartCoroutine(DestroyAfterSeconds());
    }

    void OnDestroy()
    {
    }

    void OnCollision(OnPhysicsObjectCollide collisionInfo)
    {
        if (collisionInfo.Collided.tag == "Player")
        {
            if (collisionInfo.Collided.name != Shooter.name)
            {
                try
                {
                    _pool.Despawn(this);
                }
                catch (Exception e)
                {
                    print(e.ToString());
                }
            }
            else
            {
                if (this.bounceCount >= bulletInfo.MaxBounces)
                {
                    try
                    {
                        _pool.Despawn(this);
                    }
                    catch (Exception e)
                    {
                        print(e.ToString());
                    }
                }
                else
                {
                    bounceCount += 1;
                }
            }
        }
        else if(collisionInfo.Collider.tag == "Wall")
        {
            if (this.bounceCount >= bulletInfo.MaxBounces)
            {
                try
                {
                    _pool.Despawn(this);
                }
                catch (Exception e)
                {
                    print(e.ToString());
                }
            }
            else
            {
                bounceCount += 1;
            }
        }
    }

    IEnumerator DestroyAfterSeconds() //prevent bullet from get out scene forever.
    {
        yield return new WaitForSeconds(10);
        _pool.Despawn(this);
    }

    public void OnSpawned(IMemoryPool pool)
    {
        _pool = pool; //Apenas ativando a bullet do pool. Nao devemos registrar eventos aqui.
    }

    public void OnDespawned()
    {
        customCollision.eventBus.Unsubscribe<OnPhysicsObjectCollide>(OnCollision);
        customCollision.eventBus.Publish(new OnBulletDestroyed());
        _pool = null;
    }

    public class Factory : PlaceholderFactory<Bullet> 
    {
    }
}
