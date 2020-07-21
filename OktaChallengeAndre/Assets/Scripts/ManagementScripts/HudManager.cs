using GameEventBus.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// "Observa" os eventos relacionados a dano (Player tomou dano e player morreu) e atualiza a HUD conforme esses eventos ocorrem.
/// A Lista que armazena os textos de vida de cada player depende dos indice, o que e um problema.
/// Uma melhoria futura sera criar alguma especie de dicionario, struct ou Scriptable que relacione o player com seu respectivo Text de HP.
/// </summary>

public class HudManager : MonoBehaviour
{
    [Tooltip("Os indice dos textos deve combinar com o indice a lista de players. O Texto 0 deve ser do Jogador no indice 0")]
    public List<Text> PlayerLifeHUD;

    public GameObject GameOverHud;
    public Text GameOverText;

    public GameManager GM;

    [Inject]
    public IEventBus eventBus;

    void OnEnable()
    {
        eventBus.Subscribe<OnDamageTaken>(OnDamageTaken);
        eventBus.Subscribe<OnPlayerDied>(OnPlayerDie);
    }

    private void OnDestroy()
    {
        eventBus.Unsubscribe<OnDamageTaken>(OnDamageTaken);
        eventBus.Unsubscribe<OnPlayerDied>(OnPlayerDie);
    }

    void OnDamageTaken(OnDamageTaken DamageTaken)
    {
        PlayerLifeHUD[GM.Players.IndexOf(DamageTaken.PlayerDamaged)].text = $"HP: {DamageTaken.CurrentHealth}";
    }

    void OnPlayerDie(OnPlayerDied deadPlayer)
    {
        GameOverHud.SetActive(true);
        GameOverText.text = $"Jogador {deadPlayer.DeadPlayer} morreu. O que desejam fazer agora?";
    }
}
