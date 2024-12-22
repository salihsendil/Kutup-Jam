using UnityEngine;

public class HealPowerUp : MonoBehaviour, IInteractable
{
    [SerializeField] private int healUpAmount = 10;
    public bool IsInteractable=false;

    public void Collect()
    {
        PlayerController.Instance.healthSystem.HealUp(healUpAmount);
       IsInteractable = true;
        Destroy(gameObject);
    }
}
