using UnityEngine;

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

        /*Pegando as bordas deste quadrado*/
        TopEdge = transform.position.y + transform.localScale.y / 2; // Calculo da borda superior deste objeto.                  
        BottomEdge = transform.position.y - transform.localScale.y / 2; // Calculo da borda inferior deste objeto.
        LeftEdge = transform.position.x - transform.localScale.x / 2; // Calculo da borda esquerda deste objeto.
        RightEdge = transform.position.x + transform.localScale.x / 2; // Calculo da borda direita deste objeto.
        
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
