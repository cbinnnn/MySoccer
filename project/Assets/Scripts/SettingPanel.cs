using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour {
    public Toggle openBgm;
    public Toggle openAudio;
    private static float lastBgmVolume;
    private static float lastAudioVolume;
    private float oldAudioVolume;
    private float oldBgmVolume;
    public Text cameraSetting;
    public Slider bgmSlider;
    public Slider audioSlider;
    private void Awake()
    {
        oldBgmVolume = AudioManager.Instance.audioSources[1].volume;
        oldAudioVolume = AudioManager.Instance.audioSources[0].volume;
    }
    private void Update()
    {
        if (AudioManager.Instance.audioSources[1].volume > 0)
        {
            openBgm.isOn = true;
        }
        if (AudioManager.Instance.audioSources[0].volume > 0)
        {
            openAudio.isOn = true;
        }
        bgmSlider.value = AudioManager.Instance.audioSources[1].volume;
        audioSlider.value= AudioManager.Instance.audioSources[0].volume;
    }
    public void CloseSettingPanel()
    {
        Time.timeScale = 1;
        AudioManager.Instance.audioSources[1].volume = oldBgmVolume;
        AudioManager.Instance.audioSources[0].volume = oldAudioVolume;
        Destroy(this.gameObject);
    }
	public void OnSave()
    {
        MyCamera.nowCamera = CameraSetting.cameraFollow[CameraSetting.nowIndex];
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }
    public void Bgm()
    {
        if (openBgm.isOn)
        {
            AudioManager.Instance.audioSources[1].volume = lastBgmVolume;
        }
        else
        {
            lastBgmVolume = AudioManager.Instance.audioSources[1].volume;
            AudioManager.Instance.audioSources[1].volume = 0;
        }
        
    }
    public void Audio()
    {
        if (openAudio.isOn)
        {
            AudioManager.Instance.audioSources[0].volume = lastAudioVolume;
        }
        else
        {
            lastAudioVolume = AudioManager.Instance.audioSources[0].volume;
            AudioManager.Instance.audioSources[0].volume = 0;
        }

    }
    public void ChangeBgmVolume()
    {
        AudioManager.Instance.audioSources[1].volume = bgmSlider.value;
    }
    public void ChangeAudioVolume()
    {
        AudioManager.Instance.audioSources[0].volume = audioSlider.value;
    }
}
