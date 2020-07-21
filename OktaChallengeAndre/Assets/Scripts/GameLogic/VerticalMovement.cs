using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script que muda a velocidade do BasicPhysicsObject com base no input do eixo VERTICAL da Unity.
/// </summary>

public class VerticalMovement : MonoBehaviour
{
    [Tooltip("Velocidade de movimento vertical do player")]
    public float MovementSpeed = 2;

    private float VerticalAxisInput;

    public BasicPhysicsObject bpo;

    void Start()
    {
        try
        {
            bpo = GetComponent<BasicPhysicsObject>();
        }
        catch
        {
            Debug.LogError("BasicPhysicsObject não encontrado. Por favor, adicionar CustomCircleCollider ou CustomSquareCollider");
            DestroyImmediate(this);
        }
    }

    void Update()
    {
        VerticalAxisInput = Input.GetAxisRaw("Vertical");

        bpo.Velocity = new Vector3(0, VerticalAxisInput * MovementSpeed, 0);
    }
}
