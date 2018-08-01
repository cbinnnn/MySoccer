using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingBtn : MonoBehaviour {
    private RectTransform canvas;
    private void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }
    public void ClickSettingBtn()
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
    }
}
