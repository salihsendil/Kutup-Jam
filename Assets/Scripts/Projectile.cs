using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Vector3 _spawnPointOffset = new Vector3(0f, 0.4f, 0f);
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Rigidbody2D _rb;

    private void OnEnable()
    {
        //its gonna fix
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        transform.position = PlayerController.Instance.transform.position + _spawnPointOffset;

        //transform.Translate(mousePosition - transform.position);
        //Vector3.MoveTowards(transform.position, mousePosition, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnDisable()
    {

    }

}
