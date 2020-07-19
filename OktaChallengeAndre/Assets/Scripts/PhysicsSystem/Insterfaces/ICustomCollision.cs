using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICustomCollision
{
    List<BasicPhysicsObject> collidersInScene { get; }
    
    event Action<BasicPhysicsObject> OnCollisionEvent;

    //void OnCollision(BasicPhysicsObject callerCollider);

    void AddColliderToBuffer(BasicPhysicsObject bpo);
    void RemoveColliderFromBuffer(BasicPhysicsObject bpo);

    void CollisionBetweenCircleAndSquare(CircleModel circle, SquareModel square);
    void CollisionBetweenCircleAndCircle(CircleModel circleA, CircleModel circleB);
    void CollisionBetweenSquareAndSquare(SquareModel squareA, SquareModel squareB);
}