using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Audio;

public class BMusicManager : MonoSingleton<BMusicManager>
{
    private bool _isInit = false;
    // ÒôÆµÔ´×é¼þ
    [SerializeField]private AudioMixer audioMixer;
    [SerializeField]private AudioSource bgmSource;
    [SerializeField]private AudioSource sfxSource;

    void Awake()
    {
        OnSingletonInit();
        _isInit = true;
    }
    public override void OnSingletonInit()
    {
        if (_isInit) return;
        base.OnSingletonInit();
        DontDestroyOnLoad(gameObject);

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.playOnAwake = false;

        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;

        bgmSource.volume = 0.1f;
        PlayBGM("BattleMusic");
    }
    public void PlayBGM(string voiceName, bool loop = true)
    {
        AudioClip clip = Resources.Load<AudioClip>("Voice/BackgroundMusic/" + voiceName);
        bgmSource.clip = clip;
        bgmSource.loop = loop;
        bgmSource.Play();
    }
    public void PlaySFX(string voiceName,float delayTime = 0)
    {
        AudioClip clip = Resources.Load<AudioClip>("Voice/EffectVoices/"+voiceName);
        if(delayTime <= 0) sfxSource.PlayOneShot(clip);
        else StartCoroutine(DelayPlaySFX(clip, delayTime));
        
    }
    public void StopBGM()
    {
        bgmSource.Stop();
    }
    public void SetVolume(float volume,string mixerGroup = "Master")
    {
        audioMixer.SetFloat(mixerGroup,volume * 20);
    }
    IEnumerator DelayPlaySFX(AudioClip clip, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        sfxSource.PlayOneShot(clip);
    }
}
