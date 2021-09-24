using UnityEngine;

[CreateAssetMenu(fileName = "SlowTimeItem", menuName = "Inventory/SlowTime")]
public class SlowTimeItem : Item
{
    public int duration;
    public float slowCoefficient;
    public int cooldown;
    public float speedUpCoefficient;
}
