using GameEventBus.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface que define o contrato para todos que querem utilizar e implementar colisões.
/// 
/// Nela também aparece a interface do "Barramento" de eventos (EventBus).
/// Assim, na implementação da interface, as colisões podem invocar eventos e todas as classes que
/// utilizam essa interface também podem se registrar nesses eventos.
/// 
/// </summary>
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