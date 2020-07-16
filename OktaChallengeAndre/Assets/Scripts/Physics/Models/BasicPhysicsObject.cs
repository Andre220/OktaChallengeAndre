using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPhysicsObject : MonoBehaviour
{
    public Vector3 Velocity;

    public bool isStatic;
    public bool bounce;

    void Update()
    {
        gameObject.transform.position += Velocity * Time.deltaTime;    
    }
}

public class CollisionModel
{
    public GameObject other;
}
