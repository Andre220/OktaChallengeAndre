using System;
using Zenject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class CustomCollision : ICustomCollision
{
    public List<BasicPhysicsObject> collidersInScene { get; }

    public event Action<BasicPhysicsObject> OnCollisionEvent;

    #region definições da classe e suas propriedades
    public CustomCollision()
    {
        collidersInScene = new List<BasicPhysicsObject>();
    }

    public void AddColliderToBuffer(BasicPhysicsObject bpo)
    {
        //UnityEngine.MonoBehaviour.print(bpo.gameObject.name);
        collidersInScene.Add(bpo);
    }

    public void RemoveColliderFromBuffer(BasicPhysicsObject bpo)
    {
        collidersInScene.Remove(bpo);
    }
    #endregion

    public void OnCollision(BasicPhysicsObject callerCollider)
    {
        //TO DO - pegar apenas os colisores que estão na mesma layer do BasicPhysicsObject
        //Esta lógica esta sendo feita pelo circleCollider ou Square Collider. Ideal fazer aqui

        //foreach (BasicPhysicsObject currentCollider in collidersInScene)
        //{


        //    if (callerCollider != currentCollider)
        //    {
        //        if (callerCollider.colliderType == ColliderType.Circle && currentCollider.colliderType == ColliderType.Circle)
        //        {
        //            CollisionBetweenCircleAndCircle(callerCollider.gameObject.GetComponent<CircleModel>(), currentCollider.gameObject.GetComponent<CircleModel>()); ;
        //        }
        //        else if (callerCollider.colliderType == ColliderType.Circle && currentCollider.colliderType == ColliderType.Square)
        //        {
        //            CollisionBetweenCircleAndSquare(callerCollider.gameObject.GetComponent<CircleModel>(), currentCollider.gameObject.GetComponent<SquareModel>());
        //        }
        //        else if (callerCollider.colliderType == ColliderType.Square && currentCollider.colliderType == ColliderType.Circle)
        //        {
        //            CollisionBetweenSquareAndCircle(callerCollider.gameObject.GetComponent<SquareModel>(), currentCollider.gameObject.GetComponent<CircleModel>());
        //        }
        //        else if (callerCollider.colliderType == ColliderType.Square && currentCollider.colliderType == ColliderType.Square)
        //        {
        //            CollisionBetweenSquareAndSquare(callerCollider.gameObject.GetComponent<SquareModel>(), currentCollider.gameObject.GetComponent<SquareModel>());
        //        }
        //        else
        //        {
        //            Debug.LogError($"CustomCollision L55: Game Objects com colisor {callerCollider.colliderType.ToString()} e {currentCollider.colliderType.ToString()} não possuem implementação para testar colisão entre eles. ");
        //        }
        //    }
        //}
    }

    #region Calculando colisao com base no formato dos objetos
    public void CollisionBetweenCircleAndSquare(CircleModel circle, SquareModel square)
    {
        float minDistanceTest = circle.transform.position.x + circle.CircleDiameter - square.transform.position.x + square.size.x;

        if (Vector2.Distance(circle.transform.position, square.transform.position) < 1)
        {
            //print("Chamou CircleToSquare");
            float x = circle.transform.position.x; //cordenada X deste circulo
            float y = circle.transform.position.y; //cordenada Y deste circulo       

            /*Testando se as bordas do circulo (circulo + raio) estão sobreponto alguma das bordas do quadrilatero em questão*/
            if (x + circle.CircleRadius > square.LeftEdge && x - circle.CircleRadius < square.RightEdge
                && (y + circle.CircleRadius > square.BottomEdge && square.TopEdge > y - circle.CircleRadius ||
                    y - circle.CircleRadius < square.TopEdge && square.BottomEdge < y + circle.CircleRadius))
            {
                //print($"Circulo {gameObject.name} colidiu com {gameObject.name}");

                //GameObject go = new GameObject();
                //go.AddComponent<ObjectWithGizmo>();
                //UnityEngine.MonoBehaviour.Instantiate(go, circle.transform.position, Quaternion.identity);

                if (circle.bounce)
                {
                    //UnityEngine.MonoBehaviour.print("Bounce");
                    BounceCollision(circle, square); //Not work properly
                }
                
                if (circle.stopOnCollide || square.stopOnCollide)
                {
                    if (circle.stopOnCollide)
                    {
                        StopOnCollide(circle, square);
                    }
                    else
                    {
                        StopOnCollide(square, circle);
                    }
                    //print("not bounce and not static");
                    //other.gameObject.transform.position -= movement;
                }

                //UnityEngine.MonoBehaviour.print($"Circle {circle.gameObject.name} and square {square.gameObject.name} Collided");
                //OnSquareCollision?.Invoke();

                //OnCollisionHappened?.Invoke();

                MonoBehaviour.print($"{circle.name} | {square.name}");
            }

            OnCollisionEvent?.Invoke(square);
        }
        else
        {
            return;
        }
    }

    public void CollisionBetweenSquareAndCircle(SquareModel square, CircleModel circle)
    {
        float minDistanceTest = circle.transform.position.x + circle.CircleDiameter - square.transform.position.x + square.size.x;

        if (Vector2.Distance(circle.transform.position, square.transform.position) < 1)
        {
            //print("Chamou CircleToSquare");
            float x = circle.transform.position.x; //cordenada X deste circulo
            float y = circle.transform.position.y; //cordenada Y deste circulo       

            /*Testando se as bordas do circulo (circulo + raio) estão sobreponto alguma das bordas do quadrilatero em questão*/
            if (x + circle.CircleRadius > square.LeftEdge && x - circle.CircleRadius < square.RightEdge
                && (y + circle.CircleRadius > square.BottomEdge && square.TopEdge > y - circle.CircleRadius ||
                    y - circle.CircleRadius < square.TopEdge && square.BottomEdge < y + circle.CircleRadius))
            {
                //print($"Circulo {gameObject.name} colidiu com {gameObject.name}");

                //GameObject go = new GameObject();
                //go.AddComponent<ObjectWithGizmo>();
                //UnityEngine.MonoBehaviour.Instantiate(go, circle.transform.position, Quaternion.identity);

                if (circle.bounce)
                {
                    //UnityEngine.MonoBehaviour.print("Bounce");
                    BounceCollision(circle, square); //Not work properly
                }

                if (circle.stopOnCollide || square.stopOnCollide)
                {
                    if (circle.stopOnCollide)
                    {
                        StopOnCollide(circle, square);
                    }
                    else
                    {
                        StopOnCollide(square, circle);
                    }
                    //print("not bounce and not static");
                    //other.gameObject.transform.position -= movement;
                }

                //UnityEngine.MonoBehaviour.print($"Circle {circle.gameObject.name} and square {square.gameObject.name} Collided");
                //OnSquareCollision?.Invoke();

                //OnCollisionHappened?.Invoke();
            }

            //OnCollisionEvent?.Invoke(circle);
        }
        else
        {
            return;
        }
    }

    public void CollisionBetweenCircleAndCircle(CircleModel circleA, CircleModel circleB)
    {
        float minDistanceTest = circleA.transform.position.x + circleA.CircleDiameter - circleB.transform.position.x + circleB.CircleDiameter;

        float distanceBetweenCircles = Vector2.Distance(circleA.transform.position, circleB.gameObject.transform.position);

        if (distanceBetweenCircles < 1)
        {

            if (distanceBetweenCircles < circleA.transform.localScale.x / 2 + circleB.gameObject.transform.localScale.x / 2)
            {
                //Colisao ocorreu

                float angle = Mathf.Atan2(circleA.transform.position.x - circleB.gameObject.transform.position.x, circleA.transform.position.y - circleB.gameObject.transform.position.y);

                float distanceToMove = (circleA.CircleRadius + circleB.CircleRadius) - distanceBetweenCircles;

                Vector3 movement = new Vector3(angle * distanceBetweenCircles * Time.deltaTime //* thisCircle.Velocity.x
                    , angle * distanceBetweenCircles * Time.deltaTime //* thisCircle.Velocity.y
                    , 0);

                if (circleB.isStatic)
                {
                    //print("is static");
                    //thisCircle.Velocity = new Vector2(-thisCircle.Velocity.x, -thisCircle.Velocity.y);
                }
                else if (circleB.bounce)
                {
                    //print("bounce");
                    BounceCollision(circleA, circleB);
                }
                else if (circleB.stopOnCollide || circleA.stopOnCollide)
                {
                    if (circleA.stopOnCollide)
                    {
                        StopOnCollide(circleA, circleB);
                    }
                    else
                    {
                        StopOnCollide(circleB, circleA);
                    }
                    //print("not bounce and not static");
                    //other.gameObject.transform.position -= movement;
                }

                //c1.gameObject.transform.position += movement;
                //UnityEngine.MonoBehaviour.print($"Circle {circleA.gameObject.name} and Circle {circleB.gameObject.name} Collided");
                //OnCircleCollision?.Invoke();

            }

            OnCollisionEvent?.Invoke(circleB);
        }
        else
        {
            return;
        }
    }

    public void CollisionBetweenSquareAndSquare(SquareModel squareA, SquareModel squareB)
    {
        float minDistanceTest = squareA.transform.position.x + squareA.size.x - squareB.transform.position.x + squareB.size.x;

        if (Vector2.Distance(squareA.transform.position, squareB.transform.position) < 1)
        {
            if ((squareA.LeftEdge < squareB.RightEdge && squareB.RightEdge < squareA.RightEdge) &&
            (squareA.TopEdge > squareB.BottomEdge && squareB.BottomEdge > squareA.BottomEdge ||
            squareA.BottomEdge < squareB.TopEdge && squareB.TopEdge < squareA.TopEdge))//Collision from left
            {
                UnityEngine.MonoBehaviour.print($"Square {squareA.gameObject.name} and square {squareB.gameObject.name} Collided");
                
                if (squareA.bounce || squareB.bounce)
                {
                    BounceCollision(squareA, squareB);
                }
                else if (squareA.stopOnCollide || squareB.stopOnCollide)
                {
                    if (squareA.stopOnCollide)
                    {
                        StopOnCollide(squareA, squareB);
                    }
                    else
                    {
                        StopOnCollide(squareB, squareA);
                    }
                    //print("not bounce and not static");
                    //other.gameObject.transform.position -= movement;
                }

                //OnSquareCollision?.Invoke();
            }
            else if ((squareA.RightEdge > squareB.LeftEdge && squareB.LeftEdge > squareA.LeftEdge) &&
            (squareA.TopEdge > squareB.BottomEdge && squareB.BottomEdge > squareA.BottomEdge
            || squareA.BottomEdge < squareB.TopEdge && squareB.TopEdge < squareA.TopEdge))//Collision from right
            {
                UnityEngine.MonoBehaviour.print($"Square {squareA.gameObject.name} and square {squareB.gameObject.name} Collided");
                BounceCollision(squareA, squareB);
                //OnSquareCollision?.Invoke();
            }
            
            //OnCollisionEvent?.Invoke(squareB);
        }
        else
        {
            return;
        }           
    }
    #endregion

    #region Custom Behaviour
    public void BounceCollision(BasicPhysicsObject thisObject, BasicPhysicsObject other)
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

        thisObject.Velocity.x -= Mathf.Clamp(2 * velocityComponentPerpendicularToTangent.x, -2, 2); // fazendo os valores de velocidade terem um maximo. Acima desses valores maximos a chance deles atravessarem outros objetos e grande
        thisObject.Velocity.y -= Mathf.Clamp(2 * velocityComponentPerpendicularToTangent.y, -2, 2);

        thisObject.Velocity.x -= velocityComponentPerpendicularToTangent.x; // fazendo os valores de velocidade terem um maximo. Acima desses valores maximos a chance deles atravessarem outros objetos e grande
        thisObject.Velocity.y -= velocityComponentPerpendicularToTangent.y;


        //other.Velocity.x -= velocityComponentPerpendicularToTangent.x;
        //other.Velocity.y -= velocityComponentPerpendicularToTangent.y;
    }

    public void StopOnCollide(BasicPhysicsObject toStop, BasicPhysicsObject other) // Faz o objeto parar de se mover caso colida
    {
        //TODO Ao colidir com as paredes, o player se afasta muito da parede

        float distanceBetweenCircles = Vector2.Distance(toStop.transform.position, other.gameObject.transform.position);

        float angle = Mathf.Atan2(toStop.transform.position.x - other.gameObject.transform.position.x, toStop.transform.position.y - other.gameObject.transform.position.y);

        float distanceToMove = (toStop.transform.localScale.x / 2 + other.transform.localScale.x / 2) - distanceBetweenCircles;

        toStop.transform.position += new Vector3(0, Mathf.Cos(angle) * distanceBetweenCircles, 0);
    }
    #endregion
}

public class CollisionInfo
{
    public GameObject collisionOther;
}
