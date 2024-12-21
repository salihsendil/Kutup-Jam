using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }

    [Header("References")]
    private PlayerInput _playerInput;

    [Header("Movement Variables")]
    private Vector2 _movementInput;

    [Header("Fire Variables")]
    private bool _isShooting;

    [Header("Getters - Setters")]
    public Vector2 MovementInput { get => _movementInput; }
    public bool IsShooting { get => _isShooting; }

    private void Awake()
    {
        #region SingletonPattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        #endregion

        _playerInput = new PlayerInput();

    }


    void Start()
    {
        _playerInput.Movement.Move.started += Move;
        _playerInput.Movement.Move.performed += Move;
        _playerInput.Movement.Move.canceled += Move;

        _playerInput.Mouse.Fire.started += Fire;
        _playerInput.Mouse.Fire.performed += Fire;
        _playerInput.Mouse.Fire.canceled += Fire;

    }

    private void Fire(InputAction.CallbackContext callback)
    {
        _isShooting = callback.ReadValueAsButton();
    }

    void Move(InputAction.CallbackContext callback)
    {
        _movementInput = callback.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Movement.Move.started -= Move;
        _playerInput.Movement.Move.performed -= Move;
        _playerInput.Movement.Move.canceled -= Move;
        
        _playerInput.Mouse.Fire.started -= Fire;
        _playerInput.Mouse.Fire.performed -= Fire;
        _playerInput.Mouse.Fire.canceled -= Fire;
    }

}
