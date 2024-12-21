using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2d;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float _speed = 2500f;
    [SerializeField] private float _destroyDelay = 5f;
    [SerializeField] private int _damage = 25;

    public int Damage { get => _damage; }

    private void Update()
    {
        _rb2d.linearVelocity = direction * _speed * Time.deltaTime;
    }

    private void OnEnable()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        transform.position = PlayerController.Instance.ShootingPoint.position;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        StartCoroutine(DestroyObjectAfterDelay());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>()) { return; }
        Destroy(gameObject);
    }

    private void OnDisable()
    {

    }

    IEnumerator DestroyObjectAfterDelay()
    {
        yield return new WaitForSeconds(_destroyDelay);
        ObjectPoolingManager.Instance.GetBackProjectileToPool(gameObject);
    }

}
