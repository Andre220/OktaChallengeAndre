using GameEventBus.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

//[RequireComponent(typeof(BasicPhysicsObject))]
public class Bullet : MonoBehaviour, IPoolable<IMemoryPool>
{
    public GameObject Shooter; //Player que disparou

    private int bounceCount;

    public BulletInfo bulletInfo;

    IMemoryPool _pool;

    [Inject]
    public ICustomCollision customCollision;

    //[Inject]
    //public IEventBus eventBus;

    void Start()
    {
        customCollision.eventBus.Subscribe<OnPhysicsObjectCollide>(OnCollision);
    }

    void OnDestroy()
    {
        //customCollision.eventBus.Unsubscribe<OnPhysicsObjectCollide>(OnCollision);
    }

    void OnCollision(OnPhysicsObjectCollide collisionInfo) //Forma atual de fazer um "callback" para colisões
    {
        if (collisionInfo.Collided.tag == "Player")
        {
            //print("(Bullet) " + gameObject.GetInstanceID() + " Collided");
            try
            {
                _pool.Despawn(this);
            }
            catch (Exception e)
            {
                print(e.ToString());
            }

        }
        else if(collisionInfo.Collider.tag == "Wall")
        {
            if (bounceCount >= 3)
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

            bounceCount += 1;
        }
    }

    public void OnSpawned(IMemoryPool pool)
    {
        _pool = pool;
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
