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

    [SerializeField] private Player _player;
    private Rigidbody _rb;
    #endregion

    #region Properties
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    #endregion

    #region Functions
    private void Awake()
    {
        //_player = GetComponent<Player>();
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rb.velocity = Vector3.zero;

        if (!_player.isEnabled)
            return;

        FaceRotation();

        Vector3 newPos = new Vector3(_moveVec.x, 0, _moveVec.y).normalized * _speed * Time.fixedDeltaTime;
        transform.position += newPos;

        if (Keyboard.current.eKey.IsPressed())
        {
            _player.openDoor();
        }
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (!_player.isEnabled)
                return;

            if (!gameObject.activeInHierarchy)
                return;

            _moveVec = context.ReadValue<Vector2>();
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            if (!_player.isEnabled)
                return;

            if (gameObject.activeInHierarchy)
            {
                if (context.performed)
                {
                    Debug.Log("Attacking!");
                    _player.playerAttackingScript.Attack();
                }
            }
        }
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if(context.performed && _player.isEnabled)
        {
            if (gameObject.activeInHierarchy)
            {
                if (context.performed)
                {
                    //Debug.Log("Pausing");
                    _player.PlayerUI.PauseGame();
                }
            }
        }
    }

    public void Potion(InputAction.CallbackContext context)
    {
        if (context.performed && _player.isEnabled)
        {
            if (gameObject.activeInHierarchy)
            {
                if (context.performed)
                {
                    _player.UsePotion();
                }
            }
        }
    }

    private void FaceRotation()
    {
        if (_moveVec.x > _axisDeadSpace && !(_moveVec.y > _axisDeadSpace || _moveVec.y < -_axisDeadSpace))
            transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
        else if (_moveVec.x < -_axisDeadSpace && !(_moveVec.y > _axisDeadSpace || _moveVec.y < -_axisDeadSpace))
            transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
        else if (_moveVec.x > _axisDeadSpace && _moveVec.y > _axisDeadSpace)
            transform.rotation = Quaternion.Euler(new Vector3(0, 45, 0));
        else if (_moveVec.x > _axisDeadSpace && _moveVec.y < -_axisDeadSpace)
            transform.rotation = Quaternion.Euler(new Vector3(0, 135, 0));
        else if (_moveVec.x < -_axisDeadSpace && _moveVec.y > _axisDeadSpace)
            transform.rotation = Quaternion.Euler(new Vector3(0, -45, 0));
        else if (_moveVec.x < -_axisDeadSpace && _moveVec.y < -_axisDeadSpace)
            transform.rotation = Quaternion.Euler(new Vector3(0, -135, 0));
        if (_moveVec.y > _axisDeadSpace && !(_moveVec.x > _axisDeadSpace || _moveVec.x < -_axisDeadSpace))
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        else if (_moveVec.y < -_axisDeadSpace && !(_moveVec.x > _axisDeadSpace || _moveVec.x < -_axisDeadSpace))
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }
    #endregion
}

