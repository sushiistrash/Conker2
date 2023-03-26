using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string Name;

    public AudioClip Clip;
    public bool IsPitchRandom = false;

    [Range(0f, 1f)] public float Volume = .5f;
    [Range(.1f, 3f)] public float Pitch = 1f;

    public bool Mute = false;
    public bool PlayOnAwake = false;
    public bool Loop = false;

    public AudioMixerGroup Group;

    [HideInInspector] public AudioSource Source;
    public SoundType soundType;

    public enum SoundType
    {
        Music,
        SFX,
    }
}
