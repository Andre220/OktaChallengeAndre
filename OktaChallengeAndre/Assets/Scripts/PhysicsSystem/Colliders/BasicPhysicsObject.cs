using GameEventBus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Timeline;
using Zenject;

/// <summary>
/// 
/// Este Monobehaviour armazena todas as propriedades básicas e métodos compartilhadas entre circulos e quadrados. 
/// 
/// 
/// 
/// A classe CustomCircleCollider e CustomSquareCollider derivam desta classe, e implementar métodos e 
/// propriedades referentes a sí (como o diametro para o circulo).
/// 
/// OBS: Hide in inspector previne este script de ser exibido pelo inspector 
/// quando tentamos adicionar um novo script a um GameObject.
/// </summary>

[HideInInspector]
public class BasicPhysicsObject : MonoBehaviour 
{
    public Vector3 Velocity;

    public LayerMask CollidableLayerMask;

    public CustomPhysicsProperties PhysicsProperties;

    /// <summary>
    /// OTIMIZAÇÃO PARA A COLISÃO
    /// Esta lista armazena os objetos que é necessário testar a colisão com base na layer.
    /// </summary>
    protected List<BasicPhysicsObject> CollidersInLayerMask = new List<BasicPhysicsObject>(); 

    [Inject]
    public ICustomCollision iCustomCollision; //CollisionService

    void OnEnable()
    {
        iCustomCollision.AddColliderToBuffer(this);
    }

    void OnDestroy()
    {
        iCustomCollision.RemoveColliderFromBuffer(this);
    }


    public void Start()
    {
        foreach (BasicPhysicsObject bpo in iCustomCollision.collidersInScene)
        {
            if (bpo.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())
            {
                //bit mask calculos https://www.youtube.com/watch?v=VsmgZmsPV6w
                if ((1 << CollidableLayerMask.value) != (1 << bpo.gameObject.layer))                 
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
    /// Baseado na velocidade deste objeto, esta função movimenta ele levando 
    /// em consideração também o delta de um frame e outro (Time.DeltaTime)
    /// </summary>
    public void MoveGameObject()
    {
        gameObject.transform.position += Velocity * Time.deltaTime;
    }

    /// <summary>
    /// Retornando o BasicPhysicsObject pois assim podemos retornar qualquer classe derivada dele 
    /// (CircleCustomCollider e SquareCustomCollider) e tambem conseguimos acessar 
    /// o GameObject em que o colisor esta anexado.
    /// </summary>
    public void OnCustomCollision(OnPhysicsObjectCollide collisionInfo)     
    {
    }

    /// <summary>
    /// Chamado quando chamamos o reset Component no Inspector e/ou quando adicionamos o componente no GameObject
    /// Estou impedindo que este monobehviour seja anexado a GameObjects pois suas 
    /// classes filhas (CustomCircleCollider e CustomSquareCollider) é que representam um collider.
    /// </summary>
    private void Reset()
    {
        if (this.GetType() == typeof(BasicPhysicsObject))
        {
            Debug.LogError("BasicPhysicsObject não deve ser atribuido a um GameObject. Ao invés disso, utilize CustomCircleCollider ou CustomSquareCollider");
            DestroyImmediate(this);
        }
    }

    /// <summary>
    /// Consultei essas fontes para entender como trabalhar com o Factory
    /// https://www.youtube.com/watch?v=Gp2_8ihvnvA e na página https://github.com/modesttree/Zenject/blob/master/Documentation/Factories.md
    /// </summary>
    public class Factory : PlaceholderFactory<BasicPhysicsObject> { }  
}