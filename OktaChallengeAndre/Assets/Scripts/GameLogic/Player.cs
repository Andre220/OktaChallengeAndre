using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(VerticalMovement))]
[RequireComponent(typeof(AimAndShoot))]
public class Player : MonoBehaviour
{
    //[Inject]
    //public PlayerInfo playerInfo;

    public AimAndShoot PlayerAim;
    public VerticalMovement PlayerMovement;

    void Start()
    {
        PlayerAim = this.gameObject.GetComponent<AimAndShoot>();
        PlayerMovement = this.gameObject.GetComponent<VerticalMovement>();
    }

    public void MyTurn(bool myTurn)
    {
        gameObject.GetComponent<BasicPhysicsObject>().Velocity = Vector2.zero;
        PlayerAim.enabled = myTurn;
        PlayerMovement.enabled = myTurn;
    }
}
