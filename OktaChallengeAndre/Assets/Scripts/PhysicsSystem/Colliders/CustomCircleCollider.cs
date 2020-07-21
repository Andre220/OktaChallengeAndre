using UnityEngine;

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
                //print($"{gameObject.name} can collide with {bpo.gameObject.name}");
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
