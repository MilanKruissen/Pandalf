using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private bool startedLevelTheme = false;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check the name of the loaded scene and play the corresponding music
        if (scene.name == "Village")
        {
            //SoundManager.PlayBackgroundMusic(SoundManager.Sounds.VillageTheme, 0.4f);
        }
        else
        {
            // Only play the level theme if it's not already playing
            if (!SoundManager.IsPlaying(SoundManager.Sounds.LevelTheme))
            {
                //SoundManager.PlayBackgroundMusic(SoundManager.Sounds.LevelTheme, 0.3f);
            }
        }

        SoundManager.PlaySound(SoundManager.Sounds.AmbienceOne, 0.4f);
    }
}
