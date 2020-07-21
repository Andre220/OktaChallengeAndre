using System;
using Zenject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using GameEventBus.Interfaces;

/// <summary>
/// Escrita por: André Felipe dos Santos
/// 
/// 
/// Esta é a classe principal do sistema de física.
/// 
/// Ela possui a lógica para detectar colisões entre objetos circulares (classe CustomCircleCollider) 
/// e quadrados ( classe CustomSquareCollider).
/// 
/// Também realiza os cálculos referentes as reações físicas dessas colisões, 
/// como ricochetear (BounceCollision) ou parar de se mover ao colidir com outro objeto (StopOnCollide).
/// 
/// OBS: Esta classe não é anexada a game objects ou instanciada. Ela implementa a interface 
/// ICustomCollision e é injetada em toda classe que precisa desta interface
/// 
/// </summary>
public class CustomCollision : ICustomCollision
{    
    public List<BasicPhysicsObject> collidersInScene { get; }

    [Inject]
    public IEventBus eventBus { get; }

    //IEventBus ICustomCollision.eventBus => eventBus;

    #region definições da classe e suas propriedades
    public CustomCollision()
    {
        collidersInScene = new List<BasicPhysicsObject>();
    }

    public void AddColliderToBuffer(BasicPhysicsObject bpo)
    {
        collidersInScene.Add(bpo);
    }

    public void RemoveColliderFromBuffer(BasicPhysicsObject bpo)
    {
        collidersInScene.Remove(bpo);
    }
    #endregion

    #region Calculando colisao com base no formato dos objetos
    public void CollisionBetweenCircleAndSquare(CustomCircleCollider circle, CustomSquareCollider square)
    {
        if (circle == null || square == null)
        {
            return;
        }

        float Cx = circle.transform.position.x;
        float Cy = circle.transform.position.y;
        float Sx = square.transform.position.x;
        float Sy = square.transform.position.y;

        //float minDistanceTest = square.transform.position.x + square.size.x - circle.transform.position.x + circle.CircleDiameter; 

        //float distanceBetweenObjects = (Cx - Sx * Cx - Sx) + (Cy - Sy * Cy - Sy);
        float distanceBetweenObjects = Vector2.Distance(square.transform.position, circle.gameObject.transform.position);

        if (distanceBetweenObjects < circle.CircleDiameter + square.size.x)
        {
            /*Testando se as bordas do circulo (circulo + raio) estão sobreponto alguma das bordas do quadrilatero em questão*/
            if (Cx + circle.CircleRadius > square.LeftEdge && Cx - circle.CircleRadius < square.RightEdge
                && (Cy + circle.CircleRadius > square.BottomEdge && square.TopEdge > Cy - circle.CircleRadius ||
                    Cy - circle.CircleRadius < square.TopEdge && square.BottomEdge < Cy + circle.CircleRadius))
            {
                if (circle.PhysicsProperties.canBounce)
                {
                    BounceCollision(circle, square);
                }

                if (circle.PhysicsProperties.stopOnCollide || square.PhysicsProperties.stopOnCollide)
                {
                    if (circle.PhysicsProperties.stopOnCollide)
                    {
                        StopOnCollide(circle, square);
                    }
                    else
                    {
                        StopOnCollide(square, circle);
                    }
                }
                eventBus.Publish(new OnPhysicsObjectCollide(circle, square)); // chamando o evento. Analogo ao "Invoke".
            }
            else
            {
                return;
            }

            eventBus.Publish(new OnPhysicsObjectCollide(square, circle)); // chamando o evento. Analogo ao "Invoke".
        }
    }

