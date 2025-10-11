using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicVol, sfxVol;//References to UI sliders for adjusting music and SFX volume
    public AudioMixer MainAudioMixer;//Reference to main Audiomixer

    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);//set the quailty of the video ranging from low to high
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("MusicVol"))// checks if the volume has saved preference before
        {
            LoadVolume();//if saved prefernce exist, load and apply preferences
        }
        else
        {
            //if no saved exists, set to default based on sliders
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = musicVol.value;
        MainAudioMixer.SetFloat("MusicVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVol", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxVol.value;//get current value from SFX volume slider
        MainAudioMixer.SetFloat("SFXVol", Mathf.Log10(volume) * 20);//Apply logarithmic conversion and set parameter in the mixer
        PlayerPrefs.SetFloat("SFXVol", volume);//save current SFX volume
    }

    private void LoadVolume()
    {
        //Retrieve saved values and set them on sliders
        musicVol.value = PlayerPrefs.GetFloat("MusicVol");
        sfxVol.value = PlayerPrefs.GetFloat("SFXVol");
        //Apply value to AudioMixer
        SetMusicVolume();
        SetSFXVolume();
    }
   
}
