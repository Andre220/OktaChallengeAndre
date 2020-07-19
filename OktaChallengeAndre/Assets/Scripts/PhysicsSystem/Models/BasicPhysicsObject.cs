using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Timeline;
using Zenject;
public enum ColliderType { Circle, Square}

[HideInInspector]
public class BasicPhysicsObject : MonoBehaviour
{
    public ColliderType colliderType;

    public Vector3 Velocity;

    public bool isStatic;
    public bool bounce;
    public bool stopOnCollide;

    public LayerMask CollidableLayerMask;

    public event Action<GameObject> OnCollision;

    [Inject]
    public ICustomCollision iCustomCollision; //CollisionService

    [Inject]
    public void Construct(ICustomCollision _iCustomCollision)
    {
        this.iCustomCollision = _iCustomCollision;
    }

    void OnEnable()
    {
        iCustomCollision.AddColliderToBuffer(this);
        iCustomCollision.OnCollisionEvent += OnCustomCollision;
    }

    void OnDestroy()
    {
        iCustomCollision.RemoveColliderFromBuffer(this);
        iCustomCollision.OnCollisionEvent -= OnCustomCollision;
    }

    public virtual void FixedUpdate()
    {
        //iCustomCollision.OnCollision(this); // Chamando o método principal da classe CustomCollision, para que os testes de colisão sejam feitos no Update.
    }

    public void Movement()
    {
        gameObject.transform.position += Velocity * Time.deltaTime;
    }

    public void OnCustomCollision(BasicPhysicsObject collision)
    {
        OnCollision?.Invoke(collision.gameObject);
    }

    private void Reset() // Chamado quando chamamos o reset Component no Inspector e/ou quando adicionamos o componente no GameObject
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