    public void CollisionBetweenSquareAndCircle(CustomSquareCollider square, CustomCircleCollider circle)
    {
        if (square == null || circle == null)
        {
            return;
        }

        float Cx = circle.transform.position.x;
        float Cy = circle.transform.position.y;
        float Sx = square.transform.position.x;
        float Sy = square.transform.position.y;

        //float distanceBetweenObjects = (Cx - Sx * Cx - Sx) + (Cy - Sy * Cy - Sy);
        float distanceBetweenObjects = Vector2.Distance(square.transform.position, circle.gameObject.transform.position);

        if (distanceBetweenObjects < circle.CircleDiameter + square.size.x)
        {
            /*Testando se as bordas do circulo (circulo + raio) estão sobreponto alguma das bordas do quadrilatero em questão*/
            if (Cx + circle.CircleRadius > square.LeftEdge && Cx - circle.CircleRadius < square.RightEdge
                && (Cy + circle.CircleRadius > square.BottomEdge && square.TopEdge > Cy - circle.CircleRadius ||
                    Cy - circle.CircleRadius < square.TopEdge && square.BottomEdge < Cy + circle.CircleRadius))
            {
                if (circle.PhysicsProperties.canBounce)
                {
                    BounceCollision(circle, square); //Not work properly
                }

                if (circle.PhysicsProperties.stopOnCollide || square.PhysicsProperties.stopOnCollide)
                {
                    if (circle.PhysicsProperties.stopOnCollide)
                    {
                        StopOnCollide(circle, square);
                    }
                    else
                    {
                        StopOnCollide(square, circle);
                    }
                }
            }
            else
            {
                return;
            }

            eventBus.Publish(new OnPhysicsObjectCollide(square, circle)); // chamando o evento. Analogo ao "Invoke".
        }
    }

    public void CollisionBetweenCircleAndCircle(CustomCircleCollider circleA, CustomCircleCollider circleB)
    {
        if (circleA == null || circleB == null)
        {
            return;
        }

        float Ax = circleA.transform.position.x;
        float Ay = circleA.transform.position.y;
        float Bx = circleB.transform.position.x;
        float By = circleB.transform.position.y;

        float distanceBetweenCircles = Vector2.Distance(circleA.transform.position, circleB.gameObject.transform.position);

        //UnityEngine.Debug.Log($"d: {distanceBetweenCircles} | diam: {circleA.CircleRadius + circleB.CircleRadius}");

        if(distanceBetweenCircles < circleA.CircleRadius + circleB.CircleRadius)
        {
            //Colisao ocorreu

            float angle = Mathf.Atan2(circleA.transform.position.x - circleB.gameObject.transform.position.x, circleA.transform.position.y - circleB.gameObject.transform.position.y);

            float distanceToMove = (circleA.CircleRadius + circleB.CircleRadius) - distanceBetweenCircles;

            Vector3 movement = new Vector3(angle * distanceBetweenCircles * Time.deltaTime
                , angle * distanceBetweenCircles * Time.deltaTime
                , 0);

            if (circleB.PhysicsProperties != null)
            {
                if (circleB.PhysicsProperties.canBounce)
                {
                    BounceCollision(circleA, circleB);
                }
                else if (circleB.PhysicsProperties.stopOnCollide || circleA.PhysicsProperties.stopOnCollide)
                {
                    if (circleA.PhysicsProperties.stopOnCollide)
                    {
                        StopOnCollide(circleA, circleB);
                    }
                    else
                    {
                        StopOnCollide(circleB, circleA);
                    }
                }
            }

            //UnityEngine.Debug.Log($"{circleA.name} | {circleB.name}");
            eventBus.Publish(new OnPhysicsObjectCollide(circleA, circleB)); // chamando o evento. Analogo ao "Invoke".
        }  
    }

