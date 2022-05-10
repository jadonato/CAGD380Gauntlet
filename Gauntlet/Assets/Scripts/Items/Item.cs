using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    #region Variables
    [SerializeField] private ItemData _itemData;

    private GameObject _model;
    #endregion

    #region Properties
    public ItemData ItemData { get { return _itemData; } }
    #endregion

    private void OnEnable()
    {
        InitializeItem();
    }

    private void InitializeItem()
    {
        if(_itemData.ItemModel != null)
        {
            _model = Instantiate(_itemData.ItemModel);
            _model.transform.position = transform.position;
            _model.transform.parent = transform;
        }
    }
}
