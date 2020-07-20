using GameEventBus.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HudManager : MonoBehaviour
{
    public List<Text> PlayerLifeHUD;
    public List<Player> Players;

    [Inject]
    public IEventBus eventBus;

    void OnEnable()
    {
        eventBus.Subscribe<OnDamageTaken>(OnDamageTaken);
    }

    private void OnDestroy()
    {
        eventBus.Unsubscribe<OnDamageTaken>(OnDamageTaken);
    }
    void Start()
    {
        
    }

    void Update()
    {
        //for (int i = 0; i < PlayerLifeHUD.Count - 1; i++)
        //{
        //    PlayerLifeHUD[i].text = $"HP: {Players[i].GetComponent<StatsManager>().CurrentHealth}";
        //}
    }

    void OnDamageTaken(OnDamageTaken DamageTaken)
    {
        PlayerLifeHUD[Players.IndexOf(DamageTaken.PlayerDamaged)].text = $"HP: {DamageTaken.CurrentHealth}";
    }
}
