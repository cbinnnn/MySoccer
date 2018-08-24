using UnityEngine;
using UnityEngine.SceneManagement;

public class ComfirmPanel : MonoBehaviour {
    public void ClickYes()
    {
        AudioManager.Instance.audioSources[0].Stop();
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
        BackBtn.isClone = false;
        SettingBtn.isClone = false;
    }
    public void ClickNo()
    {
        Destroy(this.gameObject);
        BackBtn.isClone = false;
    }
}
