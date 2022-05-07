using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    #region Variables
    [Header("Player Reference")]
    [SerializeField] private Player _player;

    [Header("State Flags")]
    [SerializeField] private bool _canAttack = true;
    [SerializeField] private bool _damageableInMeleeRange = false;

    [Header("Other References")]
    [SerializeField] private AttackHurtbox _meleeHurtbox;
    [SerializeField] private GameObject _magicProjectilePrefab;
    [SerializeField] private Transform _firingPoint;

    //Stats
    private float _meleeDamage;
    private float _magicDamage;
    private float _attackCooldown;

    //References
    //private MeshRenderer _mRenderer;

    //Other
    private IEnumerator _attackCoroutine;
    #endregion

    #region Properties
    //public float AttackDamage { get { return _attackDamage; } set { _attackDamage = value; } }
    public float AttackCooldown { get { return _attackCooldown; } set { _attackCooldown = value; } }
    #endregion

    #region Functions
    private void Awake()
    {
        //_mRenderer = GetComponent<MeshRenderer>();
        _canAttack = true;
    }

    public void Attack()
    {       
        if(_canAttack)
        {           
            _attackCoroutine = AttackCycle();
            StartCoroutine(_attackCoroutine);
        }
    }

    private IEnumerator AttackCycle()
    {
        float cooldown = _attackCooldown;
        float meleeActiveTime = 0.1f;
        //Set bool and check if melee or ranged
        _canAttack = false;
        if (_damageableInMeleeRange) //Attack will be melee
        {
            //Enable hurtbox
            _meleeHurtbox.SetActiveState(true);

            //Wait for some time (1 frame?)
            //yield return new WaitForSeconds(0.5f);
            yield return new WaitForSeconds(meleeActiveTime);
            cooldown -= meleeActiveTime;

            //Disable hurtbox
            _meleeHurtbox.SetActiveState(false);
        }
        else //Attack will be ranged
        {
            //TOTO: Add ranged attack stuff here
            MagicAttack();
        }

        //Wait for cooldown and reset bool
        yield return new WaitForSeconds(cooldown);
        _canAttack = true;
    }

    private void MagicAttack()
    {
        PlayerMagicProjectile attack = Instantiate(_magicProjectilePrefab, _firingPoint.position, _firingPoint.rotation).GetComponent<PlayerMagicProjectile>();
        attack.Damage = _magicDamage;
    }

    public void SetDamages(float meleeDamage, float magicDamage)
    {
        _meleeDamage = meleeDamage;
        _magicDamage = magicDamage;

        _meleeHurtbox.damage = _meleeDamage;
    }

    private void OnTriggerStay(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if(damageable != null)
        {
            _damageableInMeleeRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            _damageableInMeleeRange = false;
        }
    }
    #endregion
}
