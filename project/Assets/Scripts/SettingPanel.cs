using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour {
    public Toggle openBgm;
    private static float lastvolume;
    private float oldVolume;
    public Text cameraSetting;
    public Slider bgmSlider;
    public Slider audioSlider;
    private void Awake()
    {
        oldVolume = AudioManager.Instance.audioSources[1].volume;
    }
    private void Update()
    {
        if (AudioManager.Instance.audioSources[1].volume > 0)
        {
            openBgm.isOn = true;
        }
        bgmSlider.value = AudioManager.Instance.audioSources[1].volume;
        audioSlider.value= AudioManager.Instance.audioSources[0].volume;
    }
    public void CloseSettingPanel()
    {
        Time.timeScale = 1;
        AudioManager.Instance.audioSources[1].volume = oldVolume;
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
            AudioManager.Instance.audioSources[1].volume = lastvolume;
        }
        else
        {
            lastvolume = AudioManager.Instance.audioSources[1].volume;
            AudioManager.Instance.audioSources[1].volume = 0;
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
