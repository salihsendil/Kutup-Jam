using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

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
        Debug.Log("health:" + _currentHealth);
        Die(obj);
    }

    void Die(GameObject obj)
    {
        if (_currentHealth <= 0)
        {
            Destroy(obj);
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