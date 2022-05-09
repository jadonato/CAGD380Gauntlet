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

    [Header("Player Stats")]
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _meeleDamage;
    [SerializeField] private float _magicDamage;
    [SerializeField] private float _armor;
    [SerializeField] private float _moveSpeed;

    public GameObject door;

    private PlayerController _controller;
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
    #endregion

    #region Functions
    private void Awake()
    {
        //Spawn Player at spawnpoint
        transform.position = GameObject.FindGameObjectWithTag("SpawnPoint").transform.position;
        _controller = GetComponent<PlayerController>();
        UpdateStatValues();
    }

    #region Public Functions
    public void Attack()
    {
        Debug.Log("Attack");
    }
    #endregion

    //Open Door function
    public void openDoor()
    {
        if (door != null)
        {
            print("There is a door");
            if (Keyboard.current.eKey.IsPressed())
            {
                print("E key has been pressed");
                Destroy(door);
                door = null;
            }
        }
    }

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
    #endregion
}
