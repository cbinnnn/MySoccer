using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public RectTransform SettingPanel1;
    public Text cameraSetting;
    private RectTransform canvas;
    public static bool isSettingClone = false;
    public static bool isComfirmClone = false;
    private void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }
    public void ClickStartBtn()
    {
        MyCamera.nowCamera = CameraSetting.cameraFollow[CameraSetting.nowCameraIndex];
        SceneManager.LoadScene("Select");
    }
    public void ClickSettingBtn()
    {
        if (!isSettingClone)
        {
            if (SceneManager.GetActiveScene().name == "Menu")
            {
                GameObject settingPanel = Resources.Load("SettingPanel") as GameObject;
                Instantiate(settingPanel, canvas);
            }
            if (SceneManager.GetActiveScene().name == "Game")
            {
                Time.timeScale = 0;
                GameObject settingPanel = Resources.Load("SettingPanel1") as GameObject;
                Instantiate(settingPanel, canvas);
            }
            isSettingClone = true;
        }
    }
    public void ClickBackBtn()
    {
        if (!isComfirmClone)
        {
            GameObject comfirmPanel = Resources.Load("ComfirmPanel") as GameObject;
            Instantiate(comfirmPanel, SettingPanel1);
            isComfirmClone = true;
        }
    }
    public void ClickExitBtn()
    {
        Application.Quit();
    }
}
