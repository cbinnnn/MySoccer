using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Globe
{
    public static string nextSceneName;
}
public class AsyncLoadScene : MonoBehaviour
{
    public Slider loadingSlider;
    public Text loadingText;
    public Image bg;
    public Sprite[] sprites;
    private int randomIndex;
    private float loadingSpeed = 1;
    private float targetValue;//目标进度
    private AsyncOperation operation;//异步对象
	// Use this for initialization
	void Start () {
        bg.sprite = sprites[Random.Range(0, 8)];
        loadingSlider.value = 0;
        if (SceneManager.GetActiveScene().name == "Loading")
        {
            //启动协程
            StartCoroutine(AsyncLoading());
        }
	}
    IEnumerator AsyncLoading()
    {
        operation = SceneManager.LoadSceneAsync(Globe.nextSceneName);
        //阻止当加载完成自动切换
        operation.allowSceneActivation = false;
        yield return operation;
    }
	
	// Update is called once per frame
	void Update ()
    {
        targetValue = operation.progress;
        if (operation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9
            targetValue = 1;
        }
        if (targetValue != loadingSlider.value)
        {
            //插值运算
            loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);
            if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)
            {
                loadingSlider.value = targetValue;
            }
        }
        loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";
        if ((int)(loadingSlider.value * 100) == 100)
        {
            //允许异步加载完毕后自动切换场景
            operation.allowSceneActivation = true;
        }
	}
}
