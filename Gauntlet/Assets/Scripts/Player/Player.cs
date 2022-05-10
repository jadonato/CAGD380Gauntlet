using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    #region Variables
    [Header("Player Info")]
    [SerializeField] private PlayerClassData _class;
    [SerializeField] private int _score;
    public bool isEnabled = true;

    [Header("Player Stats")]
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _meleeDamage;
    [SerializeField] private int _magicDamage;
    [SerializeField] private int _magicProjectileSpeed;
    [SerializeField] private int _armor;
    [SerializeField] private int _moveSpeed;
    [SerializeField] private int _potionDamage;

    [Header("Player Attacking Stats")]
    [SerializeField] private float _attackCooldownSeconds;

    [Header("Items")]
    [SerializeField] private int _bluePotions;
    [SerializeField] private int _orangePotions;
    [SerializeField] private int _keys;

    [Header("Stat Upgrade Stuff")]
    [SerializeField] private int _armorIncrease = 5;
    [SerializeField] private int _potionDamageIncrease = 2;
    [SerializeField] private int _magicDamageIncrease = 2;
    [SerializeField] private int _magicSpeedIncrease = 5;
    [SerializeField] private int _meleeDamageIncrease = 2;
    [SerializeField] private int _moveSpeedIncrease = 3;
    [SerializeField] private bool _hasArmorPot;
    [SerializeField] private bool _hasPotionPot;
    [SerializeField] private bool _hasMagicDamagePot;
    [SerializeField] private bool _hasMagicSpeedPot;
    [SerializeField] private bool _hasMeleeDamagePot;
    [SerializeField] private bool _hasMoveSpeedPot;

    private PlayerController _controller;
    [SerializeField] private PlayerAttacking _playerAttacking;
    private Vector3 _spawnpoint;

    [Header("Object References")]
    public GameObject door;
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
    //public float MagicProjectileSpeed { get { return _magicProjectileSpeed; } }
    #endregion

    #region Functions
    private void Awake()
    {
        //Check for spawnpoint and move player to spawn
        if(GameObject.FindGameObjectWithTag("SpawnPoint") != null)
        {
            _spawnpoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
            transform.position = _spawnpoint;
        }

        _controller = GetComponent<PlayerController>();
        //_playerAttacking = gameObject.GetComponent<PlayerAttacking>();

        SetStatValues();
        SetAttackingInfo();
    }

    #region Public Functions
    //Open Door function
    public void openDoor()
    {
        if (door != null)
        {
            door.GetComponent<Door>().openDoor();
            door = null;
            
        }
    }

    public void takeDamage(float damage)
    {
        _health -= (int)damage;
        if(_health <= 0)
        {
            //Destroy(gameObject);
            DisablePlayer();
        }
    }

    public void Heal(int heal)
    {
        _health += heal;
        if (_health >= _maxHealth)
            _health = _maxHealth;
    }

    public void DisablePlayer()
    {
        isEnabled = false;
    }
    #endregion

    private void SetStatValues()
    {
        //Sets private variables to correct values
        _maxHealth = _class.BaseHealth;
        _meleeDamage = _class.MeeleDamage;
        _magicDamage = _class.MagicDamage;
        _magicProjectileSpeed = _class.MagicProjectileSpeed;
        _armor = _class.Armor;
        _moveSpeed = _class.MoveSpeed;
        _potionDamage = _class.PotionDamage;

        //Updates other scripts that need this info
        _controller.Speed = _moveSpeed;
    }

    private void UpdateStatValues()
    {
        _controller.Speed = _moveSpeed;
    }

    private void SetAttackingInfo()
    {
        _playerAttacking.AttackCooldown = _attackCooldownSeconds;
        _playerAttacking.SetDamages(_meleeDamage, _magicDamage, _magicProjectileSpeed);
    }


    private void PickupItem(Item item)
    {
        switch (item.ItemData.ItemType)
        {
            case ItemType.BluePotion:
                _bluePotions++;
                break;
            case ItemType.OrangePotion:
                _orangePotions++;
                break;
            case ItemType.Key:
                _keys++;
                break;
            case ItemType.ArmorPotion:
                _armor += _armorIncrease;
                break;
            case ItemType.PotionPowerPotion:
                if(!_hasArmorPot)
                {
                    _potionDamage += _potionDamageIncrease;
                    _hasArmorPot = true;
                }
                else
                    _bluePotions++;
                break;
            case ItemType.MagicDamagePotion:
                if (!_hasMagicDamagePot)
                {
                    _magicDamage += _magicDamageIncrease;
                    _hasMagicDamagePot = true;
                }
                else
                    _bluePotions++;
                break;
            case ItemType.MagicSpeedPotion:
                if (!_hasMagicSpeedPot)
                {
                    _magicProjectileSpeed += _magicSpeedIncrease;
                    _hasMagicSpeedPot = true;
                }
                else
                    _bluePotions++;
                break;
            case ItemType.MeleeDamagePotion:
                if (!_hasMeleeDamagePot)
                {
                    _meleeDamage += _meleeDamageIncrease;
                    _hasMeleeDamagePot = true;
                }
                else
                    _bluePotions++;
                break;
            case ItemType.MoveSpeedPotion:
                if (!_hasMoveSpeedPot)
                {
                    _moveSpeed += _moveSpeedIncrease;
                    _hasMoveSpeedPot = true;
                }
                else
                    _bluePotions++;
                break;
            case ItemType.Treasure:
                _score += item.ItemData.ScoreReward;
                break;
            case ItemType.BagOfJewels:
                _score += item.ItemData.ScoreReward;
                break;
            case ItemType.FoodSmall:
                Heal(item.ItemData.HealAmount);
                break;
            case ItemType.FoodMedium:
                Heal(item.ItemData.HealAmount);
                break;
            case ItemType.FoodLarge:
                Heal(item.ItemData.HealAmount);
                break;
            default:
                break;
        }

        UpdateStatValues();
        Destroy(item.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Item>())
        {
            Debug.Log("Collided with item!");
            PickupItem(other.GetComponent<Item>());
        }
    }
    #endregion
}
