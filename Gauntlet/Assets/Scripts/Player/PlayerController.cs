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
    private void FixedUpdate()
    {
        FaceRotation();

        Vector3 newPos = new Vector3(_moveVec.x, 0, _moveVec.y).normalized * _speed * Time.fixedDeltaTime;
        transform.position += newPos;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveVec = context.ReadValue<Vector2>();
    }

    private void FaceRotation()
    {
        if (_moveVec.x > _axisDeadSpace && !(_moveVec.y > _axisDeadSpace || _moveVec.y < -_axisDeadSpace))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        }
        if (_moveVec.x < -_axisDeadSpace && !(_moveVec.y > _axisDeadSpace || _moveVec.y < -_axisDeadSpace))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        }

    }
    #endregion
}

