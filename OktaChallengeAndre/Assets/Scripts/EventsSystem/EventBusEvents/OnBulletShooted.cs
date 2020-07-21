using GameEventBus.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Evento chamado quando um projetil e disparado, 
/// passando para os que assinarem este evento QUEM(Player) fez o disparo.
/// 
/// Exemplo em https://github.com/ThomasKomarnicki/GameEventBus
/// </summary>

public class OnBulletShooted : EventBase
{
    public Player Shooter;

    public OnBulletShooted(Player shooter)
    {
        Shooter = shooter;
    }
}
