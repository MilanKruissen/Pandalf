using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private String village;
    [SerializeField] private String[] levels;

    [SerializeField] private int levelIndex = 0;
    private bool isFirstLoad = true;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        anim = GetComponent<Animator>();

    }

    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }
    
    private void OnFadeOutComplete()
    {
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        FindObjectOfType<PlayerLife>()._health = FindObjectOfType<PlayerLife>()._maxHealth;
        PlayerStaff Staff = FindFirstObjectByType<PlayerStaff>();
        Staff.ResetValuesNewLevel();

        if (isFirstLoad)
        {
            // Load the village scene the first time
            SceneManager.LoadScene(village);
            isFirstLoad = false; // Set the flag to false after the first load
        }
        else
        {
            if (SceneManager.GetActiveScene().name == village)
            {
                FindObjectOfType<GameManager>().UpdateLevelStatistics();
                FindObjectOfType<PlayerMovement>().speed = FindObjectOfType<PlayerMovement>().baseSpeed;

                int randomLevelInt = Random.Range(0, levels.Length);
                SceneManager.LoadScene(levels[randomLevelInt]);
            }
            else
            {
                levelIndex++;

                if (levelIndex == 3)
                {
                    FindObjectOfType<PlayerMovement>().speed = FindObjectOfType<PlayerMovement>().baseSpeedVillage;
                    SceneManager.LoadScene(village);

                    levelIndex = 0; // Reset the index after loading the village
                }
                else
                {
                    int randomLevelInt = Random.Range(0, levels.Length);
                    SceneManager.LoadScene(levels[randomLevelInt]);
                }
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("a");
        FadeIn();
    }
    
    private void FadeIn()
    {
        if (anim == null)
        {
            Debug.Log("no anim");
            return;
        }
        anim.SetTrigger("FadeIn");
    }
}
