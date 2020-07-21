using UnityEngine;

/// <summary>
/// Responsavel por habilitar/desabilitar o monobehaviour de cada comportamento basico (AimAndShoot e VerticalMovement) do player,
/// alem de armazenar uma referencia para os mesmos.
/// </summary>

[RequireComponent(typeof(VerticalMovement))]
[RequireComponent(typeof(AimAndShoot))]
public class Player : MonoBehaviour
{
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
