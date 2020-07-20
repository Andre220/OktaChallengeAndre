using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VerticalMovement))]
[RequireComponent(typeof(AimBehaviour))]
public class Player : MonoBehaviour
{
    public AimBehaviour PlayerAim;
    public VerticalMovement PlayerMovement;

    void Start()
    {
        PlayerAim = this.gameObject.GetComponent<AimBehaviour>();
        PlayerMovement = this.gameObject.GetComponent<VerticalMovement>();
    }

    public void MyTurn(bool myTurn)
    {
        gameObject.GetComponent<BasicPhysicsObject>().Velocity = Vector2.zero;
        PlayerAim.enabled = myTurn;
        PlayerMovement.enabled = myTurn;
    }
}
