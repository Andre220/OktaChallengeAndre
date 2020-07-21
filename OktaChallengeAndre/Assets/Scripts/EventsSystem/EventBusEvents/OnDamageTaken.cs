using GameEventBus.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Evento chamado quando um player e atingido por um projetil.
/// Para os que assinarem este evento, e retornado a saude atual 
/// do player atingido e quem foi atingido.
/// 
/// Este evento e assinado pelo (HUDManager), que tem interesse em 
/// saber quem foi atingido e quanto de vida restou, para atualizar a HUD.
/// 
/// Este evento e invocado pela classe (StatsManager) que e responsavel
/// por "escutar" eventos de colisao e descobrir se esse evento inclui uma bala
/// e o player em que ela(StatsManager) esta anexada.
/// 
/// Exemplo em https://github.com/ThomasKomarnicki/GameEventBus
/// 
/// </summary>

public class OnDamageTaken : EventBase
{
    public int CurrentHealth;
    public Player PlayerDamaged;

    public OnDamageTaken(int currentHealth, Player playerDamaged)
    {
        CurrentHealth = currentHealth;
        PlayerDamaged = playerDamaged;
    }
}
