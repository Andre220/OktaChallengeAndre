using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AimBehaviour : MonoBehaviour
{
    public event Action<Player> ShootEvent;

    public Transform FirePosition;

    [Inject]
    private BasicPhysicsObject.Factory bulletFactory;

    public float AimRotateSpeed = 50;

    private float HorizontalAxisInput;

    public float BulletSpeed = 7;

    void Update()
    {
        HorizontalAxisInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        transform.Rotate(Vector3.forward * HorizontalAxisInput * AimRotateSpeed * Time.deltaTime, Space.World);

        /*Vector2 lookDirection = new Vector2(transform.localRotation.x, transform.localRotation.y);

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;

        directionSpeed = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));*/
    }

    public void Shoot()
    {
        //print(gameObject.name);
        ShootEvent?.Invoke(this.gameObject.GetComponent<Player>());
        BasicPhysicsObject bullet = bulletFactory.Create(); //Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);
        bullet.transform.position = FirePosition.transform.position;
        bullet.transform.rotation = gameObject.transform.rotation;
        bullet.GetComponent<BasicPhysicsObject>().Velocity = FirePosition.transform.localScale.normalized.x * -transform.right * BulletSpeed;
    }

    public void OnShoot()
    {

    }
}
