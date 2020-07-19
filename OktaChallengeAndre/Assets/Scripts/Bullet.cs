using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public event Action OnBulletDestroyed;

    void OnDestroy()
    {
        OnBulletDestroyed?.Invoke();
    }
}
