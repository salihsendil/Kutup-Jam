using UnityEngine;

public class CollectiableItems : MonoBehaviour
{
    private string _name;
    [SerializeField] private int _amount;

    public string Name { get => _name; } 
    public CollectiableItems(int amount)
    {
        _amount = amount;
    }

    private void Start()
    {
        _name = gameObject.name;
    }

    public string Collect()
    {
        Destroy(gameObject);
        return Name;
    }
}
