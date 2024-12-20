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
    private void Awake()
    {
        StartCoroutine(AssignPlayer());
        healthSystem = new HealthSystem(health);
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
        EnemyAnimController.Instance.IsWalking = agent.speed != 0 ? true : false;

        //if ((transform.position - agent.destination).magnitude<agent.stoppingDistance)
        //{
        //    agent.Stop();
        //    EnemyAnimController.Instance.AnimatorBody.SetTrigger("isAttacking");
        //}

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
        }

        if (other.gameObject.GetComponent<PlayerController>())
        {
            EnemyAnimController.Instance.AnimatorBody.SetBool(EnemyAnimController.Instance.IsAttackingHash, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyAnimController.Instance.AnimatorBody.SetBool(EnemyAnimController.Instance.IsAttackingHash, false);
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
