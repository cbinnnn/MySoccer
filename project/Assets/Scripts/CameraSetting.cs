using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSetting : MonoBehaviour {
    public Text nowCamera;
//    public Text nowMatchTime;
    public static  List<string> cameraFollow=new List<string> { "球员","足球"};
//    public static List<string> matchTime = new List<string> { "4", "5", "6","7","8"};
    public static int nowCameraIndex=0;
//    public static int nowMatchTimeIndex = 0;
    private void Update()
    {
        nowCamera.text = cameraFollow[nowCameraIndex];
//        nowMatchTime.text = matchTime[nowMatchTimeIndex];
    }
    public void OnCameraPre()
    {
        nowCameraIndex = Mathf.Abs(nowCameraIndex - 1) % cameraFollow.Count;
    }
    public void OnCameraNext()
    {
        nowCameraIndex = Mathf.Abs(nowCameraIndex + 1) % cameraFollow.Count;       
    }
    /*
    public void OnMatchTimePre()
    {
        if(nowMatchTimeIndex - 1 < 0)
        {
            nowMatchTimeIndex = matchTime.Count-1;
        }
        else
        {
            nowMatchTimeIndex = nowMatchTimeIndex - 1;
        }
        
    }
    public void OnMatchTimeNext()
    {
        nowMatchTimeIndex = Mathf.Abs(nowMatchTimeIndex + 1) % matchTime.Count;
    }
    */
}
