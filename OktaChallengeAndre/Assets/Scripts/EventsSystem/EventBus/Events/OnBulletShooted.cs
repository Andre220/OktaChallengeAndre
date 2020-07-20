using GameEventBus.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
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
