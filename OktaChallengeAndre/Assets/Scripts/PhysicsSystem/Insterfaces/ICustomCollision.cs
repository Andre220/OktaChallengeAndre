using GameEventBus.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICustomCollision
{
    IEventBus eventBus { get; }

    List<BasicPhysicsObject> collidersInScene { get; }

    void AddColliderToBuffer(BasicPhysicsObject bpo);
    void RemoveColliderFromBuffer(BasicPhysicsObject bpo);

    void CollisionBetweenCircleAndSquare(CustomCircleCollider circle, CustomSquareCollider square);
    void CollisionBetweenSquareAndCircle(CustomSquareCollider square, CustomCircleCollider circle);
    void CollisionBetweenCircleAndCircle(CustomCircleCollider circleA, CustomCircleCollider circleB);
    void CollisionBetweenSquareAndSquare(CustomSquareCollider squareA, CustomSquareCollider squareB);
}