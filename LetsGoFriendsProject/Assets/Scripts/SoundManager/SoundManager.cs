using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public float MasterVoulme => masterVoulme;
    public float BGMVolume => bgmVolume * MasterVoulme;
    public float FxVoulme => fxVoulme * MasterVoulme;

    private float masterVoulme;
    private float bgmVolume;
    private float fxVoulme;

    private AudioSource bgmAudioSourece;
    private List<AudioSource> fxAudioSourceList = new List<AudioSource>();

    private Dictionary<string, AudioClip> bgmSoundDic = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> fxSoundDic = new Dictionary<string, AudioClip>();

    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null) // instance 가 비어있다면
            {
                instance = FindObjectOfType<SoundManager>(); // 찾아준다
                if (instance == null) // 그래도 없다면
                {
                    instance = new GameObject(typeof(SoundManager).ToString()).AddComponent<SoundManager>(); // 만든다
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as SoundManager;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this);

        foreach (var audioClip in Resources.LoadAll<AudioClip>("Sound/BGM")) // Resource 폴더에있는 사운드들 담아두기
        {
            bgmSoundDic.Add(audioClip.name, audioClip);
            Debug.Log(audioClip.name);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = default;
        }
    }

    private AudioClip GetBGMSound(string name)
    {
        AudioClip result;
        if (!bgmSoundDic.TryGetValue(name, out result))
        {
            Debug.LogWarning(name + "Not Found");
        }
        return result;
    }

    private AudioClip GetFxSound(string name)
    {
        AudioClip result;
        if (!fxSoundDic.TryGetValue(name, out result))
        {
            result = Resources.Load<AudioClip>("Sound/Fx/" + name);
            if (result == null)
            {
                Debug.LogWarning(name + "Not Found");
                return null;
            }
            fxSoundDic.Add(name, result);
        }
        return result;
    }

    private AudioSource MakeAudioSourceObject(string name)
    {
        GameObject Object = new GameObject();
        Object.name = name;
        Object.transform.SetParent(gameObject.transform);

        return Object.AddComponent<AudioSource>();
    }

    private void SetAudioSource(AudioSource audioSource, AudioClip audioClip, bool isLoop, float volume, bool isMute = false)
    {
        audioSource.clip = audioClip;
        audioSource.loop = isLoop;
        audioSource.volume = volume;
        audioSource.mute = isMute;
    }

    public void AdjustMasterVolume(float newVolume)
    {
        masterVoulme = newVolume;
        AdjustBGMVolume(bgmVolume);
        AdjustFxVoulme(fxVoulme);
    }

    public void AdjustBGMVolume(float newVolume)
    {
        bgmVolume = newVolume;
        if (bgmAudioSourece != null)
        {
            bgmAudioSourece.volume = BGMVolume;
        }
    }

    public void AdjustFxVoulme(float newVolume)
    {
        fxVoulme = newVolume;
        foreach (var fxAudioSource in fxAudioSourceList)
        {
            if (fxAudioSource != null)
            {
                fxAudioSource.volume = FxVoulme;
            }
        }
    }

    public void PlayBGMSound(string name)
    {
        if (bgmAudioSourece == null)
        {
            bgmAudioSourece = MakeAudioSourceObject("BGMObject");
        }

        SetAudioSource(bgmAudioSourece, GetBGMSound(name), true, BGMVolume, false);
        bgmAudioSourece.Play();
    }

    public void PlayFXSound(string name)
    {
        foreach (var fxAudioSource in fxAudioSourceList)
        {
            if (!fxAudioSource.isPlaying)
            {
                SetAudioSource(fxAudioSource, GetFxSound(name), false, FxVoulme, false);
                fxAudioSource.Play();
                return;
            }
        }

        fxAudioSourceList.Add(MakeAudioSourceObject("FxObject"));
        PlayFXSound(name);
    }

    public void PauseBGM()
    {

        bgmAudioSourece.Pause();

    }


    public void ResumeBGM()
    {
        bgmAudioSourece.UnPause();
    }

}
