using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    public Transform firePosition;
    public BulletInfo equipedBulletInfo;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        GameObject bulletInstance = Instantiate(equipedBulletInfo.BulletPrefab, firePosition.position, firePosition.rotation);
    }
}
