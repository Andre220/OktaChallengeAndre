using System.ComponentModel;
using UnityEngine;

/// <summary>
/// Scriptable que armazena as informa��es de cada proj�til.
/// </summary>
[CreateAssetMenu(fileName = "BulletStats", menuName = "Scriptables/Bullets/BulletStats")]
public class BulletStats : ScriptableObject
{
    public int MaxBounces;

    public float BulletSpeed;

    public int Damage;

    public bool CanDamageShooter;

    public CustomPhysicsProperties CustomPhysicsPropertys;
}