using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingBtn : MonoBehaviour {
    private RectTransform canvas;
    public static bool isClone = false;
    private void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<RectTransform>();
    }
    public void ClickSettingBtn()
    {
        if (!isClone)
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
            isClone = true;
        }           
    }
}
