using GameEventBus.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AimBehaviour : MonoBehaviour
{
    public Transform FirePosition;

    //readonly BasicPhysicsObject.Factory bulletFactory;
    [Inject]
    readonly Bullet.Factory bulletFactory = null;

    [Inject]
    public IEventBus eventBus;

    public float AimRotateSpeed = 50;

    public float BulletSpeed = 7;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        transform.Rotate(Vector3.forward * Input.GetAxisRaw("Horizontal") * AimRotateSpeed * Time.deltaTime, Space.World);
    }

    public void Shoot()
    {
        eventBus.Publish(new OnBulletShooted(gameObject.GetComponent<Player>()));

        var bullet = bulletFactory.Create();

        if (bullet != null)
        {
            bullet.transform.position = FirePosition.transform.position;
            bullet.transform.rotation = gameObject.transform.rotation;

            //bullet.GetComponent<Bullet>().Shooter = gameObject;

            bullet.GetComponent<BasicPhysicsObject>().Velocity = FirePosition.transform.localScale.normalized.x * -transform.right * BulletSpeed;
        }
    }
}
