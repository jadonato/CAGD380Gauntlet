using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    #region Variables
    [Header("Player Info")]
    [SerializeField] private PlayerClassData _class;
    [SerializeField] private int _score;

    [Header("Player Stats")]
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _meeleDamage;
    [SerializeField] private float _magicDamage;
    [SerializeField] private float _armor;
    [SerializeField] private float _moveSpeed;

    [Header("Player Attacking Stats")]
    [SerializeField] private float _attackCooldownSeconds;

    private PlayerController _controller;
    [SerializeField] private PlayerAttacking _playerAttacking;
    #endregion

    #region Properties
    public PlayerClassData Class { get { return _class; } }
    public int Score 
    {
        get { return _score; }
        set
        {
            _score = value;
        }
    }
    public PlayerAttacking playerAttackingScript { get { return _playerAttacking; } }
    #endregion

    #region Functions
    private void Awake()
    {
        _controller = GetComponent<PlayerController>();
        //_playerAttacking = gameObject.GetComponent<PlayerAttacking>();

        UpdateStatValues();
        SetAttackingInfo();
        SetAttackingInfo();
    }

    #region Public Functions
    public void Attack()
    {
        Debug.Log("Attack");
    }
    #endregion

    private void UpdateStatValues()
    {
        //Sets private variables to correct values
        _maxHealth = _class.BaseHealth;
        _meeleDamage = _class.MeeleDamage;
        _magicDamage = _class.MagicDamage;
        _armor = _class.Armor;
        _moveSpeed = _class.MoveSpeed;

        //Updates other scripts that need this info
        _controller.Speed = _moveSpeed;
    }

    private void SetAttackingInfo()
    {
        _playerAttacking.AttackCooldown = _attackCooldownSeconds;
        _playerAttacking.SetDamages(_meeleDamage, _magicDamage);
    }    
    #endregion
}
