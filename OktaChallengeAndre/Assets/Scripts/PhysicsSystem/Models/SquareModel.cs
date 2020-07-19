using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SquareModel : BasicPhysicsObject
{
    public Vector2 size;

    public float TopEdge;      
    public float BottomEdge;
    public float LeftEdge;
    public float RightEdge;

    void Update()
    {
        //base.FixedUpdate();
        base.Movement();

        size = new Vector2(transform.localScale.x, transform.localScale.y);

        /*Pegando as bordas deste quadrado*/
        TopEdge = transform.position.y + transform.localScale.y / 2; // Calculo da borda superior deste objeto.                  
        BottomEdge = transform.position.y - transform.localScale.y / 2; // Calculo da borda inferior deste objeto.
        LeftEdge = transform.position.x - transform.localScale.x / 2; // Calculo da borda esquerda deste objeto.
        RightEdge = transform.position.x + transform.localScale.x / 2; // Calculo da borda direita deste objeto.
        
        foreach (BasicPhysicsObject bpo in iCustomCollision.collidersInScene)
        {
            //Otimizar
            if (bpo.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
            {

                if (((1 << bpo.gameObject.layer) & CollidableLayerMask) != 0)
                {
                    if (bpo is CircleModel)
                    {
                        iCustomCollision.CollisionBetweenCircleAndSquare((CircleModel)bpo, this);
                    }
                    else if (bpo is SquareModel)
                    {
                        iCustomCollision.CollisionBetweenSquareAndSquare(this, (SquareModel)bpo);
                    }
                    else
                    {
                        UnityEngine.Debug.LogError($"Colisor do tipo {bpo.GetType()} : {bpo.gameObject.name}");
                    }
                }
            }
            else
            {
                return;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(size.x, size.y, 1));
    }
}
