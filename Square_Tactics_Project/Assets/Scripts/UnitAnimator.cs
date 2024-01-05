using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Audio;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] Animator _animator = null;
    [SerializeField] Unit _unit = null;
    [SerializeField] BulletProjectile _bulletProjectilePrefab = null;
    [SerializeField] ExplosiveArrowProjectile _explosiveArrowActionProjectile = null;
    [SerializeField] GameObject _rifleGameObject = null;
    [SerializeField] GameObject _swordGameObject = null;
    [SerializeField] Transform _shootPoint = null;
    [SerializeField, ReadOnly] ShootAction _shootAction = null;
    [SerializeField, ReadOnly] SwordAction _swordAction = null;

    [Title("// Values")]
    [SerializeField] float _timeToShoot = 1f;
    [SerializeField] float _timeToShootExplosiveArrow = 1f;

    [Title("// Audio")]
    [SerializeField] AudioDataSO _pullAudio = null;
    [SerializeField] AudioDataSO _shootAudio = null;

    private int _horizontalVelocityHash = Animator.StringToHash("HorizontalVelocity");
    private int _verticalVelocityHash = Animator.StringToHash("VerticalVelocity");
    private int _isChangingFloorsHash = Animator.StringToHash("IsChangingFloors");
    private int _swordSlashTriggerHash = Animator.StringToHash("SwordSlash");
    private int _shootTriggerHash = Animator.StringToHash("Shoot");
    private int _TakeDamageTriggerHash = Animator.StringToHash("TakeDamage");
    private int _dieTriggerHash = Animator.StringToHash("Die");
    private int _specialAttackTriggerHash = Animator.StringToHash("SpecialAttack");
    private int _buffAbilityTriggerHash = Animator.StringToHash("BuffAbility");

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

        HealthSystem _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.onTakeDamage += _healthSystem_onTakeDamage;
        _healthSystem.onDead += _healthSystem_onDead;
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

        HealthSystem _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.onTakeDamage -= _healthSystem_onTakeDamage;
        _healthSystem.onDead -= _healthSystem_onDead;
    }

    //private void Start()
    //{
    //    EquipRifle();
    //}

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
        StartCoroutine(TriggerShoot_routine(_e));

        //var _targetPosition = _e.targetUnit.transform.position;
        //_targetPosition.y = _shootPoint.position.y;

        //_bullet.SetDamage(_e.damage);
        //_bullet.SetTargetPosition(_targetPosition);
    }

    private IEnumerator TriggerShoot_routine(ShootAction.OnShootEventArgs _e)
    {
        _pullAudio?.PlayAsSfx();

        yield return new WaitForSeconds(_timeToShoot);

        var _bullet = Instantiate(_bulletProjectilePrefab, _shootPoint.position, Quaternion.identity);
        _bullet.Setup(_e);
        _shootAudio?.PlayAsSfx();
    }

    private void _swordAction_onSwordActionStarted(object sender, System.EventArgs e)
    {
        //EquipSword();
        _animator.SetTrigger(_swordSlashTriggerHash);
    }

    private void _swordAction_onSwordActionCompleted(object sender, System.EventArgs e)
    {
        //EquipRifle();
    }

    //private void EquipSword()
    //{
    //    _swordGameObject.SetActive(true);
    //    _rifleGameObject.SetActive(false);
    //}

    //private void EquipRifle()
    //{
    //    _rifleGameObject.SetActive(true);
    //    _swordGameObject.SetActive(false);
    //}

    private void _healthSystem_onTakeDamage()
    {
        _animator.SetTrigger(_TakeDamageTriggerHash);
    }

    private void _healthSystem_onDead()
    {
        //_animator.applyRootMotion = true;
        _animator.SetTrigger(_dieTriggerHash);
    }

    public void TriggerSpecialAttack()
    {
        _animator.SetTrigger(_specialAttackTriggerHash);
    }

    public void TriggerBuffAbility()
    {
        _animator.SetTrigger(_buffAbilityTriggerHash);
    }

    public void TriggerExplosiveArrow(ExplosiveArrowAction _actionValue)
    {
        StartCoroutine(TriggerExplosiveArrow_routine(_actionValue));
    }

    private IEnumerator TriggerExplosiveArrow_routine(ExplosiveArrowAction _actionValue)
    {
        TriggerSpecialAttack();

        yield return new WaitForSeconds(_timeToShootExplosiveArrow);

        var _explosiveArrow = Instantiate(_explosiveArrowActionProjectile, _shootPoint.position, Quaternion.identity);
        _explosiveArrow.Setup(_actionValue);
    }
}
