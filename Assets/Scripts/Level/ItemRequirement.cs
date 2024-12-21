using UnityEngine;

[CreateAssetMenu(fileName = "NewItemRequirement", menuName = "Item/Requirement")]
public class ItemRequirement : ScriptableObject
{
    public string itemKey;
    public Sprite updatedSprite; 
    public int requiredWood; 
    public int requiredWater; 
    public int requiredIron; 
    public int requiredSeeds; 
}
