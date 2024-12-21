using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    public int health;
    private void Awake()
    {
        StartCoroutine(AssignPlayer());
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
}
