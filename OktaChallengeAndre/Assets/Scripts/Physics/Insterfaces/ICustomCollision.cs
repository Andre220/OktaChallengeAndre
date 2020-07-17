using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICustomCollision
{
    List<BasicPhysicsObject> collidersInScene { get; }

    event Action OnCollisionHappened;

    void OnCollision(BasicPhysicsObject callerCollider, CollisionType collisionType);

    void AddColliderToBuffer(BasicPhysicsObject bpo);
    void RemoveColliderToBuffer(BasicPhysicsObject bpo);

    void CollisionBetweenCircleAndSquare(CircleModel circle, SquareModel square);
    void CollisionBetweenCircleAndCircle(CircleModel circleA, CircleModel circleB);
    void CollisionBetweenSquareAndSquare(SquareModel squareA, SquareModel squareB);
}

public enum CollisionType 
{
    CircleAndSquare,
    CircleAndCircle,
    SquareAndSquare
}
