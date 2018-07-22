using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBtn : MonoBehaviour {
    public RectTransform SettingPanel1;
    public void ClickBackBtn()
    {
        GameObject comfirmPanel = Resources.Load("ComfirmPanel") as GameObject;
        Instantiate(comfirmPanel, SettingPanel1);
    }
}
