using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour {
    public Transform goal;
    public float power = 100;//默认体力值
    private Animator animator;
    private Rigidbody rgd;
    public enum OppoState//玩家状态枚举类
    {
        ATTACK,//进攻
        DEFENCE,//防守
        HOLDING//持球
    };
    public OppoState oppoState;
    void Start () {
        animator = GetComponent<Animator>();
        rgd = GetComponent<Rigidbody>();
	}	
	// Update is called once per frame
	void FixedUpdate () {
        GetBall();
        AI();
        Shoot();
        animator.SetFloat("power", power);
    }
    //带球
    void GetBall()
    {
        float distance = Vector3.Distance(transform.position, GameManager.Instance.insBall.transform.position);//球和玩家的距离
        if (distance < 1.3f)//距离过近
        {
            oppoState = OppoState.HOLDING;//对手切换持球状态
            GameManager.Instance.ballRgd.MovePosition(Vector3.Lerp(GameManager.Instance.ballRgd.position, transform.position + transform.forward * 1, Time.deltaTime * 15));//球始终处于玩家前方
            animator.SetBool("Alert",false);
            animator.SetBool("Run", true);
            rgd.MovePosition(Vector3.Lerp(transform.position, goal.position, Time.deltaTime * 0.2f));
            transform.LookAt(goal);
        }
        else
        {
            if (GameManager.Instance.TeamAttackState())
            {
                oppoState = OppoState.DEFENCE;
            }
            else if (GameManager.Instance.TeamDefenseState())
            {
                oppoState=OppoState.ATTACK;
            }
        }
    }
    void Shoot()
    {
        if (oppoState == OppoState.HOLDING && transform.position.z >= 13)
        {
            animator.SetTrigger("Shoot");
            Vector3 goalDir = (goal.transform.position - (transform.position + new Vector3(Random.Range(-8f, 8f), 0, 0))).normalized;//球门方向,往左右偏移量随机
            goalDir *= 14;
            GameManager.Instance.ballRgd.velocity = goalDir;
        }
    }
    void AI()
    {
        if (transform.name == "CenterForward")
        {
            if (oppoState == OppoState.ATTACK)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                if (GameManager.Instance.OppoAllAttack() && transform.name == GameManager.Instance.OppoDefensePlayer().name)
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                    transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                }
                else
                {
                    Vector3 targetPos = (GameManager.Instance.Opponent1.position + GameManager.Instance.Opponent5.position) / 2;
                    targetPos.y = 0;
                    rgd.MovePosition(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 0.25f));
                    transform.LookAt(targetPos);
                    if (Vector3.Distance(transform.position, targetPos) <= 1f)
                    {
                        animator.SetBool("Alert", true);
                        animator.SetBool("Run", false);
                    }
                }
            }
            else if (oppoState == OppoState.DEFENCE)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                Vector3 targetPos;
                if (GameManager.Instance.insBall.transform.position.z >10)
                {
                    targetPos = new Vector3(0, 0, 5);
                }
                else
                {
                    targetPos = new Vector3(0, 0, -5);
                }
                if (transform.name == GameManager.Instance.OppoDefensePlayer().name)
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                    transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                    if (Vector3.Distance(transform.position, GameManager.Instance.insBall.transform.position) <= 1.5f)
                    {
                        animator.SetTrigger("Tackle");
                    }
                }
                else
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 0.3f));
                    transform.LookAt(targetPos);
                    if (Vector3.Distance(transform.position, targetPos) <= 1f)
                    {
                        animator.SetBool("Alert", true);
                        animator.SetBool("Run", false);
                    }
                }
            }
        }
        if (transform.name == "RightForward")
        {
            if (oppoState == OppoState.ATTACK)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                if (GameManager.Instance.OppoAllAttack() && transform.name == GameManager.Instance.OppoDefensePlayer().name)
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                    transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                }
                else
                {
                    Vector3 targetPos = new Vector3(10, 0, 15);
                    rgd.MovePosition(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 0.25f));
                    transform.LookAt(targetPos);
                    if (Vector3.Distance(transform.position, targetPos) <= 1f)
                    {
                        animator.SetBool("Alert", true);
                        animator.SetBool("Run", false);
                    }
                }
            }
            else if (oppoState == OppoState.DEFENCE)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                Vector3 targetPos;
                if (GameManager.Instance.insBall.transform.position.z >10)
                {
                    targetPos = new Vector3(10, 0, 5);
                }
                else
                {
                    targetPos = new Vector3(10, 0, -5);
                }
                if (transform.name == GameManager.Instance.OppoDefensePlayer().name)
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                    transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                    if (Vector3.Distance(transform.position, GameManager.Instance.insBall.transform.position) <= 1.5f)
                    {
                        animator.SetTrigger("Tackle");
                    }
                }
                else
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 0.3f));
                    transform.LookAt(targetPos);
                    if (Vector3.Distance(transform.position, targetPos) <= 1f)
                    {
                        animator.SetBool("Alert", true);
                        animator.SetBool("Run", false);
                    }
                }
            }
        }
        if (transform.name == "LeftForward")
        {
            if (oppoState == OppoState.ATTACK)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                if (GameManager.Instance.OppoAllAttack() && transform.name == GameManager.Instance.OppoDefensePlayer().name)
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                    transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                }
                else
                {
                    Vector3 targetPos = new Vector3(-10, 0, 15);
                    rgd.MovePosition(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 0.25f));
                    transform.LookAt(targetPos);
                    if (Vector3.Distance(transform.position, targetPos) <= 1f)
                    {
                        animator.SetBool("Alert", true);
                        animator.SetBool("Run", false);
                    }
                }
            }
            else if (oppoState == OppoState.DEFENCE)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                Vector3 targetPos;
                if (GameManager.Instance.insBall.transform.position.z >10)
                {
                    targetPos = new Vector3(-10, 0, 5);
                }
                else
                {
                    targetPos = new Vector3(-10, 0, -5);
                }
                if (transform.name == GameManager.Instance.OppoDefensePlayer().name)
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                    transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                    if (Vector3.Distance(transform.position, GameManager.Instance.insBall.transform.position) <= 1.5f)
                    {
                        animator.SetTrigger("Tackle");
                    }
                }
                else
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 0.3f));
                    transform.LookAt(targetPos);
                    if (Vector3.Distance(transform.position, targetPos) <= 1f)
                    {
                        animator.SetBool("Alert", true);
                        animator.SetBool("Run", false);
                    }
                }
            }
        }
        if (transform.name == "RightGuard")
        {
            if (oppoState == OppoState.ATTACK)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                if (GameManager.Instance.OppoAllAttack() && transform.name == GameManager.Instance.OppoDefensePlayer().name)
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                    transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                }
                else
                {
                    Vector3 targetPos = GameManager.Instance.Opponent5.position / 2;
                    targetPos.y = 0;
                    rgd.MovePosition(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 0.25f));
                    transform.LookAt(targetPos);
                    if (Vector3.Distance(transform.position, targetPos) <= 1f)
                    {
                        animator.SetBool("Alert", true);
                        animator.SetBool("Run", false);
                    }
                }
            }
            else if (oppoState == OppoState.DEFENCE)
            {
                Vector3 targetPos;
                if (GameManager.Instance.insBall.transform.position.z >10)
                {
                    targetPos = new Vector3(5, 0, -5);
                }
                else
                {
                    targetPos = new Vector3(5, 0, -10);
                }
                if (transform.name == GameManager.Instance.OppoDefensePlayer().name)
                {
                    animator.SetBool("Alert", false);
                    animator.SetBool("Run", true);
                    rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                    transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                    if (Vector3.Distance(transform.position, GameManager.Instance.insBall.transform.position) <= 1.5f)
                    {
                        animator.SetTrigger("Tackle");
                    }
                }
                else
                {
                        animator.SetBool("Alert", false);
                        animator.SetBool("Run", true);
                        rgd.MovePosition(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 0.4f));
                        transform.LookAt(targetPos);
                        if (Vector3.Distance(transform.position, targetPos) <= 1f)
                        {
                            animator.SetBool("Alert", true);
                            animator.SetBool("Run", false);
                        }
                }
            }
        }
        if (transform.name == "LeftGuard")
        {
            if (oppoState == OppoState.ATTACK)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                if (GameManager.Instance.OppoAllAttack() && transform.name == GameManager.Instance.OppoDefensePlayer().name)
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                    transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                }
                else
                {
                    Vector3 targetPos = GameManager.Instance.Opponent1.position / 2;
                    targetPos.y = 0;
                    rgd.MovePosition(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 0.25f));
                    transform.LookAt(targetPos);
                    if (Vector3.Distance(transform.position, targetPos) <= 1f)
                    {
                        animator.SetBool("Alert", true);
                        animator.SetBool("Run", false);
                    }
                }
            }
            else if (oppoState == OppoState.DEFENCE)
            {
                Vector3 targetPos;
                if (GameManager.Instance.insBall.transform.position.z >10)
                {
                    targetPos = new Vector3(-5, 0, -5);
                }
                else
                {
                    targetPos = new Vector3(-5, 0, -10);
                }
                if (transform.name == GameManager.Instance.OppoDefensePlayer().name)
                {
                    animator.SetBool("Alert", false);
                    animator.SetBool("Run", true);
                    rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                    transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                    if (Vector3.Distance(transform.position, GameManager.Instance.insBall.transform.position) <= 1.5f)
                    {
                        animator.SetTrigger("Tackle");
                    }
                }
                else
                {
                        animator.SetBool("Alert", false);
                        animator.SetBool("Run", true);
                        rgd.MovePosition(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 0.4f));
                        transform.LookAt(targetPos);
                        if (Vector3.Distance(transform.position, targetPos) <= 1f)
                        {
                            animator.SetBool("Alert", true);
                            animator.SetBool("Run", false);
                        }
                }
            }
        }
    }
}
