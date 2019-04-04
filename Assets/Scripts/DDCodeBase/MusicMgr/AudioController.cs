using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 音乐管理器
/// </summary> 

public class AudioController : MonoSingleton<AudioController>
{
    private Dictionary<MusicType, int> AudioDictionary = new Dictionary<MusicType, int>();

    private const int MaxAudioCount = 10;
    private const string ResourcePath = "Music/All/";
    private const string StreamingAssetsPath = "";
    private AudioSource BGMAudioSource;
    private AudioSource LastAudioSource;

    #region Mono Function
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    #endregion

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="musicType"></param>
    public void SoundPlay(MusicType musicType, float volume = 1)
    {
        if (AudioDictionary.ContainsKey(musicType))
        {
            //某一音效可同时播放数最大为MaxAudioCount
            if (AudioDictionary[musicType] <= MaxAudioCount)
            {
                AudioClip sound = this.GetAudioClip(musicType);
                if (sound != null)
                {
                    StartCoroutine(this.PlayClipEnd(sound, musicType));
                    this.PlayClip(sound, volume);
                    AudioDictionary[musicType]++;
                }
            }
        }
        else
        {
            AudioDictionary.Add(musicType, 1);
            AudioClip sound = this.GetAudioClip(musicType);
            if (sound != null)
            {
                StartCoroutine(this.PlayClipEnd(sound, musicType));
                this.PlayClip(sound, volume);
                AudioDictionary[musicType]++;
            }
        }
    }

    /// <summary>
    /// 暂停
    /// </summary>
    /// <param name="audioname"></param>
    public void SoundPause(string audioname)
    {
        if (this.LastAudioSource != null)
        {
            this.LastAudioSource.Pause();
        }
    }

    /// <summary>
    /// 暂停所有音效音乐
    /// </summary>
    public void SoundAllPause()
    {
        AudioSource[] allsource = FindObjectsOfType<AudioSource>();
        if (allsource != null && allsource.Length > 0)
        {
            for (int i = 0; i < allsource.Length; i++)
            {
                allsource[i].Pause();
            }
        }
    }

    /// <summary>
    /// 停止特定的音效
    /// </summary>
    /// <param name="audioname"></param>
    public void SoundStop(string audioname)
    {
        GameObject obj = this.transform.Find(audioname).gameObject;
        if (obj != null)
        {
            Destroy(obj);
        }
    }

    /// <summary>
    /// 设置音量
    /// </summary>
    public void BGMSetVolume(float volume)
    {
        if (this.BGMAudioSource != null)
        {
            this.BGMAudioSource.volume = volume;
        }
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="musicType"></param>
    /// <param name="volume"></param>
    public void BGMPlay(MusicType musicType, float volume = 1f)
    {
        BGMStop();

        AudioClip bgmsound = this.GetAudioClip(musicType);

        if (bgmsound != null)
        {
            this.PlayLoopBGMAudioClip(bgmsound, volume);
        }
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void BGMPause()
    {
        if (this.BGMAudioSource != null)
        {
            this.BGMAudioSource.Pause();
        }
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void BGMStop()
    {
        if (this.BGMAudioSource != null && this.BGMAudioSource.gameObject)
        {
            Destroy(this.BGMAudioSource.gameObject);
            this.BGMAudioSource = null;
        }
    }

    /// <summary>
    /// 重新播放
    /// </summary>
    public void BGMReplay()
    {
        if (this.BGMAudioSource != null)
        {
            this.BGMAudioSource.Play();
        }
    }

    #region 获取音效资源

    enum eResType
    {
        AB = 0,
        CLIP = 1
    }

    /// <summary>
    /// 获取音效  1.AssetBundle 2.Resources
    /// </summary>
    /// <param name="musicType"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    private AudioClip GetAudioClip(MusicType musicType, eResType type = eResType.CLIP)
    {
        AudioClip audioclip = null;
        switch (type)
        {
            case eResType.AB:
                //AssetBundle
                break;
            case eResType.CLIP:
                audioclip = GetResAudioClip(musicType);
                break;
            default:
                break;
        }
        return audioclip;
    }

    private IEnumerator GetAbAudioClip(string aduioname)
    {
        yield return null;
    }

    private AudioClip GetResAudioClip(MusicType musicType)
    {
        return ResourcesMgr.Instance.Load<AudioClip>(musicType);
    }
    #endregion

    #region 背景音乐
    /// <summary>
    /// 背景音乐控制器
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    /// <param name="isloop"></param>
    /// <param name="name"></param>
    private void PlayBGMAudioClip(AudioClip audioClip, float volume = 1f, bool isloop = false)
    {
        if (audioClip == null)
        {
            return;
        }
        else
        {
            GameObject obj = new GameObject(audioClip.ToString());
            obj.transform.parent = this.transform;
            AudioSource LoopClip = obj.AddComponent<AudioSource>();
            LoopClip.clip = audioClip;
            LoopClip.volume = volume;
            LoopClip.loop = true;
            LoopClip.pitch = 1f;
            LoopClip.Play();
            this.BGMAudioSource = LoopClip;
        }
    }

    /// <summary>
    /// 播放一次的背景音乐
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    /// <param name="name"></param>
    private void PlayOnceBGMAudioClip(AudioClip audioClip, float volume = 1f)
    {
        PlayBGMAudioClip(audioClip, volume, false);
    }

    /// <summary>
    /// 循环播放的背景音乐
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    /// <param name="name"></param>
    private void PlayLoopBGMAudioClip(AudioClip audioClip, float volume = 1f)
    {
        PlayBGMAudioClip(audioClip, volume, true);
    }

    #endregion

    #region  音效

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    /// <param name="name"></param>
    private void PlayClip(AudioClip audioClip, float volume = 1f, string name = null)
    {
        if (audioClip == null)
        {
            return;
        }
        else
        {
            GameObject obj = new GameObject(name == null ? "SoundClip" : name);
            obj.transform.parent = this.transform;
            AudioSource source = obj.AddComponent<AudioSource>();
            StartCoroutine(this.PlayClipEndDestroy(audioClip, obj));
            source.pitch = 1f;
            source.volume = volume;
            source.clip = audioClip;
            source.Play();
            this.LastAudioSource = source;
        }
    }

    /// <summary>
    /// 播放完音效删除物体
    /// </summary>
    /// <param name="audioclip"></param>
    /// <param name="soundobj"></param>
    /// <returns></returns>
    private IEnumerator PlayClipEndDestroy(AudioClip audioclip, GameObject soundobj)
    {
        if (soundobj == null || audioclip == null)
        {
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(audioclip.length * Time.timeScale);
            Destroy(soundobj);
        }
    }

    /// <summary>
    /// 音效播放完毕的处理，某一音效同时播放数减一
    /// </summary>
    /// <returns></returns>
    private IEnumerator PlayClipEnd(AudioClip audioclip, MusicType musicType)
    {
        if (audioclip != null)
        {
            yield return new WaitForSeconds(audioclip.length * Time.timeScale);
            AudioDictionary[musicType]--;
            if (AudioDictionary[musicType] <= 0)
            {
                AudioDictionary.Remove(musicType);
            }
        }
        yield break;
    }
    #endregion
}
