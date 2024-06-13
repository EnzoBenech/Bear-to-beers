using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public float health = 100;
    public float maxHealth =100;

    public Image healthBarImage;
    public TextMeshProUGUI healthText;

    // Update is called once per frame
    void Update()
    {
        healthBarImage.fillAmount = health / maxHealth;
        healthText.text = health + " / " + maxHealth;
        health = Mathf.Clamp(health, 0f, maxHealth);
    }

    public void DamageButton(int damageAmount)
    {
        health -= damageAmount;
    }

    public void HealButton(int healAmount)
    {
        health += healAmount;
    }
}
