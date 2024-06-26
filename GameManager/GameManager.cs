using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _levelIndex;
    [SerializeField] private float _amountOfWaves = 2f;
    [SerializeField] private float totalWeight = 6f;
    [SerializeField] private float RepeatInterval = 5f;

    public int currentRound = 0;

    private void Start()
    {
        StartCoroutine(PlayAmbienceSound());
    }

    public void UpdateLevelStatistics()
    {
        _levelIndex += 1;
        _amountOfWaves += 0.4f;
        totalWeight += 4f;
    }
    
    // Getter functions
    public int GetLevelIndex()
    {
        return _levelIndex;
    }
    
    public int GetWaveAmount()
    {
        return (int)_amountOfWaves;
    }

    public int GetTotalWeight()
    {
        return (int)totalWeight;
    }

    private IEnumerator PlayAmbienceSound()
    {
        while (true)
        {
            SoundManager.PlaySound(SoundManager.Sounds.AmbienceOne, 0.4f);
            yield return new WaitForSeconds(RepeatInterval); 
        }
    }
}