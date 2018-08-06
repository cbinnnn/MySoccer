using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SatrtBtn : MonoBehaviour {
    public Text cameraSetting;
	public void ClickStartBtn()
    {
        MyCamera.nowCamera = CameraSetting.cameraFollow[CameraSetting.nowIndex];
        AudioManager.Instance.audioSources[0].Play();
        DontDestroyOnLoad(AudioManager.Instance);
        SceneManager.LoadScene("Game");
        
    }
}
