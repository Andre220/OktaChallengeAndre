﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareModel : BasicPhysicsObject
{
    public Vector2 size;

    public float TopEdge;      
    public float BottomEdge;
    public float LeftEdge;
    public float RightEdge;

    private void Update()
    {
        size = new Vector2(transform.localScale.x, transform.localScale.y);

        /*Pegando as bordas deste quadrado*/
        TopEdge = transform.position.y + transform.localScale.y / 2; // Calculo da borda superior deste objeto.                  
        BottomEdge = transform.position.y - transform.localScale.y / 2; // Calculo da borda inferior deste objeto.
        LeftEdge = transform.position.x - transform.localScale.x / 2; // Calculo da borda esquerda deste objeto.
        RightEdge = transform.position.x + transform.localScale.x / 2; // Calculo da borda direita deste objeto.
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(size.x, size.y, 1));
    }
}