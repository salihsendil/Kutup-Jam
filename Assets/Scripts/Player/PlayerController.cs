using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IShootable
{
    public static PlayerController Instance { get; private set; }
    public Action updatedHealth;

    [Header("References")]
    [SerializeField] private Transform _mainCamTransform;
    [SerializeField] private Transform _playerLookTo;

    [Header("Movement Variables")]
    [SerializeField] private Vector3 _movementVector;
    [SerializeField] private float _speed = 3f;

    [Header("Rotation Variables")]
    [SerializeField] private float _rotationOffset;

    [Header("Shooting Variables")]
    [SerializeField] private bool _canShoot = true;
    [SerializeField] private float _shootDelay = 0.5f;
    [SerializeField] private Transform _shootingPoint;

    [Header("Health Variables")]
    [SerializeField] private int health = 100;
    public HealthSystem healthSystem;


    public Transform ShootingPoint { get => _shootingPoint; }

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
        healthSystem = new HealthSystem(health);

    }


    void Start()
    {
        _mainCamTransform = Camera.main.transform;
    }
  

    void Update()
    {
        HandleMovement();
        HandleRotation();
        if (PlayerInputManager.Instance.IsShooting)
        {
            Shoot();
        }

    }

    private void HandleMovement()
    {
        //rotation relative movement
        //_movementVector = PlayerInputManager.Instance.MovementInput;
        //_movementVector = _movementVector.x * -_playerLookTo.transform.up + _movementVector.y * _playerLookTo.transform.right;
        //_movementVector.z = 0f;
        //transform.position += _movementVector * _speed * Time.deltaTime;
        //Debug.Log(_playerLookTo.transform.up.y);

        //universal movement
        _mainCamTransform = Camera.main.transform;
        _movementVector = PlayerInputManager.Instance.MovementInput;
        _movementVector.x = _movementVector.x * _mainCamTransform.right.x;
        _movementVector.y = _movementVector.y * _mainCamTransform.up.y;
        _movementVector.z = 0f;
        transform.position += _movementVector * _speed * Time.deltaTime;
        PlayerAnimController.Instance.IsWalking = _movementVector != Vector3.zero ? true : false;
    }

    private void HandleRotation()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.DrawLine(transform.position, mousePosition, Color.red, 1f);
        transform.rotation = Quaternion.Euler(0, 0, angle + _rotationOffset);
    }

    public void Shoot()
    {
        if (_canShoot)
        {
            StartCoroutine(FireDelay());
        }
    }

    private IEnumerator FireDelay()
    {
        ObjectPoolingManager.Instance.GetOutProjectileFromPool();
        _canShoot = false;
        PlayerAnimController.Instance.AnimatorBody.SetTrigger("isShooting");
        yield return new WaitForSeconds(_shootDelay);
        _canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HealPowerUp>())
        {
            collision.gameObject.GetComponent<HealPowerUp>().Collect();
            updatedHealth?.Invoke();
        }
    }

}
