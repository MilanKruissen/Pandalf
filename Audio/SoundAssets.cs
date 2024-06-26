using UnityEngine;

public class AudioAssets : MonoBehaviour
{
    private static AudioAssets _i;

    public static AudioAssets I
    {
        get
        {
            if (_i == null)
            {
                _i = (Instantiate(Resources.Load<AudioAssets>("AudioAssets")));
            }
            return _i;
        }
    }

    // Sounds
    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sounds sound;
        public AudioClip audioClip;
    }
}