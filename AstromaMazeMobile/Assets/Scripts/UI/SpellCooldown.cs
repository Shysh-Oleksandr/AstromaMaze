using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCooldown : MonoBehaviour
{
    [SerializeField] private Image imageCooldown;
    public Image skillImage, imageFrame;

    public bool isCooldown = false;
    public float cooldownTime;
    private float cooldownTimer = 0f;

    private void Start()
    {
        imageCooldown.fillAmount = 0;
        imageFrame.enabled = true;
    }

    private void Update()
    {
        if (isCooldown)
        {
            ApplyCooldown();
        }
    }

    public void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if(cooldownTimer < 0)
        {
            AudioManager.Instance.Play("BackTweenWhoosh");

            isCooldown = false;
            imageCooldown.fillAmount = 0f;
            imageFrame.enabled = true;

            var tempColor = skillImage.color;
            tempColor.a = 1f;
            skillImage.color = tempColor;
        }
        else
        {
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
        }
    }

    public void UseSpell()
    {
        if (!isCooldown)
        {
            isCooldown = true;
            cooldownTimer = cooldownTime;
            imageFrame.enabled = false;
        }
    }
}
