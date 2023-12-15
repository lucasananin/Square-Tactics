using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WarriorAnimatorEvents : MonoBehaviour
{
    [SerializeField] UnityEvent _onHit = new UnityEvent();
    [SerializeField] UnityEvent _onFootR = new UnityEvent();
    [SerializeField] UnityEvent _onFootL = new UnityEvent();
    [SerializeField] UnityEvent _onLand = new UnityEvent();
    [SerializeField] UnityEvent _onShoot = new UnityEvent();
    [SerializeField] UnityEvent _onWeaponSwitch = new UnityEvent();

    public void Hit()
    {
        _onHit.Invoke();
    }

    public void FootR()
    {
        _onFootR.Invoke();
    }

    public void FootL()
    {
        _onFootL.Invoke();
    }

    public void Land()
    {
        _onLand.Invoke();
    }

    public void Shoot()
    {
        _onShoot.Invoke();
    }

    /// <summary>
    /// Checked when switching weapons to know when to turn on/off weapon models.
    /// </summary>
    public void WeaponSwitch()
    {
        _onWeaponSwitch.Invoke();
    }
}
