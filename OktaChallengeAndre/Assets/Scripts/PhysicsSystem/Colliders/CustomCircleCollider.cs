using UnityEngine;

/// <summary>
/// Classe filha do BasicPhysicsObject, responsavel por:
/// 
/// Descrever as propriedades de um circulo, para ser utilizada durante os calculos de colisão
/// Chamar as funções de teste de colisão vindas da interface ICustomCollision (que está na classe pai)
/// levando em consideração se a colisão é entre Circulos ou entre um circulo e um quadrado.
/// 
/// </summary>
public class CustomCircleCollider : BasicPhysicsObject
{
    public float CircleRadius, CircleDiameter;

    void Update()
    {
        base.MoveGameObject();

        CircleDiameter = gameObject.transform.localScale.x;
        CircleRadius = gameObject.transform.localScale.x / 2;

        foreach (BasicPhysicsObject Other in CollidersInLayerMask)
        {
            if(this.PhysicsProperties != null && !this.PhysicsProperties.isStatic && Other != null)
            {
                if (Other is CustomCircleCollider)
                {
                    iCustomCollision.CollisionBetweenCircleAndCircle(this, (CustomCircleCollider)Other);
                }
                else if (Other is CustomSquareCollider)
                {
                    iCustomCollision.CollisionBetweenCircleAndSquare(this, (CustomSquareCollider)Other);
                }
                else
                {
                    UnityEngine.Debug.LogError($"Colisor do tipo {Other.GetType()} : {Other.gameObject.name}");
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, CircleRadius);
    }
}
