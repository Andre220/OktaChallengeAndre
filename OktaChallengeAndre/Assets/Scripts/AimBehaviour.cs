using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AimBehaviour : MonoBehaviour
{
    public GameEvent OnBulletShooted;

    public event Action<Player> ShootEvent;

    public Transform FirePosition;

    [Inject]
    private BasicPhysicsObject.Factory bulletFactory;


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
        OnBulletShooted.Raise();
        BasicPhysicsObject bullet = bulletFactory.Create();

        bullet.transform.position = FirePosition.transform.position;
        bullet.transform.rotation = gameObject.transform.rotation;

        bullet.GetComponent<Bullet>().Shooter = gameObject;
        
        bullet.GetComponent<BasicPhysicsObject>().Velocity = FirePosition.transform.localScale.normalized.x * -transform.right * BulletSpeed;
    }
}
