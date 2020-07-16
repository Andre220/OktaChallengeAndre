using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleModel))]
public class CircleCollision : MonoBehaviour
{
    public event Action OnSquareCollision;
    public event Action OnCircleCollision;

    //public Circle thisCircle;
    private CircleModel[] collidablesCircles;
    private SquareModel[] collidablesSquares;

    private CircleModel thisCircle;

    void Start()
    {
        thisCircle = gameObject.GetComponent<CircleModel>();
        collidablesCircles = FindObjectsOfType<CircleModel>();
        collidablesSquares = FindObjectsOfType<SquareModel>();

        foreach (SquareModel s in collidablesSquares)
        {
            print(s.gameObject.name);
        }
    }

    void Update()
    {
        foreach (CircleModel c in collidablesCircles)
        {
            if (c == thisCircle)
            {
                return;
            }
            else
            {
                CollisionCircleToCircle(c);
            }
        }

        foreach (SquareModel s in collidablesSquares)
        {
            print(s.gameObject.name);
            CollisionCircleToSquare(s);
        }
    }

    //Este script deve ser chamado de um circulo, para detectar quando ele colide com um quadrado
    void CollisionCircleToSquare(SquareModel other)
    {
        print("Chamou CircleToSquare");
        float x = thisCircle.transform.position.x; //cordenada X deste circulo
        float y = thisCircle.transform.position.y; //cordenada Y deste circulo       

        /*Testando se as bordas do circulo (circulo + raio) estão sobreponto alguma das bordas do quadrilatero em questão*/
        if (x + thisCircle.CircleRadius > other.LeftEdge && x - thisCircle.CircleRadius < other.RightEdge 
            && (y + thisCircle.CircleRadius > other.BottomEdge && other.TopEdge > y - thisCircle.CircleRadius ||
                y - thisCircle.CircleRadius < other.TopEdge && other.BottomEdge < y + thisCircle.CircleRadius))
        {
            //print($"Circulo {gameObject.name} colidiu com {gameObject.name}");

            if (this.thisCircle.bounce) 
            {
                print("teste");
                BounceCollision(other);
            }

            //OnSquareCollision?.Invoke();
        }
    }

    void CollisionCircleToCircle(CircleModel other)
    {
        float distanceBetweenCircles = Vector2.Distance(thisCircle.transform.position, other.gameObject.transform.position);

        if (distanceBetweenCircles < thisCircle.transform.localScale.x / 2 + other.gameObject.transform.localScale.x / 2)
        {
            //Colisao ocorreu

            float angle = Mathf.Atan2(thisCircle.transform.position.x - other.gameObject.transform.position.x, thisCircle.transform.position.y - other.gameObject.transform.position.y);

            float distanceToMove = (thisCircle.CircleRadius + other.CircleRadius) - distanceBetweenCircles;

            Vector3 movement = new Vector3(angle * distanceBetweenCircles * Time.deltaTime //* thisCircle.Velocity.x
               , angle * distanceBetweenCircles * Time.deltaTime //* thisCircle.Velocity.y
               , 0);

            if (other.isStatic)
            {
                print("is static");
                //thisCircle.Velocity = new Vector2(-thisCircle.Velocity.x, -thisCircle.Velocity.y);
            }
            else if (other.bounce)
            {
                BounceCollision(other);
            }
            else
            {
                print("is not static");
                other.gameObject.transform.position -= movement;
            }

            //c1.gameObject.transform.position += movement;
            //print(other.gameObject.name);
            //OnCircleCollision?.Invoke();
        }
    }

    void BounceCollision(CircleModel other)
    {
        Vector2 tangent;
        tangent.y = -(other.transform.position.x - thisCircle.transform.position.x);
        tangent.x = other.transform.position.x - thisCircle.transform.position.y;

        tangent.Normalize();

        Vector2 relativeVelocity = new Vector2(thisCircle.Velocity.x - other.Velocity.x, thisCircle.Velocity.y - other.Velocity.y);

        float lenght = Vector2.Dot(relativeVelocity, tangent);

        Vector2 velocityComponentOnTangent;
        velocityComponentOnTangent = tangent * lenght;

        Vector2 velocityComponentPerpendicularToTangent = relativeVelocity - velocityComponentOnTangent;

        thisCircle.Velocity.x -= velocityComponentPerpendicularToTangent.x;
        thisCircle.Velocity.y -= velocityComponentPerpendicularToTangent.y;

        other.Velocity.x -= velocityComponentPerpendicularToTangent.x;
        other.Velocity.y -= velocityComponentPerpendicularToTangent.y;
    }

    void BounceCollision(SquareModel other)
    {
        Vector2 tangent;
        tangent.y = -(other.transform.position.x - thisCircle.transform.position.x);
        tangent.x = other.transform.position.x - thisCircle.transform.position.y;

        tangent.Normalize();

        Vector2 relativeVelocity = new Vector2(thisCircle.Velocity.x - other.Velocity.x, thisCircle.Velocity.y - other.Velocity.y);

        float lenght = Vector2.Dot(relativeVelocity, tangent);

        Vector2 velocityComponentOnTangent;
        velocityComponentOnTangent = tangent * lenght;

        Vector2 velocityComponentPerpendicularToTangent = relativeVelocity - velocityComponentOnTangent;

        thisCircle.Velocity.x -= velocityComponentPerpendicularToTangent.x;
        thisCircle.Velocity.y -= velocityComponentPerpendicularToTangent.y;

        other.Velocity.x -= velocityComponentPerpendicularToTangent.x;
        other.Velocity.y -= velocityComponentPerpendicularToTangent.y;
    }
}