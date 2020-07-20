using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Timeline;
using Zenject;
public enum ColliderType { Circle, Square}


[HideInInspector] // Hide in inspector previne este script de ser exibido pelo inspector quando tentamos adicionar um novo script a um GameObject
public class BasicPhysicsObject : MonoBehaviour 
{
    public ColliderType colliderType;

    public Vector3 Velocity;

    public LayerMask CollidableLayerMask;

    public CustomPhysicsProperties PhysicsProperties;

    /// <summary>
    /// Otimização para colisão. Chamando a função de teste de colisão apenas para GameObjects nas layers que o dev dfinir que dem colidir
    /// </summary>
    protected List<BasicPhysicsObject> CollidersInLayerMask = new List<BasicPhysicsObject>(); 

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

    public void Start()
    {
        foreach (BasicPhysicsObject bpo in iCustomCollision.collidersInScene)
        {
            if (bpo.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
            {
                if ((1 << CollidableLayerMask.value) != (1 << bpo.gameObject.layer)) //bit mask calculos https://www.youtube.com/watch?v=VsmgZmsPV6w
                {
                    CollidersInLayerMask.Add(bpo);
                }
            }
        }
    }

    void Update()
    {
    }

    /// <summary>
    /// Baseado na velocidade deste objeto, esta função movimenta ele levando em consideração também o delta de um frame e outro (Time.DeltaTime)
    /// </summary>
    public void MoveGameObject()
    {
        gameObject.transform.position += Velocity * Time.deltaTime;
    }

    /// <summary>
    /// Retornando o BasicPhysicsObject pois assim podemos retornar qualquer classe derivada dele (CircleCustomCollider e SquareCustomCollider) 
    /// e tambem conseguimos acessar o GameObject em que o colisor esta anexado.
    /// </summary>
    public void OnCustomCollision(BasicPhysicsObject collision)     
    {
        OnCollision?.Invoke(collision.gameObject);
    }

    /// <summary>
    /// Chamado quando chamamos o reset Component no Inspector e/ou quando adicionamos o componente no GameObject
    /// </summary>
    private void Reset()
    {
        if (this.GetType() == typeof(BasicPhysicsObject))
        {
            Debug.LogError("BasicPhysicsObject não deve ser atribuido a um GameObject. Ao invés disso, utilize CircleModel ou SquareModel");
            DestroyImmediate(this);
        }
    }

    public class Factory : PlaceholderFactory<BasicPhysicsObject> { }  //Informações vindas do vídeo: https://www.youtube.com/watch?v=Gp2_8ihvnvA
}