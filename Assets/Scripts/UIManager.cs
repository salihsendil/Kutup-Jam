using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   public TextMeshProUGUI woodText;
   public TextMeshProUGUI seedText;
   public TextMeshProUGUI waterText;
   public TextMeshProUGUI ironText;
   public Image healthBar;
   private float maxHealth = 100f;
   private float currentHealth;
   private Coroutine currentFillCoroutine;
   private void Start()
   {
      currentHealth = maxHealth;
      UpdateUI();
   }

   private void Update()
   {
      UpdateBar();
   }

   public void UpdateBar()
   {
      currentHealth = Mathf.Clamp(PlayerController.Instance.healthSystem.GetHealth(), 0, maxHealth); 
      UpdateHealthBarSmooth();
   }

   public void Heal(float healAmount)
   {
      currentHealth += healAmount;
      currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
      UpdateHealthBarSmooth();
   }

   private void UpdateHealthBarSmooth()
   {
      if (currentFillCoroutine != null)
      {
         StopCoroutine(currentFillCoroutine);
      }
      currentFillCoroutine = StartCoroutine(SmoothFill(currentHealth / maxHealth));
   }
   private IEnumerator SmoothFill(float targetFill)
   {
      if (healthBar == null) yield break;

      float startFill = healthBar.fillAmount;
      float elapsed = 0f;

      while (elapsed < 5)
      {
         elapsed += Time.deltaTime;
         healthBar.fillAmount = Mathf.Lerp(startFill, targetFill, elapsed / 5);
         yield return null;
      }

      healthBar.fillAmount = targetFill; 
   }
   private void UpdateUI()
   {
      woodText.text = PlayerPrefs.GetInt("Wood", 0).ToString();
      waterText.text = PlayerPrefs.GetInt("Water", 0).ToString();
      ironText.text = PlayerPrefs.GetInt("Iron", 0).ToString();
      seedText.text  = PlayerPrefs.GetInt("Seeds", 0).ToString();
   }
}
