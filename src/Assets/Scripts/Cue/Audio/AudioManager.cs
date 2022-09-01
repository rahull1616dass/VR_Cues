using DevCommons.Sound;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class AudioManager : MonoBehaviour
{
    public enum AudioType
    {
        None = 0,
        Sound,
        Music
    }

    public static AudioManager Instance;

    private int m_AudioCount;

    private AudioSource m_BGSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        m_BGSound = this.GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("music") == 1)
            EndBGSound();
    }


    public AudioSource Play(AudioClip clip, AudioType audioType)
    {
        if (audioType == AudioType.Music)
            if (PlayerPrefs.GetInt("music") == 1)
                return null;
        if (audioType == AudioType.Sound)
            if (PlayerPrefs.GetInt("sound") == 1)
                return null;

        if (clip != null /*&& m_AudioCount < 10*/)
        {
            //m_AudioCount++;
            return AudioPlayer.Play(new AudioPlayerData
            {
                audioClip = clip,
                oneShot = true
            }/*, () => { m_AudioCount--; }*/).GetAudioSource();
        }
        else
            return null;
    }
    public AudioSource Play(AudioClip clip, float volume, bool IsLoop, bool withPlayer, AudioType audioType)
    {
        if (audioType == AudioType.Music)
            if (PlayerPrefs.GetInt("music") == 1)
                return null;
        if (audioType == AudioType.Sound)
            if (PlayerPrefs.GetInt("sound") == 1)
                return null;

        Debug.Log("PlayingAudio>" + clip.ToString());
        if (clip != null /*&&  m_AudioCount < 10*/)
        {
            //m_AudioCount++;
            return AudioPlayer.Play(new AudioPlayerData
            {
                audioClip = clip,
                oneShot = false,
                loop = IsLoop,
                volume = volume,
                withCamera = withPlayer
            }/*, () => { m_AudioCount--; }*/).GetAudioSource();
        }
        else
            return null;
    }

    public void StartBGSound()
    {
        //m_BGSound = Play(AudioData.EAudio.BGSound, 0.3f, true);
        //DontDestroyOnLoad(m_BGSound);

        m_BGSound.Play();
    }

    public void EndBGSound()
    {
        m_BGSound.Stop();
    }

}
