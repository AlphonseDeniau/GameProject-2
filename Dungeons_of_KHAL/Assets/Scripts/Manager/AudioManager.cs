using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public enum EAudioType
    {
        Music,
        Sound,
    }

    [Serializable]
    public class AudioSound
    {
        public AudioSound(string name, EAudioType type, AudioSource source, AudioClip clip)
        {
            m_Name = name;
            m_Type = type;
            m_Source = source;
            m_Clip = clip;
        }

        public string m_Name;
        public EAudioType m_Type;
        public AudioSource m_Source;
        public AudioClip m_Clip;
    }

    [Header("Default Sources")]
    [SerializeField] private AudioSource m_SourceSound;
    [SerializeField] private AudioSource m_SourceMusic;

    [Header("Volume Settings")]
    [SerializeField] private int m_SoundVolume = 50;
    [SerializeField] private int m_MusicVolume = 50;

    [Header("Clips")]
    [SerializeField] private List<AudioSound> m_AudioSounds = new List<AudioSound>();
    public List<AudioSound> AudioSounds => m_AudioSounds;

    // SOUND \\
    public void AddSound(string name, AudioClip clip, AudioSource source = null)
    {
        if (m_AudioSounds.Exists(x => x.m_Name.Equals(name) || x.m_Clip.Equals(clip)))
            return;

        AudioSound sound = new AudioSound(name, EAudioType.Sound, source != null ? source : m_SourceSound, clip);
        m_AudioSounds.Add(sound);
    }

    public void RemoveSound(string name)
    {
        if (!m_AudioSounds.Exists(x => x.m_Name.Equals(name) && x.m_Type == EAudioType.Sound))
            return;
        AudioSound sound = m_AudioSounds.Find(x => x.m_Name.Equals(name) && x.m_Type == EAudioType.Sound);
        m_AudioSounds.Remove(sound);
    }

    public void PlaySound(string name)
    {
        List<AudioSound> sounds = m_AudioSounds.FindAll(x => x.m_Type == EAudioType.Sound);

        if (sounds.Exists(x => x.m_Name == name))
        {
            AudioSound sound = sounds.Find(x => x.Equals(name));
            sound.m_Source.PlayOneShot(sound.m_Clip);
        }
    }

    // MUSIC \\
    public void AddMusic(string name, AudioClip clip, AudioSource source = null)
    {
        if (m_AudioSounds.Exists(x => x.m_Name.Equals(name) || x.m_Clip.Equals(clip)))
            return;
        AudioSound music = new AudioSound(name, EAudioType.Music, source != null ? source : m_SourceMusic, clip);
        m_AudioSounds.Add(music);
    }

    public void RemoveMusic(string name)
    {
        if (!m_AudioSounds.Exists(x => x.m_Name.Equals(name) && x.m_Type == EAudioType.Music))
            return;
        AudioSound music = m_AudioSounds.Find(x => x.m_Name.Equals(name) && x.m_Type == EAudioType.Music);
        m_AudioSounds.Remove(music);
    }

    public void PlayMusic(string name)
    {
        List<AudioSound> musics = m_AudioSounds.FindAll(x => x.m_Type == EAudioType.Music);

        if (musics.Exists(x => x.m_Name == name))
        {
            AudioSound music = musics.Find(x => x.Equals(name));
            if (music.m_Source.isPlaying && music.m_Source.clip == music.m_Clip)
                return;

            if (music.m_Source.isPlaying && music.m_Source.clip != music.m_Clip)
                music.m_Source.Stop();

            music.m_Source.loop = true;
            music.m_Source.clip = music.m_Clip;
            music.m_Source.Play();
        }
    }

    public void StopMusic(string name)
    {
        List<AudioSound> musics = m_AudioSounds.FindAll(x => x.m_Type == EAudioType.Music);

        if (musics.Exists(x => x.m_Name == name))
        {
            AudioSound music = musics.Find(x => x.Equals(name));
            if (music.m_Source.isPlaying == false)
                return;
            music.m_Source.Stop();
        }
    }

    public void PauseMusic(string name, bool pause)
    {
        List<AudioSound> musics = m_AudioSounds.FindAll(x => x.m_Type == EAudioType.Music);

        if (musics.Exists(x => x.m_Name == name))
        {
            AudioSound music = musics.Find(x => x.Equals(name));
            if (music.m_Source.clip != music.m_Clip)
                return;
            if (pause)
            {
                music.m_Source.Pause();
            } 
            else
            {
                music.m_Source.UnPause();
            }
        }
    }
}
