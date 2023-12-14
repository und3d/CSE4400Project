using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBarUI : MonoBehaviour
{
   public Character character;
   public TextMeshProUGUI nameText;
   public Image healthBarFill;

   void OnEnable()
   {
    character.onTakeDamage += UpdateHealthBar;
    character.onHeal += UpdateHealthBar;
   }

   void OnDisable()
   {
    character.onTakeDamage -= UpdateHealthBar;
    character.onHeal -= UpdateHealthBar;
   }

   void Start()
   {
    SetNameText(character.DisplayName);
   }
   void SetNameText (string text)
   {
    nameText.text = text;
   }

   void UpdateHealthBar()
   {
    float healthPercent = (float)character.CurHP / (float)character.MaxHP;
    healthBarFill.fillAmount = healthPercent;
   }
}
