using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdollSpawner : MonoBehaviour
{
    [SerializeField] UnitRagdoll _ragdollPrefab = null;
    [SerializeField] Transform _rootBone = null;
    [SerializeField] HealthSystem _healthSystem = null;

    //private void OnEnable()
    //{
    //    _healthSystem.onDead += Spawn;
    //}

    //private void OnDisable()
    //{
    //    _healthSystem.onDead -= Spawn;
    //}

    private void Spawn()
    {
        var _ragdoll = Instantiate(_ragdollPrefab, transform.position, transform.rotation);
        _ragdoll.SetOriginalRootBone(_rootBone);
    }
}
