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
    [SerializeField] private int _lives;
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
    //[SerializeField] private int _orangePotions;
    [SerializeField] private int _keys;
    [SerializeField] private int _itemsAmount;
    [SerializeField] private int _maxItems;

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
    [SerializeField] private PlayerUI _playerUI;
    [SerializeField] private PlayerAttacking _playerAttacking;
    private Vector3 _spawnpoint;
    private Material _mainMat;

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
    public PlayerUI PlayerUI { get { return _playerUI; } }
    public int Health { get { return _health; } }
    public int Lives { get { return _lives; } }
    public int PotionCount { get { return _bluePotions; } }
    public int KeysCount { get { return _keys; } }
    //public float MagicProjectileSpeed { get { return _magicProjectileSpeed; } }
    #endregion

    #region Functions
    private void Awake()
    {
        //Check for spawnpoint and move player to spawn
        goToSpawnPoint();

        _controller = GetComponent<PlayerController>();
        //_playerUI = GetComponent<PlayerUI>();
        //_playerAttacking = gameObject.GetComponent<PlayerAttacking>();

        //SetStatValues();
        //SetAttackingInfo();
        _mainMat = GetComponent<MeshRenderer>().material;       
    }

    private void Start()
    {
        _playerUI.OpenClassSelectMenu();
        StartCoroutine(HealthDecayCycle());
    }

    #region Public Functions
    //Open Door function
    public void openDoor()
    {
        if (door != null)
        {
            if(door.GetComponent<Door>().isLocked && _keys > 0)
            {
                door.GetComponent<Door>().openDoor();
                door = null;
                _keys--;
                return;
            }
            door.GetComponent<Door>().openDoor();
            door = null;           
        }
    }

    public void takeDamage(float damage)
    {
        int finalDamage = (int)damage - _armor;
        if (finalDamage <= 0)
            finalDamage = 1;

        _health -= finalDamage;
        if(_health <= 0)
        {
            //Destroy(gameObject);
            _lives--;
            if(_lives <= 0)
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

    public void SetClass(PlayerClassData input)
    {
        _class = input;

        GameManager.Instance.MainUI.CreatePlayerScorecard(this);
        SetStatValues();
        SetAttackingInfo();
    }

    public void UsePotion()
    {
        if(_bluePotions > 0)
        {
            _bluePotions--;
            foreach(CoreEnemy enemy in FindObjectsOfType<CoreEnemy>())
            {
                enemy.TakeDamage(_potionDamage);
            }
            foreach(Generator generator in FindObjectsOfType<Generator>())
            {
                generator.TakeDamage(_potionDamage);
            }
            foreach(Death death in FindObjectsOfType<Death>())
            {
                death.DeathDie(this);
            }
        }
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
        _mainMat.color = _class.ClassColor;

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

    #region ItemPickupFunction
    private void PickupItem(Item item)
    {
        switch (item.ItemData.ItemType)
        {
            case ItemType.BluePotion:
                if (_itemsAmount < _maxItems)
                {
                    _bluePotions++;
                    _itemsAmount++;
                }
                else
                    return;
                break;
            case ItemType.OrangePotion:
                if (_itemsAmount < _maxItems)
                {
                    _bluePotions++;
                    _itemsAmount++;
                }
                else
                    return;
                break;
            case ItemType.Key:
                if (_itemsAmount < _maxItems)
                {
                    _keys++;
                    _itemsAmount++;
                }
                else
                    return;
                break;
            case ItemType.ArmorPotion:
                if (!_hasArmorPot)
                {
                    _armor += _armorIncrease;
                    _hasArmorPot = true;
                }
                else
                {
                    if (_itemsAmount < _maxItems)
                    {
                        _bluePotions++;
                        _itemsAmount++;
                    }
                    else
                        return;
                }
                break;
            case ItemType.PotionPowerPotion:
                if (!_hasPotionPot)
                {
                    _potionDamage += _potionDamageIncrease;
                    _hasPotionPot = true;
                }
                else
                {
                    if (_itemsAmount < _maxItems)
                    {
                        _bluePotions++;
                        _itemsAmount++;
                    }
                    else
                        return;
                }
                break;
            case ItemType.MagicDamagePotion:
                if (!_hasMagicDamagePot)
                {
                    _magicDamage += _magicDamageIncrease;
                    _hasMagicDamagePot = true;
                }
                else
                {
                    if (_itemsAmount < _maxItems)
                    {
                        _bluePotions++;
                        _itemsAmount++;
                    }
                    else
                        return;
                }
                break;
            case ItemType.MagicSpeedPotion:
                if (!_hasMagicSpeedPot)
                {
                    _magicProjectileSpeed += _magicSpeedIncrease;
                    _hasMagicSpeedPot = true;
                }
                else
                {
                    if (_itemsAmount < _maxItems)
                    {
                        _bluePotions++;
                        _itemsAmount++;
                    }
                    else
                        return;
                }
                break;
            case ItemType.MeleeDamagePotion:
                if (!_hasMeleeDamagePot)
                {
                    _meleeDamage += _meleeDamageIncrease;
                    _hasMeleeDamagePot = true;
                }
                else
                {
                    if (_itemsAmount < _maxItems)
                    {
                        _bluePotions++;
                        _itemsAmount++;
                    }
                    else
                        return;
                }
                break;
            case ItemType.MoveSpeedPotion:
                if (!_hasMoveSpeedPot)
                {
                    _moveSpeed += _moveSpeedIncrease;
                    _hasMoveSpeedPot = true;
                }
                else
                {
                    if (_itemsAmount < _maxItems)
                    {
                        _bluePotions++;
                        _itemsAmount++;
                    }
                    else
                        return;
                }
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
        SetAttackingInfo();
        Destroy(item.gameObject);
    }
    #endregion

    private IEnumerator HealthDecayCycle()
    {
        float timer = 0f;
        while(timer <= 1f)
        {
            if (!isEnabled)
            {
                yield return new WaitForEndOfFrame();
                StartCoroutine(HealthDecayCycle());
                yield break;
            }

            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }

        takeDamage(1f);
        StartCoroutine(HealthDecayCycle());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Item>())
        {
            Debug.Log("Collided with item!");
            PickupItem(other.GetComponent<Item>());
        }
    }

    public void goToSpawnPoint()
    {
        if (GameObject.FindGameObjectWithTag("SpawnPoint") != null)
        {
            _spawnpoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
            transform.position = _spawnpoint;
        }
    }
    #endregion
}
