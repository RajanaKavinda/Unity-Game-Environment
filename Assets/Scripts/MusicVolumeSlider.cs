using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public GameObject objectMusic;

    private AudioSource bgMusic;
    private float musicVolume = 0f;

    void Start()
    {
        objectMusic = GameObject.FindWithTag("GameMusic");
        bgMusic = objectMusic.GetComponent<AudioSource>();

        // Set the initial volume based on PlayerPrefs or default to 0.5f
        musicVolume = PlayerPrefs.GetFloat("Volume"); 
        bgMusic.volume = musicVolume;
        volumeSlider.value = musicVolume;
    }

    public void VolumeUpdater(float volume)
    {
        musicVolume = volume;
        bgMusic.volume = musicVolume;
        PlayerPrefs.SetFloat("Volume", musicVolume);
    }

    public void MusicReset()
    {
        PlayerPrefs.DeleteKey("Volume");
        bgMusic.volume = 1f;
        volumeSlider.value = 1f;
    }
}
