using GameEventBus.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// 
/// 
/// 
/// </summary>

public class Bullet : MonoBehaviour, IPoolable<IMemoryPool>
{
    public GameObject Shooter;

    private int bounceCount;

    public BulletStats bulletInfo;

    IMemoryPool _pool;

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
