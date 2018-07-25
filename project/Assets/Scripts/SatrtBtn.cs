using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SatrtBtn : MonoBehaviour {

	public void ClickStartBtn()
    {
        SceneManager.LoadScene("Game");
    }
}
