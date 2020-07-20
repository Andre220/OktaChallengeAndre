using GameEventBus.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPhysicsObjectCollide : EventBase
{
    public BasicPhysicsObject Collider;
    public BasicPhysicsObject Collided;

    public OnPhysicsObjectCollide(BasicPhysicsObject collider, BasicPhysicsObject collided)
    {
        Collider = collider;
        Collided = collided;
    }
}
