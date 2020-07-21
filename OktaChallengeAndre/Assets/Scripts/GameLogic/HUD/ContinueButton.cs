using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public AimAndShoot[] PlayersAimAndShoot;

    public GameObject ChooseWeaponHUD;

    public void OnContinueButton(Text errorText)
    {
        bool canDisable = !PlayersAimAndShoot.Any(current => current.ChoosedBullet == null);

        if (canDisable == true)
        {
            ChooseWeaponHUD.SetActive(false);
        }
        else
        {
            errorText.text = "Um dos jogadores não escolheu uma arma.";
        }
    }
}
