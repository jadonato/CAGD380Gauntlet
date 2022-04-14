using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerClassData", menuName = "PlayerClassData", order = 51)]
public class PlayerClassData : ScriptableObject
{
    [SerializeField] private float _baseHealth;
    [SerializeField] private float _meleeDamage;
    [SerializeField] private float _magicDamage;
    [SerializeField] private float _defense;
    [SerializeField] private float _moveSpeed;

    public float BaseHealth { get { return _baseHealth; } }
    public float MeeleDamage { get { return _meleeDamage; } }
    public float MagicDamage { get { return _magicDamage; } }
    public float Defense { get { return _defense; } }
    public float MoveSpeed { get { return _moveSpeed; } }
}
