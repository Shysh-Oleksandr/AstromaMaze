using UnityEngine;

[CreateAssetMenu(fileName = "New CompassItem", menuName = "Inventory/Compass")]
public class CompassItem : Item
{
    public bool isBought = false;
    public bool canRotateToNorth = false;
    public bool canShowDistance = false;
    public float compassCooldown = 60f;
}
