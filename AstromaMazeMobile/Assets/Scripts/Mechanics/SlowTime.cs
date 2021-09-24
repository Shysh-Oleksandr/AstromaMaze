using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpellCooldown))]
public class SlowTime : MonoBehaviour
{
    public SlowTimeItem slowTimeItem;
    public BootItem bootItem;

    public bool isSlowTimeMode;

    private int duration;
    public float slowCoefficient;
    public int cooldown;
    private float speedUpCoefficient;
    private Button slowTimeButton;

    public delegate void OnSlowTime();
    public event OnSlowTime OnSlowTimeEvent;

    private SpellCooldown spellCooldown;


    void Start()
    {
        slowTimeButton = GetComponent<Button>();

        spellCooldown = GetComponent<SpellCooldown>();
        spellCooldown.cooldownTime = slowTimeItem.cooldown;

        OnSlowTimeEvent += spellCooldown.UseSpell;

        duration = slowTimeItem.duration;
        slowCoefficient = slowTimeItem.slowCoefficient;
        cooldown = slowTimeItem.cooldown;
        speedUpCoefficient = slowTimeItem.speedUpCoefficient;

    }

    public void SlowDownTime()
    {
        if (!spellCooldown.isCooldown && GameManager.Instance.isGameRunning)
        {
            AudioManager.Instance.Play("TimeDilation");

            isSlowTimeMode = true;
            slowTimeButton.interactable = false;
            transform.localScale = Vector3.one * 1.54f;

            var tempColor = spellCooldown.skillImage.color;
            tempColor.a = 0.8f;
            spellCooldown.skillImage.color = tempColor;

            bootItem.speed *= speedUpCoefficient;

            UIManager.Instance.iceFrame.enabled = true;

            StartCoroutine(SlowDownTimeCoroutine());
        }

    }

    IEnumerator SlowDownTimeCoroutine()
    {        
        yield return new WaitForSeconds(duration);

        AudioManager.Instance.Play("Whoosh");
        OnSlowTimeEvent?.Invoke();
        bootItem.speed /= speedUpCoefficient;
        transform.localScale = Vector3.one * 1.32f;
        UIManager.Instance.iceFrame.enabled = false;
        slowTimeButton.interactable = true;
        isSlowTimeMode = false;
    }


    private void OnDestroy()
    {
        OnSlowTimeEvent -= spellCooldown.UseSpell;
    }
}
