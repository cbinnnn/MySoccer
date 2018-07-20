using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BtnManager : MonoBehaviour {
    public Image settingPanel;
    public Image comfirmPanel;
    private static BtnManager _instance;
    public static BtnManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    public void StartBtn()
    {
        SceneManager.LoadScene("Game");
    }
    public void SettingBtn()
    {
        settingPanel.gameObject.SetActive(true);
    }
    public void CloseSettingBtn()
    {
        settingPanel.gameObject.SetActive(false);
        if(comfirmPanel!=null)
        comfirmPanel.gameObject.SetActive(false);   
    }
    public void MenuBtn()
    {
        settingPanel.gameObject.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
    public void ComfirmPanel()
    {
        comfirmPanel.gameObject.SetActive(true);
    }
    public void NoComfirmPanel()
    {
        comfirmPanel.gameObject.SetActive(false);
    }
}
