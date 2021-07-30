using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCooldown : MonoBehaviour
{
    [SerializeField] private Image imageCooldown, imageEdge;
    public Image skillImage, imageFrame;
    public BirdsEyeView birdsEyeView;

    public bool isCooldown = false;
    public float cooldownTime;
    private float cooldownTimer = 0f;

    private void Start()
    {
        birdsEyeView.OnBirdsEyeViewEvent += UseSpell;
        imageCooldown.fillAmount = 0;
        imageFrame.enabled = true;
    }

    private void OnDestroy()
    {
        birdsEyeView.OnBirdsEyeViewEvent -= UseSpell;
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
            isCooldown = false;
            imageCooldown.fillAmount = 0f;
            imageFrame.enabled = true;
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
