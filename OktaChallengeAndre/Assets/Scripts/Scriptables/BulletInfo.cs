using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletInfo", menuName = "scriptables/BulletInfo", order = 0)]
public class BulletInfo : ScriptableObject
{
    public int MaxBounces;

    public float BulletSpeed;

    public int Damage;

    public void Explosion(Vector2 spawnPosition)
    {
        /*Instantiate(BulletPrefab, spawnPosition, new Quaternion(0, 0, 0, 0));
        Instantiate(BulletPrefab, spawnPosition, new Quaternion(0, 0, 90, 0));
        Instantiate(BulletPrefab, spawnPosition, new Quaternion(0, 0, 180, 0));
        Instantiate(BulletPrefab, spawnPosition, new Quaternion(0, 0, 270, 0));*/
    }

    public void BlackHoles(float holeCreationDelta, int maxBlackHoles)
    {
        Debug.Log("Test Serialized void");
    }

    public IEnumerator CreateHole(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
