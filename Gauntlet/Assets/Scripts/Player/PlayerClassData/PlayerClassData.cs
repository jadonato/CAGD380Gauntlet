using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewPlayerClassData", menuName = "PlayerClassData", order = 51)]
public class PlayerClassData : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _baseHealth;
    [SerializeField] private float _meleeDamage;
    [SerializeField] private float _magicDamage;
    [SerializeField] private float _armor;
    [SerializeField] private float _moveSpeed;

    public Sprite Sprite { get { return _sprite; } }
    public float BaseHealth { get { return _baseHealth; } }
    public float MeeleDamage { get { return _meleeDamage; } }
    public float MagicDamage { get { return _magicDamage; } }
    public float Armor { get { return _armor; } }
    public float MoveSpeed { get { return _moveSpeed; } }
}
