using System;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemRequirement itemRequirement; 
    private Image itemImage; 
    private Button itemButton; 
    private Transform resourceLayout;
    
    private void Start()
    {
        resourceLayout= transform.GetChild(0);
        itemImage=transform.GetChild(1).transform.GetComponent<Image>();
        itemButton=resourceLayout.gameObject.GetComponent<Button>();
        LoadItemState();
        itemButton.onClick.AddListener(OnItemButtonClicked);
        UpdateResourceDisplay();
    }

    private void OnItemButtonClicked()
    {
        if (ResourceManager.Instance.HasEnoughResources(itemRequirement))
        {
            itemImage.gameObject.SetActive(true);
            itemImage.sprite = itemRequirement.updatedSprite;
            ConsumeAllResources(); 
            MarkAsCompleted();
            resourceLayout.gameObject.SetActive(false);
        }
    }
    public void MarkAsCompleted()
    {
        PlayerPrefs.SetInt(itemRequirement.itemKey, 1);
        PlayerPrefs.Save();
    }
    private void LoadItemState()
    {
        if (IsCompleted())
        {
            itemImage.sprite = itemRequirement.updatedSprite;
            itemImage.gameObject.SetActive(true);
            resourceLayout.gameObject.SetActive(false);
        }
    }
    public bool IsCompleted()
    {
        return PlayerPrefs.GetInt(itemRequirement.itemKey, 0) == 1;
    }
    private void UpdateResourceDisplay()
    {
        foreach (Transform child in resourceLayout)
        {
            Destroy(child.gameObject);
        }
        if (itemRequirement.requiredWood > 0)
        {
            CreateResourceDisplay("Wood", itemRequirement.requiredWood);
        }

        if (itemRequirement.requiredWater > 0)
        {
            CreateResourceDisplay("Water", itemRequirement.requiredWater);
        }

        if (itemRequirement.requiredIron > 0)
        {
            CreateResourceDisplay("Iron", itemRequirement.requiredIron);
        }

        if (itemRequirement.requiredSeeds > 0)
        {
            CreateResourceDisplay("Seeds", itemRequirement.requiredSeeds);
        }
    }
    private void CreateResourceDisplay(string resourceName, int amount)
    {
        GameObject resourcePrefab = ResourceManager.Instance.resourcePrefab;
        GameObject resourceObj = Instantiate(resourcePrefab, resourceLayout);
        Image resourceImage = resourceObj.GetComponent<ResourcePrefab>().image;
        TextMeshProUGUI resourceText = resourceObj.GetComponent<ResourcePrefab>().text;

        resourceImage.sprite = ResourceManager.Instance.GetResourceSprite(resourceName);
        resourceText.text = amount.ToString();
    }
    private void ConsumeAllResources()
    {
        if (itemRequirement.requiredWood > 0)
        {
            ResourceManager.Instance.ConsumeResource("Wood", itemRequirement.requiredWood);
        }

        if (itemRequirement.requiredWater > 0)
        {
            ResourceManager.Instance.ConsumeResource("Water", itemRequirement.requiredWater);
        }

        if (itemRequirement.requiredIron > 0)
        {
            ResourceManager.Instance.ConsumeResource("Iron", itemRequirement.requiredIron);
        }

        if (itemRequirement.requiredSeeds > 0)
        {
            ResourceManager.Instance.ConsumeResource("Seeds", itemRequirement.requiredSeeds);
        }
    }
}
