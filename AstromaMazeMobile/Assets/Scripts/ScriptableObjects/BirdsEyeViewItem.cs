using UnityEngine;

[CreateAssetMenu(fileName = "New BirdsEyeViewItem", menuName = "Inventory/BirdsEyeView")]
public class BirdsEyeViewItem : Item
{
    [Range(1, 100)] public float maxHeight = 35f;
    [Range(1, 8)] public float coroutineDuration = 1.5f;
    [Range(0, 5)] public float maxHeightDuration = 0.5f;
    [Range(5, 30)] public float birdsEyeViewCooldown = 10f;
}
