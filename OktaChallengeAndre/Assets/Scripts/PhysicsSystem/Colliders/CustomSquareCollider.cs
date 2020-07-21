using UnityEngine;

/// <summary>
/// Classe filha do BasicPhysicsObject, responsavel por:
/// 
/// Descrever as propriedades de um quadrado, para ser utilizada durante os calculos de colisão
/// Chamar as funções de teste de colisão vindas da interface ICustomCollision (que está na classe pai)
/// levando em consideração se a colisão é entre quadrados ou entre um quadrado e um circulo.
/// 
/// </summary>
public class CustomSquareCollider : BasicPhysicsObject
{
    public Vector2 size;

    public float TopEdge;      
    public float BottomEdge;
    public float LeftEdge;
    public float RightEdge;

    void Update()
    {
        size = new Vector2(transform.localScale.x, transform.localScale.y);

        TopEdge = transform.position.y + transform.localScale.y / 2; // Calculo da borda superior do quadrado.                  
        BottomEdge = transform.position.y - transform.localScale.y / 2; // Calculo da borda inferior do quadrado.  
        LeftEdge = transform.position.x - transform.localScale.x / 2; // Calculo da borda esquerda do quadrado.  
        RightEdge = transform.position.x + transform.localScale.x / 2; // Calculo da borda direita do quadrado.  

        foreach (BasicPhysicsObject Other in iCustomCollision.collidersInScene)
        {
            if (this.PhysicsProperties != null && !this.PhysicsProperties.isStatic && Other != null)
            {
                if (Other is CustomCircleCollider)
                {
                    iCustomCollision.CollisionBetweenSquareAndCircle(this, (CustomCircleCollider)Other);
                }
                else if (Other is CustomSquareCollider)
                {
                    iCustomCollision.CollisionBetweenSquareAndSquare(this, (CustomSquareCollider)Other);
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
        Gizmos.DrawWireCube(gameObject.transform.position, new Vector3(size.x, size.y, 1));
    }
}
