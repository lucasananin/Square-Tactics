using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] Animator _animator = null;
    [SerializeField] Unit _unit = null;
    [SerializeField] BulletProjectile _bulletProjectilePrefab = null;
    [SerializeField] GameObject _rifleGameObject = null;
    [SerializeField] GameObject _swordGameObject = null;
    [SerializeField] Transform _shootPoint = null;
    [SerializeField, ReadOnly] ShootAction _shootAction = null;
    [SerializeField, ReadOnly] SwordAction _swordAction = null;

    private int _shootTriggerHash = Animator.StringToHash("Shoot");
    private int _swordSlashTriggerHash = Animator.StringToHash("SwordSlash");
    private int _horizontalVelocityHash = Animator.StringToHash("HorizontalVelocity");
    private int _verticalVelocityHash = Animator.StringToHash("VerticalVelocity");
    private int _isChangingFloorsHash = Animator.StringToHash("IsChangingFloors");

    private void OnEnable()
    {
        if (TryGetComponent(out ShootAction _shootAction))
        {
            this._shootAction = _shootAction;
            this._shootAction.onShoot += TriggerShoot;
        }

        if (TryGetComponent(out SwordAction _swordAction))
        {
            this._swordAction = _swordAction;
            this._swordAction.onSwordActionStarted += _swordAction_onSwordActionStarted;
            this._swordAction.onSwordActionCompleted += _swordAction_onSwordActionCompleted;
        }
    }

    private void OnDisable()
    {
        if (_shootAction != null)
        {
            _shootAction.onShoot -= TriggerShoot;
        }

        if (_swordAction != null)
        {
            _swordAction.onSwordActionStarted -= _swordAction_onSwordActionStarted;
            _swordAction.onSwordActionCompleted -= _swordAction_onSwordActionCompleted;
        }
    }

    private void Start()
    {
        EquipRifle();
    }

    private void LateUpdate()
    {
        MoveAction _moveAction = _unit.GetAction<MoveAction>();
        _animator.SetBool(_isChangingFloorsHash, _moveAction.IsChangingFloors());
        _animator.SetFloat(_horizontalVelocityHash, _moveAction.GetVelocity().x);
        _animator.SetFloat(_verticalVelocityHash, _moveAction.GetVelocity().y);
    }

    [Button]
    private void TriggerShoot(object _sender, ShootAction.OnShootEventArgs _e)
    {
        _animator.SetTrigger(_shootTriggerHash);

        var _bullet = Instantiate(_bulletProjectilePrefab, _shootPoint.position, Quaternion.identity);
        _bullet.Setup(_e);

        //var _targetPosition = _e.targetUnit.transform.position;
        //_targetPosition.y = _shootPoint.position.y;

        //_bullet.SetDamage(_e.damage);
        //_bullet.SetTargetPosition(_targetPosition);
    }

    private void _swordAction_onSwordActionStarted(object sender, System.EventArgs e)
    {
        EquipSword();
        _animator.SetTrigger(_swordSlashTriggerHash);
    }

    private void _swordAction_onSwordActionCompleted(object sender, System.EventArgs e)
    {
        EquipRifle();
    }

    private void EquipSword()
    {
        _swordGameObject.SetActive(true);
        _rifleGameObject.SetActive(false);
    }

    private void EquipRifle()
    {
        _rifleGameObject.SetActive(true);
        _swordGameObject.SetActive(false);
    }
}
