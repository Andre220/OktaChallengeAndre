using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletStats", menuName = "Scriptables/Bullets/BulletStats")]
public class BulletStats : ScriptableObject
{
    public int MaxBounces;

    public float BulletSpeed;

    public int Damage;

    public CustomPhysicsProperties CustomPhysicsPropertys;
}