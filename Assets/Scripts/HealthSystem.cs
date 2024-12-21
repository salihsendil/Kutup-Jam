using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    public int _currentHealth;

    public HealthSystem(int healthAmount)
    {
        _maxHealth = _currentHealth = healthAmount;
    }

    public int GetHealth()
    {
        return _currentHealth;
    }

    public void TakeDamage(int damage, GameObject obj)
    {
        _currentHealth -= damage;
    }

    public void Die(GameObject obj)
    {
        if (_currentHealth <= 0)
        {
            if (obj.GetComponent<EnemyAnimController>())
            {
                EnemyAnimController.Instance.AnimatorBody.SetTrigger("isDeath");
            }
            Destroy(obj, 1f);
        }
    }

    

    public void HealUp(int healAmount)
    {
        if (_maxHealth - _currentHealth < healAmount)
        {
            _currentHealth = _maxHealth;
        }
        else
        {
            _currentHealth += healAmount;
        }
    }


}
