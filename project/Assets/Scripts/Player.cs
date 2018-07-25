using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Animation anim;
    private enum PlayerState
    {
        IDLE,//默认状态
        WALK//行走
    };
    private static PlayerState playerState;
    private Rigidbody rgd;
    public float speed=10;//移动速度
	// Use this for initialization
	void Start () {
        rgd = GetComponent<Rigidbody>();
        playerState = PlayerState.IDLE;//初始化为默认状态
        anim = GetComponent<Animation>();
	}
    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");     
        PlayerMove(h, v);
        PlayerAnim();
    }
    //移动控制
    void PlayerMove(float h,float v)
    {
        
        Vector3 movement=new Vector3(v, 0, -h); 
        if (movement != Vector3.zero)
        {
            playerState = PlayerState.WALK;//设置为行走状态
        }
        else
        {
            playerState = PlayerState.IDLE;
        }
        movement = movement.normalized * speed * Time.deltaTime; //movement.normalized使向量单位化，结果等于每帧角色移动的距离
        rgd.MovePosition(transform.position + movement);//移动
        transform.LookAt(transform.position+movement);  //面向前进的方向   
    }
    //动画控制
    void PlayerAnim()
    {
        switch (playerState)
        {
            case PlayerState.IDLE:
                anim.Play("alert");
                break;
            case PlayerState.WALK:
                anim.Play("walk");
                break;
        }
    }
}
