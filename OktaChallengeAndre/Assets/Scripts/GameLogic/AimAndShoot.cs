using GameEventBus.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AimAndShoot : MonoBehaviour
{
    public BulletStats ChoosedBullet;

    public Transform FirePosition;

    [Inject]
    readonly Bullet.Factory bulletFactory = null;

    [Inject]
    public IEventBus eventBus;

    public float AimRotateSpeed = 50;

    //public float BulletSpeed = 7;

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

            Bullet info = bullet.GetComponent<Bullet>();
            info.Shooter = gameObject;
            info.bulletInfo = ChoosedBullet;

            BasicPhysicsObject physicsProperties = bullet.GetComponent<BasicPhysicsObject>();

            physicsProperties.PhysicsProperties = ChoosedBullet.CustomPhysicsPropertys;

            physicsProperties.Velocity = FirePosition.transform.localScale.normalized.x * 
                -transform.right * ChoosedBullet.BulletSpeed;
        }
    }
}
