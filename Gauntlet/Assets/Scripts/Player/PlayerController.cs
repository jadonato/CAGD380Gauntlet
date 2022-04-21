using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private float _speed;
    [SerializeField] [Range(0f, 0.5f)] private float _axisDeadSpace = 0.3f;

    [SerializeField] private Vector2 _moveVec = Vector2.zero;
    #endregion

    #region Properties
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    #endregion

    #region Functions
    private void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveVec = context.ReadValue<Vector2>();
    }
    #endregion
}

