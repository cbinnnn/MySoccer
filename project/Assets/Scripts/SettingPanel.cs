using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour {

    public void CloseSettingPanel()
    {
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }
	
}
