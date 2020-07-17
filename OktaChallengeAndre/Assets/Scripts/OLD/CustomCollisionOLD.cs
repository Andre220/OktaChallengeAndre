using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Algoritmo de colisao entre circulos - e ricochete - da pagina:
/// http://flatredball.com/documentation/tutorials/math/circle-collision/
/// Codigo em XNA, alguma modificacoes basicas foram necessarias
/// 
/// Algoritmo de colisao entre quadrados da minha pagina do github:
/// https://github.com/Andre220/FroggerLearningGame/blob/master/frogger%20c%C3%B3digo.html
/// logica simples de colisao, calculando a posição das bordas do quadrado e 
/// comparando com a posição da borda de outros quadrados
/// 
/// </summary>

public class CustomCollisionOLD : MonoBehaviour
{
    public static CustomCollision instance;

    public static List<BasicPhysicsObject> sceneCollidables;

    public LayerMask CollidableLayerMask;

    public event Action<BasicPhysicsObject> OnCustomCollision;

    void Start()
    {
        foreach (BasicPhysicsObject b in FindObjectsOfType<BasicPhysicsObject>())
        {
            if (b.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
            {
                sceneCollidables.Add(b);
            }
        }
    }

    void Update()
    {
        foreach (BasicPhysicsObject b in sceneCollidables)
        {
            if (Vector2.Distance(b.transform.position, gameObject.transform.position) < 2)
            {
                CollisionCalculation(b);
            }
        }
        //print(CollidableLayerMask);
    }

    public void AddCollider(BasicPhysicsObject po)
    {
        sceneCollidables.Add(po);
    }

    public void RemoveCollider(BasicPhysicsObject po)
    {
        sceneCollidables.Remove(po);
    }

    // Update is called once per frame


    public void CollisionCalculation(BasicPhysicsObject other)
    {
        if (other is SquareModel)
        {
            SquareModel Other = (SquareModel)other;
            CircleModel thisCircle = gameObject.GetComponent<CircleModel>();

            if (thisCircle != null)
            {
                CollisionBetweenCircleAndSquare(thisCircle, Other);
            }
            else
            {
                CollisionBetweenSquareAndSquare(gameObject.GetComponent<SquareModel>(), Other);
            }
        }
        else if (other is CircleModel)
        {
            CircleModel Other = (CircleModel)other;
            SquareModel thisSquare = gameObject.GetComponent<SquareModel>();

            if (thisSquare != null)
            {
                CollisionBetweenCircleAndSquare(Other, thisSquare);
            }
            else
            {
                CollisionBetweenCircleAndCircle(gameObject.GetComponent<CircleModel>(), Other);
            }
        }
        else
        {
            Debug.LogError("Tipo de colisor não identificado");
        }
    }

    #region Calculando colisao com base no formato dos objetos
    void CollisionBetweenCircleAndSquare(CircleModel circle, SquareModel square)
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

            if (circle.bounce)
            {
                //print("Bounce");
                BounceCollision(circle, square);
            }

            print($"Circle {circle.gameObject.name} and square {square.gameObject.name} Collided");
            //OnSquareCollision?.Invoke();
        }
    }

    void CollisionBetweenCircleAndCircle(CircleModel circleA, CircleModel circleB)
    {
        float distanceBetweenCircles = Vector2.Distance(circleA.transform.position, circleB.gameObject.transform.position);

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
            else
            {
                //print("not bounce and not static");
                //other.gameObject.transform.position -= movement;
            }

            //c1.gameObject.transform.position += movement;
            print($"Circle {circleA.gameObject.name} and Circle {circleB.gameObject.name} Collided");
            //OnCircleCollision?.Invoke();
        }
    }

    void CollisionBetweenSquareAndSquare(SquareModel squareA, SquareModel squareB)
    {
        if ((squareA.LeftEdge < squareB.RightEdge && squareB.RightEdge < squareA.RightEdge) &&
           (squareA.TopEdge > squareB.BottomEdge && squareB.BottomEdge > squareA.BottomEdge || 
           squareA.BottomEdge < squareB.TopEdge && squareB.TopEdge < squareA.TopEdge))//Collision from left
        {
            print($"Square {squareA.gameObject.name} and square {squareB.gameObject.name} Collided");
            BounceCollision(squareA, squareB);
            //OnSquareCollision?.Invoke();
        }
        else if ((squareA.RightEdge > squareB.LeftEdge && squareB.LeftEdge > squareA.LeftEdge) &&
        (squareA.TopEdge > squareB.BottomEdge && squareB.BottomEdge > squareA.BottomEdge 
        || squareA.BottomEdge < squareB.TopEdge && squareB.TopEdge < squareA.TopEdge))//Collision from right
        {
            print($"Square {squareA.gameObject.name} and square {squareB.gameObject.name} Collided");
            BounceCollision(squareA, squareB);
            //OnSquareCollision?.Invoke();
        }
    }
    #endregion

    #region Custom Behaviour
    void BounceCollision(BasicPhysicsObject thisObject, BasicPhysicsObject other)
    {
        //Bugado - A cada richochete, a velocidade aumenta, até um ponto onde se move tão rápido a ponto da física não detectar

        Vector2 tangent;
        tangent.y = -(other.transform.position.x - thisObject.transform.position.x);
        tangent.x = other.transform.position.y - thisObject.transform.position.y;

        tangent.Normalize();

        Vector2 relativeVelocity = new Vector2(thisObject.Velocity.x - other.Velocity.x, thisObject.Velocity.y - other.Velocity.y);

        float lenght = Vector2.Dot(relativeVelocity, tangent);

        Vector2 velocityComponentOnTangent;
        velocityComponentOnTangent = tangent * lenght;

        Vector2 velocityComponentPerpendicularToTangent = relativeVelocity - velocityComponentOnTangent;

        thisObject.Velocity.x -= Mathf.Clamp(2 * velocityComponentPerpendicularToTangent.x, -2,2); // fazendo os valores de velocidade terem um maximo. Acima desses valores maximos a chance deles atravessarem outros objetos e grande
        thisObject.Velocity.y -= Mathf.Clamp(2 * velocityComponentPerpendicularToTangent.y, -2,2);

        //other.Velocity.x -= velocityComponentPerpendicularToTangent.x;
        //other.Velocity.y -= velocityComponentPerpendicularToTangent.y;
    }
    #endregion
}
