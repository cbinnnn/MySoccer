﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComfirmPanel : MonoBehaviour {

	public void ClickYes()
    {
        AudioManager.Instance.audioSources[0].Stop();
        SceneManager.LoadScene("Menu");      
    }
    public void ClickNo()
    {
        Destroy(this.gameObject);
    }
}
