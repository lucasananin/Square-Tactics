using Cinemachine;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : Singleton<ScreenShaker>
{
    [SerializeField] CinemachineImpulseSource _cinemachineImpulseSource = null;

    private void OnEnable()
    {
        //ShootAction.onAnyShoot += ShootAction_onAnyShoot;
        BulletProjectile.onAnyBulletHit += BulletProjectile_onAnyBulletHit;
        GrenadeProjectile.onAnyGrenadeExploded += GrenadeProjectile_onAnyGrenadeExploded;
        SwordAction.onAnySwordHit += SwordAction_onAnySwordHit;
        SpinAttackAction.onAnyHit += SwordAction_onAnySwordHit;
    }

    private void OnDisable()
    {
        //ShootAction.onAnyShoot -= ShootAction_onAnyShoot;
        BulletProjectile.onAnyBulletHit -= BulletProjectile_onAnyBulletHit;
        GrenadeProjectile.onAnyGrenadeExploded -= GrenadeProjectile_onAnyGrenadeExploded;
        SwordAction.onAnySwordHit -= SwordAction_onAnySwordHit;
        SpinAttackAction.onAnyHit -= SwordAction_onAnySwordHit;
    }

    private void ShootAction_onAnyShoot(object sender, ShootAction.OnShootEventArgs e)
    {
        Shake();
    }

    private void BulletProjectile_onAnyBulletHit()
    {
        Shake();
    }

    private void GrenadeProjectile_onAnyGrenadeExploded()
    {
        Shake(5f);
    }

    private void SwordAction_onAnySwordHit(object sender, System.EventArgs e)
    {
        Shake(2f);
    }

    [Button]
    public void Shake(float _force = 1f)
    {
        _cinemachineImpulseSource.GenerateImpulse(_force);
    }
}
