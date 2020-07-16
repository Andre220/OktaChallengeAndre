using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SquareModel))]
public class SquareCollision : MonoBehaviour
{
    public event Action OnSquareCollision;
    public event Action OnCircleCollision;

    private CircleModel[] collidablesCircles;
    private SquareModel[] collidablesSquares;

    private SquareModel thisSquare;

    void Start()
    {
        thisSquare = gameObject.GetComponent<SquareModel>();
        collidablesCircles = FindObjectsOfType<CircleModel>();
        collidablesSquares = FindObjectsOfType<SquareModel>();
    }

    void Update()
    {
        foreach (CircleModel c in collidablesCircles)
        {
            if (c == this)
            {
                return;
            }
            else
            {
                CollisionSquareToCircle(c);
            }
        }

        foreach (SquareModel s in collidablesSquares)
        {
            CollisionSquareToSquare(s);
        }
    }

    void CollisionSquareToCircle(CircleModel other)
    {
        //float distanceBetweenCircles = Vector2.Distance(thisCircle.transform.position, other..transform.position);

        //if (distanceBetweenCircles < thisCircle.transform.localScale.x / 2 + other..transform.localScale.x / 2)
        //{

        //    //Colisao ocorreu

        //    float angle = Mathf.Atan2(thisCircle.transform.position.x - other..transform.position.x, thisCircle.transform.position.y - other..transform.position.y);

        //    float distanceToMove = (thisCircle.transform.localScale.x / 2 + other..transform.localScale.x / 2) - distanceBetweenCircles;

        //    /*Vector3 movement = new Vector3(angle * distanceBetweenCircles * Time.deltaTime * CircleSpeed,
        //        angle * distanceBetweenCircles * Time.deltaTime * CircleSpeed, 0);*/

        //    //c1.gameObject.transform.position += movement;
        //    //c2.gameObject.transform.position += movement;
        //}
    }

    //Este script deve ser chamado de um circulo, para detectar quando ele colide com um quadrado
    void CollisionSquareToSquare(SquareModel other)
    {
        if ((thisSquare.LeftEdge < other.RightEdge && other.RightEdge < thisSquare.RightEdge) &&
        (thisSquare.TopEdge > other.BottomEdge && other.BottomEdge > thisSquare.BottomEdge || thisSquare.BottomEdge < other.TopEdge && other.TopEdge < thisSquare.TopEdge))//Collision from left
        {
            print($"L:{thisSquare.LeftEdge}|R:{thisSquare.RightEdge}|LO:{other.LeftEdge}|RO:{other.RightEdge}");
            OnSquareCollision?.Invoke();
        }
        else if ((thisSquare.RightEdge > other.LeftEdge && other.LeftEdge > thisSquare.LeftEdge) &&
        (thisSquare.TopEdge > other.BottomEdge && other.BottomEdge > thisSquare.BottomEdge || thisSquare.BottomEdge < other.TopEdge && other.TopEdge < thisSquare.TopEdge))//Collision from right
        {
            print($"L:{thisSquare.LeftEdge}|R:{thisSquare.RightEdge}|LO:{other.LeftEdge}|RO:{other.RightEdge}");
            OnSquareCollision?.Invoke();
        }
    }
}