    public void CollisionBetweenSquareAndSquare(CustomSquareCollider squareA, CustomSquareCollider squareB)
    {
        if (squareA == null || squareB == null)
        {
            return;
        }

        float Ax = squareA.transform.position.x;
        float Ay = squareA.transform.position.y;
        float Bx = squareB.transform.position.x;
        float By = squareB.transform.position.y;

        //float minDistanceTest = squareA.transform.position.x + squareA.size.x - squareB.transform.position.x + squareB.size.x;

        //float distanceBetweenSquares = (Ax - Bx * Ax - Bx) + (Ay - By * Ay - By);
        float distanceBetweenSquares = Vector2.Distance(squareA.transform.position, squareB.gameObject.transform.position);

        if (distanceBetweenSquares < squareA.size.x / 2 + squareB.size.x /2)
        {
            if ((squareA.LeftEdge < squareB.RightEdge && squareB.RightEdge < squareA.RightEdge) &&
            (squareA.TopEdge > squareB.BottomEdge && squareB.BottomEdge > squareA.BottomEdge ||
            squareA.BottomEdge < squareB.TopEdge && squareB.TopEdge < squareA.TopEdge))
            {
                UnityEngine.MonoBehaviour.print($"Square {squareA.gameObject.name} and square {squareB.gameObject.name} Collided");

                if (squareA.PhysicsProperties.canBounce || squareB.PhysicsProperties.canBounce)
                {
                    BounceCollision(squareA, squareB);
                }
                else if (squareA.PhysicsProperties.stopOnCollide || squareB.PhysicsProperties.stopOnCollide)
                {
                    if (squareA.PhysicsProperties.stopOnCollide)
                    {
                        StopOnCollide(squareA, squareB);
                    }
                    else
                    {
                        StopOnCollide(squareB, squareA);
                    }
                }
            }
            else if ((squareA.RightEdge > squareB.LeftEdge && squareB.LeftEdge > squareA.LeftEdge) &&
            (squareA.TopEdge > squareB.BottomEdge && squareB.BottomEdge > squareA.BottomEdge
            || squareA.BottomEdge < squareB.TopEdge && squareB.TopEdge < squareA.TopEdge))
            {
                BounceCollision(squareA, squareB);
            }

            eventBus.Publish(new OnPhysicsObjectCollide(squareA, squareB)); // chamando o evento. Analogo ao "Invoke".
        }
    }
    #endregion

    #region Custom Behaviour
    public void BounceCollision(BasicPhysicsObject thisObject, BasicPhysicsObject other)
    {
        if (thisObject.PhysicsProperties.canBounce)
        {
            Vector2 tangent;
            tangent.y = -(other.transform.position.x - thisObject.transform.position.x);
            tangent.x = other.transform.position.y - thisObject.transform.position.y;

            tangent.Normalize();

            Vector2 relativeVelocity = new Vector2(thisObject.Velocity.x - other.Velocity.x, thisObject.Velocity.y - other.Velocity.y);

            float lenght = Vector2.Dot(relativeVelocity, tangent);

            Vector2 velocityComponentOnTangent;
            velocityComponentOnTangent = tangent * lenght;

            Vector2 velocityComponentPerpendicularToTangent = relativeVelocity - velocityComponentOnTangent;

            thisObject.Velocity.x -= 2 * velocityComponentPerpendicularToTangent.x;
            thisObject.Velocity.y -= 2 * velocityComponentPerpendicularToTangent.y;
        }
        else
        {
            return;
        }
    }

    public void StopOnCollide(BasicPhysicsObject toStop, BasicPhysicsObject other) // Faz o objeto parar de se mover caso colida
    {
        //TOFIX Ao colidir com as paredes, o player se afasta muito da parede

        
        float distanceBetweenCircles = Vector2.Distance(toStop.transform.position, other.gameObject.transform.position);

        float angle = Mathf.Atan2(toStop.transform.position.x - other.gameObject.transform.position.x, toStop.transform.position.y - other.gameObject.transform.position.y);

        float distanceToMove = (toStop.transform.localScale.x + other.transform.localScale.x) - distanceBetweenCircles;

        toStop.transform.position += new Vector3(0, Mathf.Cos(angle) * distanceToMove / 4, 0); //divindo por 4 para reduzir a distancia a qual o objeto se afasta da parede ao colidir
        
    }
    #endregion
}