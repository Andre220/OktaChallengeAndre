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

    void CollisionBetweenCircleAndSquare(CircleModel circle, SquareModel square);
    void CollisionBetweenSquareAndCircle(SquareModel square, CircleModel circle);
    void CollisionBetweenCircleAndCircle(CircleModel circleA, CircleModel circleB);
    void CollisionBetweenSquareAndSquare(SquareModel squareA, SquareModel squareB);
}