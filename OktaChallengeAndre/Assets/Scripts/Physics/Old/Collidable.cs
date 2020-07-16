using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    ////public Transform CollidableTransform;

    //public bool isCircle; //if not, it`s a square


    //void Start()
    //{
    //    //CollidableTransform = this.gameObject.transform;
    //}

    //void Update()
    //{
    //    Collidable[] collidables = GameObject.FindObjectsOfType<Collidable>();

    //    foreach (Collidable c in collidables)
    //    {
    //        if (c == this)
    //        {
    //            return;
    //        }
    //        else
    //        {
    //            if (isCircle)
    //                CircleCollision(c.transform);
    //            else
    //                BoxCollision(c.transform);
    //        }
    //    }
    //}

    //public bool BoxCollision(Transform other.)
    //{
    //    float TopEdge = transform.position.y + transform.localScale.y / 2; // Calculo da borda superior deste objeto.                          
    //    float BottomEdge = transform.position.y - transform.localScale.y / 2; // Calculo da borda inferior deste objeto.
    //    float LeftEdge = transform.position.x - transform.localScale.x / 2; // Calculo da borda esquerda deste objeto.
    //    float RightEdge = transform.position.x + transform.localScale.x / 2; // Calculo da borda direita deste objeto.

    //    float other.TopEdge = transform.position.y + transform.localScale.y / 2; // Calculo da borda superior do objeto colisior.
    //    float other.BottomEdge = transform.position.y - transform.localScale.y / 2; // Calculo da borda inferior do objeto colisior.
    //    float other.LeftEdge = transform.position.x - transform.localScale.x / 2; // Calculo da borda esquerda do objeto colisior.
    //    float other.RightEdge = transform.position.x + transform.localScale.x / 2; // Calculo da borda direita do objeto colisior.

    //    if ((LeftEdge < other.RightEdge && other.RightEdge < RightEdge) &&
    //       (TopEdge > other.BottomEdge && other.BottomEdge > BottomEdge || BottomEdge < other.TopEdge && other.TopEdge < TopEdge))//Collision from left
    //    {
    //            print($"L:{LeftEdge}|R:{RightEdge}|LO:{other.LeftEdge}|RO:{other.RightEdge}");
    //            return true;
    //    }
    //    else if ((RightEdge > other.LeftEdge && other.LeftEdge > LeftEdge) &&
    //            (TopEdge > other.BottomEdge && other.BottomEdge > BottomEdge || BottomEdge < other.TopEdge && other.TopEdge < TopEdge))//Collision from right
    //    {
    //        print($"L:{LeftEdge}|R:{RightEdge}|LO:{other.LeftEdge}|RO:{other.RightEdge}");
    //        return true;
    //    }
    //    else //nao houve colisao
    //    {
    //        return false;
    //    }
    //}

    //public bool CircleCollision(Transform other.)
    //{
    //    if(Vector2.Distance(transform.position, transform.position) < transform.localScale.x / 2 || 
    //        Vector2.Distance(transform.position, transform.position) < transform.localScale.x / 2) 
    //    {
    //        print(Vector2.Distance(transform.position, transform.position));
    //        return true;
    //    }
    //    else
    //        return false;
    //}
}
