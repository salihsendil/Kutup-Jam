using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform target; 
    private NavMeshAgent agent; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        // Hedefe doğru hareket et
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void Update()
    {
        // Hedef sürekli değişiyorsa, hedef konumunu güncelle
        if (target != null)
        {
            agent.SetDestination(target.position);
        }

        // Düşmanı hedefe doğru çevir (isteğe bağlı)
        Vector3 direction = (target.position - transform.position).normalized;
        if (direction.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
