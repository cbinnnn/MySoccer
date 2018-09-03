using UnityEngine;
using UnityEngine.SceneManagement;

public class ComfirmPanel : MonoBehaviour {
    public void ClickYes()
    {
        AudioManager.Instance.audioSources[0].Stop();
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
        Menu.isComfirmClone = false;
        Menu.isSettingClone = false;
    }
    public void ClickNo()
    {
        Destroy(this.gameObject);
        Menu.isComfirmClone = false;
    }
}
