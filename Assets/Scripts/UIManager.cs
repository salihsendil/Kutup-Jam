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
    public float currentHealth;
    public float smoothSpeed = 2f;
    private int incrementHealth = 10;
    public float maxHealth = 100f;
    private float targetFillAmount;
    private Coroutine currentFillCoroutine;
    [SerializeField] private GameObject _gameOverPanel;
    public SceneController sceneController;
    
    private void Start()
    {
        UpdateUI();
        GameOverPanel(false);
        currentHealth = PlayerController.Instance.healthSystem._currentHealth;
        targetFillAmount = currentHealth / maxHealth; 
        healthBar.fillAmount = targetFillAmount;
        PlayerController.Instance.updatedHealth += IncreaseHealth;
        Enemy.OnDeath += DecreaseHealth;
    }

    private void Update()
    {
        if (currentHealth <= 10)
        {
            StartCoroutine(GameOverPanelDelay());
        }
        if (healthBar != null)
        {
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, targetFillAmount, smoothSpeed * Time.deltaTime);
        }

    }
    public void DecreaseHealth()
    {
        currentHealth = Mathf.Clamp(currentHealth - incrementHealth, 0f, maxHealth);
        targetFillAmount = currentHealth / maxHealth; 
    }

    public void IncreaseHealth()
    {
        currentHealth = Mathf.Clamp(currentHealth + incrementHealth, 0f, maxHealth);
        targetFillAmount = currentHealth / maxHealth;
        Debug.Log("Increased Health: " + currentHealth);
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
        yield return new WaitForSeconds(3f);
        GameOverPanel(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
