using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    public List<Text> PlayerLifeHUD;
    public List<PlayerCombatInfo> PlayerLife;
    //public List<PlayerCombatInfo> CombatInfos;

    void Start()
    {
        
    }

    void Update()
    {
        for(int i = 0; i < PlayerLife.Count - 1; i++)
        {
            PlayerLifeHUD[i].text = $"HP: {PlayerLife[i].PlayerHP}";
        }
    }
}
