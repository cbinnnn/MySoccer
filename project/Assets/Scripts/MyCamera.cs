using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour {
    public static string nowCamera;//当前相机设置
    public Transform player;//球员位置
    public enum Follow
    {
        足球,//跟随球
        球员//跟随球员
    };
    private float smoothing = 5;//镜头灵敏度
    private Vector3 offset;//镜头与跟随物的偏移量
    private Vector3 endPos;//镜头要到达的位置
	void Start () {
            offset = transform.position - Vector3.zero;//默认偏移量
            nowCamera = "球员";
    }
	
	void Update () {
        if (nowCamera==Follow.足球.ToString())
        {
            BallCamera();
        }
        else if (nowCamera == Follow.球员.ToString())
        {
            PlayerCamera();
        } 
        //镜头平滑移动至目标位置
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime*smoothing);
	}
    //跟随足球函数
    private void BallCamera()
    {
        if ((GameManager.Instance.insBall.transform.position + offset).x < -25)
        {
            if ((GameManager.Instance.insBall.transform.position + offset).z > -8.5f)
            {
                if ((GameManager.Instance.insBall.transform.position + offset).z > 7f)
                    endPos = new Vector3(-25, (GameManager.Instance.insBall.transform.position + offset).y, 7);
                else
                    endPos = new Vector3(-25, (GameManager.Instance.insBall.transform.position + offset).y, (GameManager.Instance.insBall.transform.position + offset).z);
            }
            else
                endPos = new Vector3(-25, (GameManager.Instance.insBall.transform.position + offset).y, -8.5f);

        }
        else
        {
            if ((GameManager.Instance.insBall.transform.position + offset).z < -8.5f)
                endPos = new Vector3((GameManager.Instance.insBall.transform.position + offset).x, (GameManager.Instance.insBall.transform.position + offset).y, -8.5f);
            else
            {
                if ((GameManager.Instance.insBall.transform.position + offset).z > 7f)
                    endPos = new Vector3((GameManager.Instance.insBall.transform.position + offset).x, (GameManager.Instance.insBall.transform.position + offset).y, 7f);
                else
                    endPos = (GameManager.Instance.insBall.transform.position + offset);
            }
        }
    }
    //跟随球员函数
    private void PlayerCamera()
    {
        if ((player.position + offset).x < -25)
        {
            if ((player.position + offset).z > -8.5f)
            {
                if ((player.position + offset).z > 7f)
                    endPos = new Vector3(-25, (player.position + offset).y, 7);
                else
                    endPos = new Vector3(-25, (player.position + offset).y, (player.position + offset).z);
            }
            else
                endPos = new Vector3(-25, (player.position + offset).y, -8.5f);

        }
        else
        {
            if ((player.position + offset).z < -8.5f)
                endPos = new Vector3((player.position + offset).x, (player.position + offset).y, -8.5f);
            else
            {
                if ((player.position + offset).z > 7f)
                    endPos = new Vector3((player.position + offset).x, (player.position + offset).y, 7f);
                else
                    endPos = (player.position + offset);
            }
        }
    }
}
