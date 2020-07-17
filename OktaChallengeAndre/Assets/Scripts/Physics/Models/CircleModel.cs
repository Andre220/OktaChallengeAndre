﻿using System;
using UnityEngine;

public class CircleModel : BasicPhysicsObject
{
    public float CircleRadius;
    public float CircleDiameter;

    void Update()
    {
        base.Movement();

        CircleDiameter = gameObject.transform.localScale.x;
        CircleRadius = gameObject.transform.localScale.x / 2;

        foreach (BasicPhysicsObject bpo in iCustomCollision.collidersInScene)
        {
            //Otimizar
            if (bpo.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
            {
                if (bpo is CircleModel)
                {
                    iCustomCollision.CollisionBetweenCircleAndCircle(this, (CircleModel)bpo);
                }
                else if(bpo is SquareModel)
                {
                    iCustomCollision.CollisionBetweenCircleAndSquare(this, (SquareModel)bpo);
                }
                else
                {
                    UnityEngine.Debug.LogError("Colisor testado não corresponde ao CircleModel nem ao SquareModel");
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
        Gizmos.DrawWireSphere(gameObject.transform.position, CircleRadius);
    }
}
