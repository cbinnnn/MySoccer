using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public bool isSelected;
    public Transform passPlayer;
    public enum PlayerState//玩家状态枚举类
    {
        ATTACK,//进攻
        DEFENCE,//防守
        HOLDING//持球
    };
    public PlayerState playerState;
    private static float h;
    private static float v;
    public GameObject goal;//球门
    private static  Vector3 movement;//移动方向
    private Animator animator;
    private Rigidbody rgd;
    public float speed=8;//初始移动速度
	// Use this for initialization
	void Start () {
        rgd = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (isSelected)//如果被选中的话
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            PlayerMove(h, v);
            GetBall();
            Pass();
            Shoot();
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            float distance = Vector3.Distance(transform.position, GameManager.Instance.insBall.transform.position);//球和玩家的距离
            if (distance < 1.3f)//判断是否被传球
            {
                playerState = PlayerState.HOLDING;
            }
        }
    }
    //移动控制
    void PlayerMove(float h,float v)
    {    
        movement=new Vector3(v, 0, -h); //获得移动方向
        if (movement != Vector3.zero)
        {          
            if (Input.GetKey(KeyCode.E))//按下E键加速
            {
                //动画状态机
                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
                //加速后速度
                speed = 12;
            }
            //没按E键或松开
            else
            {
                //动画状态机
                animator.SetBool("Run", false);
                animator.SetBool("Walk", true);
                //恢复默认速度
                speed = 8;
            }
        }
        else
        {
            //动画状态机
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
        
        movement = movement.normalized * speed * Time.deltaTime; //movement.normalized使向量单位化，结果等于每帧角色移动的距离
        rgd.MovePosition(transform.position + movement);//移动
        transform.LookAt(transform.position+movement);  //面向前进的方向   
    }
    //带球
    void GetBall()
    {
        float distance = Vector3.Distance(transform.position, GameManager.Instance.insBall.transform.position);//球和玩家的距离
        if (distance<1.3f)//距离过近
        {
            playerState = PlayerState.HOLDING;//玩家切换进攻状态
                GameManager.Instance.ballRgd.MovePosition(transform.position + transform.forward * 1);//球始终处于玩家前方
        }
        else
        {
            playerState = PlayerState.DEFENCE;//玩家处于进攻状态
        }
    }
    //射门
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0)&&playerState==PlayerState.HOLDING)//玩家处于持球状态时点击射门
        {
            animator.SetBool("Shoot", true);
            transform.LookAt(goal.transform);//玩家面向球门
            Vector3 goalDir = (goal.transform.position - transform.position+new Vector3(Random.Range(-3f,3f),0,0)).normalized;//球门方向            
                GameManager.Instance.ballRgd.MovePosition(transform.position + transform.forward*1.4f);//球脱离过近距离
                Vector3 vector3 = (goalDir+transform.forward)* 12;
                vector3.y = 7f;
                GameManager.Instance.ballRgd.velocity = vector3;       
        }
        else
        {
            animator.SetBool("Shoot", false);
        }
    }
    //传球
    void Pass()
    {
        if (playerState == PlayerState.HOLDING && Input.GetKeyDown(KeyCode.S))//持球状态下按下S
        {
            transform.LookAt(passPlayer);//面向接球者
            animator.SetBool("Pass", true);//动画状态机
            GameManager.Instance.ballRgd.MovePosition(transform.position + transform.forward * 2f);//球脱离过近距离
            GameManager.Instance.ballRgd.velocity = (passPlayer.position - transform.position).normalized * 25;//传球速度
        }
        else
        {
            //动画状态机
            animator.SetBool("Pass", false);
        }
    }
}
