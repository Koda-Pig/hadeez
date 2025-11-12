using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private float volume = .3f;
    private AudioSource audioSource;
    private const string PLAYER_PREFS_MUSIC_VOLUME = "MusicVolume";

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_MUSIC_VOLUME, .3f);
        audioSource.volume = volume;
    }

    public void SetVolume(float newVolume)
    {
        audioSource.volume = volume;
        volume = newVolume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() => volume;
}
