using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

    public static bool isClone;
    public GameObject obj;
    private GameObject cloneObj=null;
    void Awake()
    {
        if (!isClone)
        {
            cloneObj = Instantiate(obj);
            isClone = true;
        }
    }
}
