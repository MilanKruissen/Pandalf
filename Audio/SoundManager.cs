using UnityEngine;

public static class SoundManager 
{
    public enum Sounds
    {
        PandalfStepOne,
        PandalfStepTwo,
        FireballShoot,
        FireballImpact,
        AmbienceOne,
        VillageTheme,
        HitSound,
        KillSound,
        ZapShootOne,
        ZapShootTwo,
        ZapHit,
        HoneyThrow,
        HoneySplatter,
        ButtonClicked,
        LevelTheme,
        SingleSlash,
        MultiSlash,
        SawSound,
        GrowlShortOne,
        GrowlShortTwo,
        GrowlShortThree,
        GrowlShortFour,
        GrowlShortVife,
        GrowlLongOne,
        BuySound,
        LevelUp,
        CheeringCrowd
    }

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    private static GameObject backgroundMusicGameObject;
    private static AudioSource backgroundMusicAudioSource;

    public static void PlaySound(Sounds sound, float volume)
    {
        if (oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject("Sound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }

        oneShotAudioSource.PlayOneShot(GetAudioClip(sound), volume);
    }

    public static void PlayBackgroundMusic(Sounds sound, float volume)
    {
        if (backgroundMusicGameObject == null)
        {
            backgroundMusicGameObject = new GameObject("BackgroundMusic");
            backgroundMusicAudioSource = backgroundMusicGameObject.AddComponent<AudioSource>();
            backgroundMusicAudioSource.loop = true; // Set looping for background music
            Object.DontDestroyOnLoad(backgroundMusicGameObject);
        }

        AudioClip audioClip = GetAudioClip(sound);
        if (backgroundMusicAudioSource.clip != audioClip)
        {
            backgroundMusicAudioSource.clip = audioClip;
            backgroundMusicAudioSource.volume = volume;
            backgroundMusicAudioSource.Play();
        }
    }

    public static bool IsPlaying(Sounds sound)
    {
        if (backgroundMusicAudioSource == null) return false;
        return backgroundMusicAudioSource.clip == GetAudioClip(sound) && backgroundMusicAudioSource.isPlaying;
    }

    private static AudioClip GetAudioClip(Sounds sound)
    {
        foreach (AudioAssets.SoundAudioClip soundAudioClip in AudioAssets.I.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }

        Debug.LogError("Sound" + sound + "not found!");
        return null;
    }
}
