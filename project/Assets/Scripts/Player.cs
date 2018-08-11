using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float power=100;//默认体力值
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
    public Animator animator;
    private Rigidbody rgd;
    private float speed;//初始移动速度
    public float upSpeed;//加速后速度
    public float timer;
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
        animator.SetFloat("power", power);
    }
    //移动控制
    void PlayerMove(float h,float v)
    {
        
        movement =new Vector3(v, 0, -h); //获得移动方向
        if (movement != Vector3.zero)
        {          
            if (Input.GetKey(KeyCode.E))//按下E键加速
            {
                MinusPower();
                //动画状态机
                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
                //加速后速度
                speed = Mathf.Lerp(speed,upSpeed,Time.deltaTime*2);
            }
            //没按E键或松开
            else
            {
                //动画状态机
                animator.SetBool("Run", false);
                animator.SetBool("Walk", true);
                //恢复默认速度
                PowerToSpeed();
            }
        }
        else
        {
            RePower();
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
                GameManager.Instance.ballRgd.MovePosition(Vector3.Lerp(GameManager.Instance.ballRgd.position, transform.position + transform.forward * 1,Time.deltaTime*15));//球始终处于玩家前方
        }
        else
        {
            playerState = PlayerState.DEFENCE;//玩家处于进攻状态
        }
    }
    //射门
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && playerState == PlayerState.HOLDING)
        {
            transform.LookAt(goal.transform);//玩家面向球门
            timer = 0;
        }
        if (Input.GetMouseButton(0) && playerState == PlayerState.HOLDING)
        {
            //蓄力力度计算，最大为10
            if (timer < 10)
            {
                    timer += Time.deltaTime * 22;
            }
            else
            {
                timer = 10;
                animator.SetTrigger("Shoot");
                AudioManager.Instance.audioSources[0].PlayOneShot(AudioManager.Instance.kick);
                Vector3 goalDir = (goal.transform.position - (transform.position + new Vector3(Random.Range(-15f, 15f), 0, 0))).normalized;//球门方向,往左右偏移量随机
                GameManager.Instance.ballRgd.MovePosition(transform.position + transform.forward * 1.4f);//球脱离过近距离
                Vector3 vector3 = (goalDir + transform.forward);
                vector3.z = -24;
                vector3.y = -vector3.z * 0.35f;
                GameManager.Instance.ballRgd.velocity = vector3;
                Invoke("Zero", 1.5f);
            }
            Debug.Log(timer);
        }
        if (Input.GetMouseButtonUp(0)&&playerState==PlayerState.HOLDING)//玩家处于持球状态时点击射门
        {
            animator.SetTrigger("Shoot");
            AudioManager.Instance.audioSources[0].PlayOneShot(AudioManager.Instance.kick);           
            Vector3 goalDir = (goal.transform.position - (transform.position+new Vector3(Random.Range(-15f,15f),0,0))).normalized;//球门方向,往左右偏移量随机
                GameManager.Instance.ballRgd.MovePosition(transform.position + transform.forward*1.4f);//球脱离过近距离
                Vector3 vector3 = (goalDir+transform.forward);
            //球速与蓄力时间有关
            if (timer/10 <0.33f)
            {
                vector3.z = -8-timer;
                
            }
            else if (timer/10 < 0.66f)
            {
                vector3.z = -12  -timer;
            }
            else
            {
                vector3.z = -14 - timer;
            }
                vector3.y = -vector3.z*0.35f;
                GameManager.Instance.ballRgd.velocity = vector3;
            Invoke("Zero", 1.5f);
        }
    }
    //传球
    void Pass()
    {
        if (playerState == PlayerState.HOLDING && Input.GetKeyDown(KeyCode.S))//持球状态下按下S
        {
            transform.LookAt(passPlayer);//面向接球者
            animator.SetBool("Pass", true);//动画状态机
            AudioManager.Instance.audioSources[0].PlayOneShot(AudioManager.Instance.kick);
            GameManager.Instance.ballRgd.MovePosition(transform.position + transform.forward * 2f);//球脱离过近距离
            GameManager.Instance.ballRgd.velocity = (passPlayer.position - transform.position).normalized * 25;//传球速度
        }
        else
        {
            //动画状态机
            animator.SetBool("Pass", false);
        }
    }
    void PowerToSpeed()//体力与速度的对应
    {
        //有体力的话默认速度
        if (power > 66)
        {
            speed = 6;
        }
        else if (power > 33)
        {
            speed = 4.5f;
        }
        else
        {
            if (power == 0)//没有体力就走不动
                speed = 0;
            else
                speed = 3f;
        }        
    }
    void MinusPower()//扣除体力
    {
        if (power > 0)
            power -= Time.deltaTime * 8;
        else
            power = 0;
    }
    void RePower()//恢复体力
    {
        if(power<100)
        {
            power += Time.deltaTime*2;
        }
        else
        {
            power = 100;
        }
    }
    void Zero()
    {
        timer = 0;
    }
}
