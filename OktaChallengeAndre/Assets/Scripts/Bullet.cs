using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BasicPhysicsObject))]
public class Bullet : MonoBehaviour
{
    public GameObject Shooter; //Player que disparou

    public GameEvent OnBulletDestroyed;

    private int bounceCount;

    public BulletInfo bulletInfo;

    [Inject]
    public ICustomCollision customCollision;

    void OnEnable()
    {
        customCollision.OnCollisionEvent += OnCollision;
    }

    private void OnDestroy()
    {
        OnBulletDestroyed.Raise();
        customCollision.OnCollisionEvent -= OnCollision;
    }

    void OnCollision(BasicPhysicsObject bpo) //Forma atual de fazer um "callback" para colisões
    {
        if (bpo.gameObject != Shooter)
        {
            if (bpo.gameObject.name == "LeftPlayer")
            {
                Destroy(gameObject);
            }
            else if (bpo.gameObject.name == "RightPlayer")
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    //void DestroyGameObject()
    //{
    //    customCollision.OnCollisionEvent -= OnCollision;
    //    OnBulletDestroyed.Raise();
    //    Destroy(this.gameObject);
    //}

    IEnumerator DestroyWithDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

    }
}
