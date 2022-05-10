using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewItemData", menuName = "ItemData", order = 52)]
public class ItemData : ScriptableObject
{
    #region
    [SerializeField] private ItemType _itemType;
    [SerializeField] private GameObject _itemModel;
    [SerializeField] private int _scoreReward;
    [SerializeField] private int _healAmount;
    #endregion

    #region Properties
    public ItemType ItemType { get { return _itemType; } }
    public GameObject ItemModel { get { return _itemModel; } }
    public int ScoreReward { get { return _scoreReward; } }
    public int HealAmount { get { return _healAmount; } }
    #endregion
}
