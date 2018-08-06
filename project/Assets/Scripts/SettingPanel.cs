using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour {
    public Text cameraSetting;
    public GameObject mainCamera;
    public void CloseSettingPanel()
    {
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }
	public void OnSave()
    {
        MyCamera.nowCamera = cameraSetting.text;
        CloseSettingPanel();
    }
}
