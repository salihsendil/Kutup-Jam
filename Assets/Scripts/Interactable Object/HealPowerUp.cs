using UnityEngine;

public class HealPowerUp : MonoBehaviour, IInteractable
{
    [SerializeField] private int healUpAmount = 10;

    public void Collect()
    {
        PlayerController.Instance.healthSystem.HealUp(healUpAmount);
        Destroy(gameObject);
    }
}
