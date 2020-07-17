﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AimBehaviour : MonoBehaviour
{
    /*[Inject]
    ICustomCollision iCustomCollision;*/

    [Inject]
    private BasicPhysicsObject.Factory bulletFactory;

    public float AimRotateSpeed = 2;

    private float HorizontalAxisInput;

    //public Vector2 directionSpeed;

    public float BulletSpeed;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalAxisInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            BasicPhysicsObject bullet = bulletFactory.Create(); //Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);
            bullet.transform.position = gameObject.transform.position;
            bullet.transform.rotation = gameObject.transform.rotation;
            bullet.GetComponent<BasicPhysicsObject>().Velocity = -transform.right * BulletSpeed;
        }

        transform.Rotate(Vector3.forward * HorizontalAxisInput * AimRotateSpeed * Time.deltaTime, Space.World);
        /*Vector2 lookDirection = new Vector2(transform.localRotation.x, transform.localRotation.y);

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90;

        directionSpeed = new Vector2(Mathf.Sin(Mathf.Deg2Rad * angle), Mathf.Cos(Mathf.Deg2Rad * angle));*/
    }

    void FixedUpdate()
    {

    }
}
