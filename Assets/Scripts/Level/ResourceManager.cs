using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public int wood;
    public int water;
    public int iron;
    public int seeds;
    
    public Sprite woodSprite;
    public Sprite waterSprite;
    public Sprite ironSprite;
    public Sprite seedsSprite;

    public GameObject resourcePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        LoadResources();
    }
    public void SaveResources()
    {
        PlayerPrefs.SetInt("Wood", wood);
        PlayerPrefs.SetInt("Water", water);
        PlayerPrefs.SetInt("Iron", iron);
        PlayerPrefs.SetInt("Seeds", seeds);
        PlayerPrefs.Save();
    }
    public void LoadResources()
    {
        wood = PlayerPrefs.GetInt("Wood", 0);
        water = PlayerPrefs.GetInt("Water", 0);
        iron = PlayerPrefs.GetInt("Iron", 0);
        seeds = PlayerPrefs.GetInt("Seeds", 0);
    }
    public void AddResource(string resourceName, int amount)
    {
        LoadResources();
        switch (resourceName)
        {
            case "Wood":
                wood += amount;
                break;
            case "Water":
                water += amount;
                break;
            case "Iron":
                iron += amount;
                break;
            case "Seeds":
                seeds += amount;
                break;
        }
        SaveResources();
    }
    public bool HasEnoughResources(ItemRequirement requirement)
    {
        return wood >= requirement.requiredWood &&
               water >= requirement.requiredWater &&
               iron >= requirement.requiredIron &&
               seeds >= requirement.requiredSeeds;
    }
    public void ConsumeResource(string resourceName, int amount)
    {
        switch (resourceName)
        {
            case "Wood":
                wood -= amount;
                break;
            case "Water":
                water -= amount;
                break;
            case "Iron":
                iron -= amount;
                break;
            case "Seeds":
                seeds -= amount;
                break;
        }
        SaveResources();
    }
    public Sprite GetResourceSprite(string resourceName)
    {
        return resourceName switch
        {
            "Wood" => woodSprite,
            "Water" => waterSprite,
            "Iron" => ironSprite,
            "Seeds" => seedsSprite,
            _ => null,
        };
    }
    public void ResetResources()
    {
        wood = 0;
        water = 0;
        iron = 0;
        seeds = 0;
    }
}
