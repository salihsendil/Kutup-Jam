using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI seedText;
    public TextMeshProUGUI waterText;
    public TextMeshProUGUI ironText;
    public Image healthBar;
    private float maxHealth = 100f;
    public float healIncrease = 10f;
    public float currentHealth;
    private Coroutine currentFillCoroutine;
    [SerializeField] private GameObject _gameOverPanel;
    
    private void Start()
    {
        UpdateUI();
        GameOverPanel(false);
        currentHealth = PlayerController.Instance.health;
        healthBar.fillAmount = currentHealth;
        PlayerController.Instance.updatedHealth += Heal;
        Enemy.OnDeath += UpdateBar;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            StartCoroutine(GameOverPanelDelay());
        }

    }

    public void UpdateBar()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBarSmooth();
    }

    public void Heal()
    {
        currentHealth += healIncrease;
       
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.fillAmount = currentHealth;
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
            elapsed += Time.deltaTime*30;
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
        seedText.text = PlayerPrefs.GetInt("Seeds", 0).ToString();
    }

    private void GameOverPanel(bool boolean)
    {
        _gameOverPanel.SetActive(boolean);

    }

    IEnumerator GameOverPanelDelay()
    {
        GameOverPanel(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
