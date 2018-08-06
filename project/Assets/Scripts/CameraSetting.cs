using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSetting : MonoBehaviour {
    public Text now;
    private List<string> cameraFollow=new List<string> { "球员","足球"};
    private static int nowIndex=0;
    private void Update()
    {
        now.text = cameraFollow[nowIndex];
    }
    public void OnPre()
    {
        nowIndex = Mathf.Abs(nowIndex - 1) % cameraFollow.Count;
    }
    public void OnNext()
    {
        nowIndex = Mathf.Abs(nowIndex + 1) % cameraFollow.Count;
    }
}
