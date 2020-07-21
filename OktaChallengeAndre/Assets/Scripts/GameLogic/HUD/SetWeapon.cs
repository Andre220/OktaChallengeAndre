using UnityEngine;

public class SetWeapon : MonoBehaviour
{
    public AimAndShoot PlayerAimAndShoot;
    public void SetPlayerBullet(BulletStats bulletStats)
    {
        PlayerAimAndShoot.ChoosedBullet = bulletStats;
    }
}
