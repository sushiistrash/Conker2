using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem Instance;

    [SerializeField] private Sound[] _soundEffects;
    [SerializeField] private Sound[] _music;
    [SerializeField] private string _startMusicName;

    private void Awake()
    {
        if (_startMusicName == null) _startMusicName = "Main Level";

        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of audio system found!");
            return;
        }
        Instance = this;

        SetAudioSourceSettings(_soundEffects);
        SetAudioSourceSettings(_music);
    }

    private void SetAudioSourceSettings(Sound[] sound)
    {
        foreach (Sound s in sound)
        {
            if (string.IsNullOrEmpty(s.Name))
            {
                s.Name = s.Clip.name;
            }
        }

        foreach (Sound s in sound)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;

            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;

            s.Source.mute = s.Mute;
            s.Source.playOnAwake = s.PlayOnAwake;
            s.Source.loop = s.Loop;
            s.Source.outputAudioMixerGroup = s.Group;
        }
    }

    private void Start()
    {
        Play(_startMusicName);
    }

    public void Play(string name)
    {
        Sound sound = System.Array.Find(_soundEffects, sound => sound.Name == name);
        if (sound == null)
            sound = System.Array.Find(_music, sound => sound.Name == name);

        if (sound.soundType == Sound.SoundType.Music)
        {
            foreach (Sound s in _music)
            {
                if (s.Source.isPlaying)
                {
                    s.Source.Stop();
                }
            }
        }
        sound.Source.Stop();
        if (sound.IsPitchRandom)
        {
            sound.Source.pitch = Random.Range(sound.Pitch * .85f, sound.Pitch * 1.15f);
        }
        sound.Source.Play();
    }
}
