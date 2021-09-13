using UnityEngine;

[CreateAssetMenu(fileName ="New SprayItem", menuName = "Inventory/Spray")]
public class SprayItem : Item
{
    public int sprayAmount = 500; 
    public float maxDistance = 10f;
    public float brushSize = 0.15f;
    public float baseLifetime = 15f;
    public float fadeRate = 0.05f;
    public float fadeDelay = 0.5f;
    public Color paintColor;
}
