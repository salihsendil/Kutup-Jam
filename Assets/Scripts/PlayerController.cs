using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _mainCamTransform;
    [SerializeField] private Transform _playerLookTo;

    [Header("Movement Variables")]
    [SerializeField] private Vector3 _movementVector;
    [SerializeField] private float _speed = 6f;

    [Header("Rotation Variables")]
    [SerializeField] private float _rotationOffset = -90f;



    void Start()
    {
        _mainCamTransform = Camera.main.transform;
    }


    void Update()
    {
        HandleMovement();
        HandleRotation();

    }

    private void HandleMovement()
    {
        //rotation relative movement
        _movementVector = PlayerInputManager.Instance.MovementInput;
        _movementVector = _movementVector.y * _playerLookTo.transform.up + _movementVector.x * _playerLookTo.transform.right;
        _movementVector.z = 0f;
        transform.position += _movementVector * _speed * Time.deltaTime;
        Debug.Log(_playerLookTo.transform.up.y);

        //universal movement
        //_mainCamTransform = Camera.main.transform;
        //_movementVector = PlayerInputManager.Instance.MovementInput;
        //_movementVector.x = _movementVector.x * _mainCamTransform.right.x;
        //_movementVector.y = _movementVector.y * _mainCamTransform.up.y;
        //_movementVector.z = 0f;
        //transform.position += _movementVector * _speed * Time.deltaTime;
    }

    private void HandleRotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;

        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.DrawLine(transform.position, mousePosition, Color.red, 1f);
        transform.rotation = Quaternion.Euler(0, 0, angle + _rotationOffset);
    }
}
