using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IShootable
{
    private NavMeshAgent agent;
    public Transform player;
    [SerializeField] private int health = 100;
    private HealthSystem healthSystem;
    [SerializeField] private int _damage = 10;
    public static Action OnDeath;
    public Action<Transform> OnEnemyDeathActionForDropItem;
    public EnemyAnimController enemyAnimController;
    public HealthSystem HealthSystem { get => healthSystem; }
    

    private void Awake()
    {
        StartCoroutine(AssignPlayer());
        healthSystem = new HealthSystem(health);
        healthSystem._currentHealth = 100;
    }

    private void OnEnable()
    {
        agent.speed = 3;
        GetComponent<Collider2D>().enabled = true;
    }

    private IEnumerator AssignPlayer()
    {
        while (GameManager.Instance == null || GameManager.Instance.GetPlayer() == null)
        {
            yield return null;
        }

        player = GameManager.Instance.GetPlayer().transform;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void Update()
    {
        
        enemyAnimController.IsWalking = agent.speed != 0 ? true : false;
        if (player != null)
        {
            agent.SetDestination(player.position);
            Vector3 direction = (player.position - transform.position).normalized;
            if (direction.magnitude > 0.1f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Projectile>())
        {
            healthSystem.TakeDamage(other.GetComponent<Projectile>().Damage, gameObject);
            enemyAnimController.AnimatorBody.SetTrigger("isDamage");
            if (healthSystem.GetHealth() <= 0)
            {
                StartCoroutine(DiedEnemy());
            }
        }

        if (other.gameObject.GetComponent<PlayerController>())
        {
            enemyAnimController.AnimatorBody.SetBool(enemyAnimController.IsAttackingHash, true);
        }
    }

    IEnumerator DiedEnemy()
    {
        enemyAnimController.AnimatorBody.SetTrigger("isDeath");
        agent.speed = 0;
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        ObjectPoolingManager.Instance.ReturnEnemyToPool(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemyAnimController.AnimatorBody.SetBool(enemyAnimController.IsAttackingHash, false);
    }


    public void Shoot()
    {
        if (PlayerController.Instance.gameObject)
        {
            PlayerController.Instance.healthSystem.TakeDamage(_damage, PlayerController.Instance.gameObject);
            if (PlayerController.Instance.healthSystem.GetHealth()>0)
            {
                OnDeath?.Invoke();
            }
        }
    }

}
