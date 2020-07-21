using GameEventBus.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Evento chamado quando uma bala e destruida.
/// 
/// Para o cenario atual, nao existe necessidade de enviar informacoes da bala.
/// 
/// Caso seja necessario no futuro, basta adicionar as propriedades aqui e colocar estas propriedades no construtor.
/// 
/// Exemplo em https://github.com/ThomasKomarnicki/GameEventBus
/// </summary>


public class OnBulletDestroyed : EventBase
{
    public OnBulletDestroyed()
    {

    }
}
