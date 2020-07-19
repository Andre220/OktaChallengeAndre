using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimBehaviour))]
[RequireComponent(typeof(VerticalMovement))]
public class Player : MonoBehaviour
{
    public AimBehaviour PlayerAim;
    public VerticalMovement PlayerMovement;

    void Start()
    {
        PlayerAim = gameObject.GetComponent<AimBehaviour>();
        PlayerMovement = gameObject.GetComponent<VerticalMovement>();
    }

    public void MyTurn(bool myTurn)
    {
        PlayerAim.enabled = myTurn;
        PlayerMovement.enabled = myTurn;
    }
}
