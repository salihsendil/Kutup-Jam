using UnityEngine;

public class CollectiableItems : MonoBehaviour
{
    public string _name;
    [SerializeField] private int _amount;
    
    public CollectiableItems(int amount)
    {
        _amount = amount;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            ResourceManager.Instance.LoadResources();
            Debug.Log("_name"+_name);
            switch (_name)
            {
                case "Wood":
                   int currentWoodAmount= PlayerPrefs.GetInt("Wood", 0);
                    PlayerPrefs.SetInt(_name,currentWoodAmount+ _amount);
                    ResourceManager.Instance.LoadResources();
                    break;
                case "Water":
                    int currentWaterAmount= PlayerPrefs.GetInt("Water", 0);
                    PlayerPrefs.SetInt(_name,currentWaterAmount+ _amount);
                    ResourceManager.Instance.LoadResources();
                    break;
                case "Iron":
                    int currentIronAmount= PlayerPrefs.GetInt("Water", 0);
                    PlayerPrefs.SetInt(_name,currentIronAmount+ _amount);
                    ResourceManager.Instance.LoadResources();

                    break;
                case "Seeds":
                    int currentSeedsAmount= PlayerPrefs.GetInt("Seeds", 0);
                    PlayerPrefs.SetInt(_name,currentSeedsAmount+ _amount);
                    ResourceManager.Instance.LoadResources();
                    break;
            }
            Destroy(gameObject);
            
        }
    }

}
