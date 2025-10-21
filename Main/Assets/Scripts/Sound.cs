using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{

<<<<<<< HEAD
    public string name;

    public AudioClip clip;
    public AudioMixerGroup mixer;

    [Range(0f, 1f)]
    public float volume = 1;

    [Range(-3f, 3f)]
    public float pitch = 1;

    public bool loop = false;

    [HideInInspector]
    public AudioSource source;
=======
    public string name;//name to identify the sound

    public AudioClip clip;//the audio clip that will play
    public AudioMixerGroup mixer;//control volume from mixer

    //controls playback volume
    [Range(0f, 1f)]
    public float volume = 1;

    //control pitch multiplier
    [Range(-3f, 3f)]
    public float pitch = 1;

    public bool loop = false;//whether sound will continue in a loop or not

    [HideInInspector]
    public AudioSource source;//used to play the clip
>>>>>>> AccountLink

}