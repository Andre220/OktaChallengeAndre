using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BasicPhysicsObject))]
public class Bullet : MonoBehaviour
{
    [Inject]
    public ICustomCollision customCollision;

    //public event Action OnBulletDestroyed;

    void OnEnable()
    {
        customCollision.OnCollisionEvent += OnCollision;
    }

    void OnDestroy()
    {
        customCollision.OnCollisionEvent -= OnCollision;
        //customCollision.RemoveColliderFromBuffer(this);
        //OnBulletDestroyed?.Invoke();
    }

    void OnCollision(BasicPhysicsObject bpo) //Forma atual de fazer um "callback" para colisões
    {
        if ((gameObject.name == "Player1" && bpo.name == "Player2") || (gameObject.name == "Player2" && bpo.name == "Player1"))
        {
            Destroy(gameObject);
        }
        //print(bpo.gameObject.name);
    }
}
