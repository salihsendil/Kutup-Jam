using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public GameObject lockLevel; 
        public Image playButtonImage; 
        public Button playButton;
        public Item[] items;
    }
    [Header("Level Setting")]
    public Level[] levels; 
    public int startingLevel = 0;
    public Sprite playButtonUnlockImage;
    public Sprite playButtonLockImage;
    
    [Header("Game Level Reset Setting")]
    public bool resetResources = true;
    public bool resetLevels = true;
    public GameObject gamePlayScene;
    public GameObject levelScene;
    private void Start()
    {
        InitializeLevels();
        levels[startingLevel].
            playButton.onClick.AddListener(GetGamePlayScene);
    }

    private void GetGamePlayScene()
    {
        gamePlayScene.SetActive(true);
        levelScene.SetActive(false);
    }
    
    private void InitializeLevels()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (i < startingLevel)
            {
                UnlockLevel(levels[i]); 
            }
            else
            {
                LockLevel(levels[i]); 
            }
        }
    }

    private void Update()
    {
        ResetGameProgress();
        for (int i = 0; i < levels.Length - 1; i++)
        {
            if (IsLevelCompleted(levels[i]))
            {
                UnlockLevel(levels[i + 1]);
            }
        }
    }

    private bool IsLevelCompleted(Level level)
    {
        foreach (var item in level.items)
        {
            if (!item.IsCompleted())
            {
                return false; 
            }
        }

        return true;
    }

    private void UnlockLevel(Level level)
    {
        if (level.lockLevel != null)
        {
            level.lockLevel.SetActive(false); 
        }

        if (level.playButtonImage != null)
        {
            level.playButtonImage.sprite = playButtonUnlockImage;
            startingLevel++;
            levels[startingLevel].
                playButton.onClick.AddListener(GetGamePlayScene);
        }
    }

    private void LockLevel(Level level)
    {
        if (level.lockLevel != null)
        {
            level.lockLevel.SetActive(true);
        }

        if (level.playButtonImage != null)
        {
            level.playButtonImage.sprite = playButtonLockImage; 
        }
    }
    public void ResetGameProgress()
    {
        if (resetResources)
        {
            ResourceManager.Instance.ResetResources();
        }
        if (resetLevels)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}