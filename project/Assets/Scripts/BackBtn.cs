using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBtn : MonoBehaviour {
    public RectTransform SettingPanel1;
    public static  bool isClone=false;
    public void ClickBackBtn()
    {
        if (!isClone)
        {
            GameObject comfirmPanel = Resources.Load("ComfirmPanel") as GameObject;
            Instantiate(comfirmPanel, SettingPanel1);
            isClone = true;
        }        
    }
}
