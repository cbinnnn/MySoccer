using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour {
    public enum OppoState//玩家状态枚举类
    {
        ATTACK,//进攻
        DEFENCE,//防守
        HOLDING//持球
    };
    public OppoState oppoState;
    // Use this for initialization
    void Start () {
		
	}	
	// Update is called once per frame
	void Update () {
        GetBall();
	}
    //带球
    void GetBall()
    {
        float distance = Vector3.Distance(transform.position, GameManager.Instance.insBall.transform.position);//球和玩家的距离
        if (distance < 1.3f)//距离过近
        {
            oppoState = OppoState.HOLDING;//玩家切换进攻状态
            GameManager.Instance.ballRgd.MovePosition(Vector3.Lerp(GameManager.Instance.ballRgd.position, transform.position + transform.forward * 1, Time.deltaTime * 15));//球始终处于玩家前方
        }
    }
}
