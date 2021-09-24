using UnityEngine;

[CreateAssetMenu(fileName = "New BootItem", menuName = "Inventory/Boot")]
public class BootItem : Item
{
    public float speed = 12f;
    public float baseLifetime = 5f;
    public float fadeRate = 0.05f;
    public float fadeDelay = 0.5f;
    public Color bootColor;
}
