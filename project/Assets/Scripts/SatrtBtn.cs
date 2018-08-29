using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SatrtBtn : MonoBehaviour {
    public Text cameraSetting;
	public void ClickStartBtn()
    {
        MyCamera.nowCamera = CameraSetting.cameraFollow[CameraSetting.nowCameraIndex];
        SceneManager.LoadScene("Select");        
    }
}
