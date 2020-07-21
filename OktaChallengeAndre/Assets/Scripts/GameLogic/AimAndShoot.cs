using GameEventBus.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Classe responsavel por:
/// 
/// rotacionar o player - e consequentemente a mira dele - conforme o eixo HORIZONTAL da Unity e pressionado
/// 
/// Criar (esta sendo utilizado o Pool do Zenject. Isso significa que ao Spawnar, 
/// a bala e ativada no hierarchy, e ao Despawnar, ela e desativada) a bala ao presionar o botao ESPACO.
/// </summary>
public class AimAndShoot : MonoBehaviour
{
    public BulletStats ChoosedBullet;

    public Transform FirePosition;

    [Inject]
    readonly Bullet.Factory bulletFactory = null;

    [Inject]
    public IEventBus eventBus;

    public float AimRotateSpeed = 50;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        transform.Rotate(Vector3.forward * Input.GetAxisRaw("Horizontal") * AimRotateSpeed * Time.deltaTime, Space.World);
    }

    public void Shoot()
    {
        eventBus.Publish(new OnBulletShooted(gameObject.GetComponent<Player>())); //Evento de quando uma bala e disparada, fazendo todos os jogadores pararem de efetuar inputs.

        var bullet = bulletFactory.Create();

        if (bullet != null)
        {
            bullet.transform.position = FirePosition.transform.position;
            bullet.transform.rotation = gameObject.transform.rotation;

            Bullet info = bullet.GetComponent<Bullet>();
            info.Shooter = gameObject;
            info.bulletInfo = ChoosedBullet;

            SpriteRenderer SR = bullet.GetComponent<SpriteRenderer>();
            SR.color = ChoosedBullet.BulletColor;

            BasicPhysicsObject physicsProperties = bullet.GetComponent<BasicPhysicsObject>();

            physicsProperties.PhysicsProperties = ChoosedBullet.CustomPhysicsPropertys;

            physicsProperties.Velocity = FirePosition.transform.localScale.normalized.x * 
                -transform.right * ChoosedBullet.BulletSpeed;
        }
    }
}
