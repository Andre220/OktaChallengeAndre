using GameEventBus.Events;
using UnityEngine;

/// <summary>
/// 
/// Evento invocado quando a vida do player e menor que zero. 
/// Retorna o player que esta com a vida menor ou igual a zero (e morto).
/// 
/// Exemplo em https://github.com/ThomasKomarnicki/GameEventBus
/// 
/// </summary>

public class OnPlayerDied : EventBase
{
    public GameObject DeadPlayer;

    public OnPlayerDied(GameObject playerDied)
    {
        DeadPlayer = playerDied;
    }
}
