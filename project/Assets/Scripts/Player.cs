using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private enum PlayerState
    {
        ATTACK,
        DEFENCE
    }
    private PlayerState playerState;
    private static float h;
    private static float v;
    public GameObject goal;
    private static  Vector3 movement;
    private Animator animator;
    private Rigidbody rgd;
    public float speed=10;//移动速度
	// Use this for initialization
	void Start () {
        rgd = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");     
        PlayerMove(h, v);
        GetBall();
        Shoot();
    }
    //移动控制
    void PlayerMove(float h,float v)
    {    
        movement=new Vector3(v, 0, -h); 
        if (movement != Vector3.zero)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        movement = movement.normalized * speed * Time.deltaTime; //movement.normalized使向量单位化，结果等于每帧角色移动的距离
        rgd.MovePosition(transform.position + movement);//移动
        transform.LookAt(transform.position+movement);  //面向前进的方向   
    }
    //带球
    void GetBall()
    {
        float distance = Vector3.Distance(transform.position, GameManager.Instance.insBall.transform.position);
        if (distance<1.3f)
        {
            playerState = PlayerState.ATTACK;
                GameManager.Instance.ballRgd.MovePosition(transform.position + transform.forward * 1);
        }
        else
        {
            playerState = PlayerState.DEFENCE;
        }
    }
    //射门
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0)&&playerState==PlayerState.ATTACK)
        {
            animator.SetBool("Shoot", true);
            transform.LookAt(goal.transform);
            Vector3 goalDir = (goal.transform.position - transform.position).normalized;
       
            if (movement != Vector3.zero)
            {
                GameManager.Instance.ballRgd.MovePosition(transform.position + goalDir * 1.4f);
                GameManager.Instance.ballRgd.velocity = (goalDir+movement) * 40;//播放完毕，要执行的内容              
            }
            else
            {
                GameManager.Instance.ballRgd.MovePosition(transform.position + transform.forward*1.4f);
                GameManager.Instance.ballRgd.velocity = (goalDir+transform.forward) * 40;
            }           
        }
        else
        {
            animator.SetBool("Shoot", false);
        }
    }
}
