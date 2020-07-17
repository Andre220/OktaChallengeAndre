using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Timeline;
using Zenject;

[HideInInspector]
public class BasicPhysicsObject : MonoBehaviour
{
    public Vector3 Velocity;

    public bool isStatic;
    public bool bounce;
    public bool stopOnCollide;

    public LayerMask CollidableLayerMask;

    [Inject]
    public ICustomCollision iCustomCollision; //CollisionService

    [Inject]
    public void Construct(ICustomCollision _iCustomCollision)
    {
        this.iCustomCollision = _iCustomCollision;
    }


    //public void Init(ICustomCollision _collisionService)
    //{
    //    iCustomCollision = _collisionService;
    //}

    void OnEnable()
    {
        iCustomCollision.AddColliderToBuffer(this);
        //print(this.gameObject.name);
    }

    void OnDisable()
    {
        iCustomCollision.RemoveColliderToBuffer(this);
    }

    void OnDestroy()
    {
        iCustomCollision.RemoveColliderToBuffer(this);
    }

    void Update()
    {
    }

    public void Movement()
    {
        gameObject.transform.position += Velocity * Time.deltaTime;
    }

    private void Reset()
    {
        if (this.GetType() == typeof(BasicPhysicsObject))
        {
            Debug.LogError("BasicPhysicsObject não deve ser atribuido a um GameObject. Ao invés disso, utilize CircleModel ou SquareModel");
            DestroyImmediate(this);
        }
    }

    //Informações vindas do vídeo: https://www.youtube.com/watch?v=Gp2_8ihvnvA
    public class Factory : PlaceholderFactory<BasicPhysicsObject> {}
}