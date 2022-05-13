using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewPlayerClassData", menuName = "PlayerClassData", order = 51)]
public class PlayerClassData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Color _classColor;
    [SerializeField] private int _baseHealth;
    [SerializeField] private int _meleeDamage;
    [SerializeField] private int _magicDamage;
    [SerializeField] private int _magicProjectileSpeed;
    [SerializeField] private int _armor;
    [SerializeField] private int _moveSpeed;
    [SerializeField] private int _potionDamage;

    public string ClassName { get { return _name; } }
    public Sprite Sprite { get { return _sprite; } }
    public Color ClassColor { get { return _classColor; } }
    public int BaseHealth { get { return _baseHealth; } }
    public int MeeleDamage { get { return _meleeDamage; } }
    public int MagicDamage { get { return _magicDamage; } }
    public int MagicProjectileSpeed { get { return _magicProjectileSpeed; } }
    public int Armor { get { return _armor; } }
    public int MoveSpeed { get { return _moveSpeed; } }
    public int PotionDamage { get { return _potionDamage; } }
}
