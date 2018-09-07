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
    public  Vector3 movement;//移动方向
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
        GetBall();
        if (isSelected)//如果被选中的话
        {
            h = ETCInput.GetAxis("Horizontal");
            v = ETCInput.GetAxis("Vertical");
            PlayerMove(h, v);
            Pass();
            Shoot();
            Tackle();
        }
        else
        {
            AI();
        }
        animator.SetFloat("power", power);
    }
    //移动控制
    void PlayerMove(float h,float v)
    {        
        movement =new Vector3(v, 0, -h); //获得移动方向
        if (movement != Vector3.zero)
        {
            //动画状态机
            animator.SetBool("Alert", false);
            animator.SetBool("Run", true);
            if (ETCInput.GetButton("Run"))//按下E键加速
            {
                MinusPower();                            
                //加速后速度
                speed = Mathf.Lerp(speed,upSpeed,Time.deltaTime*2);
            }
            //没按E键或松开
            else
            {              
                //恢复默认速度
                PowerToSpeed();
            }
        }
        else
        {
            RePower();
            //动画状态机
            animator.SetBool("Alert",true);
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
            if (GameManager.Instance.TeamAttackState())
            {
                playerState = PlayerState.ATTACK;
            }
            else if (GameManager.Instance.TeamDefenseState())
            {
                playerState = PlayerState.DEFENCE;
            }
        }
    }
    //射门
    void Shoot()
    {
//        if (!EventSystem.current.IsPointerOverGameObject())//UI防穿透
//        {
            if (ETCInput.GetButton("Shoot")&& playerState == PlayerState.HOLDING)
            {
            transform.LookAt(goal.transform);//玩家面向球门
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
                    Vector3 goalDir = (goal.transform.position - (transform.position + new Vector3(Random.Range(-8f, 8f), 0, 0))).normalized;//球门方向,往左右偏移量随机
                    GameManager.Instance.ballRgd.MovePosition(transform.position + transform.forward * 1.4f);//球脱离过近距离
                    Vector3 vector3 = (goalDir + transform.forward);
                    vector3 *= 14;
                    vector3.y = -vector3.z * 0.35f;
                    GameManager.Instance.ballRgd.velocity = vector3;
                    Invoke("Zero", 1f);
                }
            }
            if (GameManager.isShootUp && playerState == PlayerState.HOLDING)//玩家处于持球状态时点击射门
            {
                animator.SetTrigger("Shoot");
                AudioManager.Instance.audioSources[0].PlayOneShot(AudioManager.Instance.kick);
                Vector3 goalDir = (goal.transform.position - (transform.position)).normalized;//球门方向,往左右偏移量随机
                GameManager.Instance.ballRgd.MovePosition(transform.position + transform.forward * 1.4f);//球脱离过近距离
                Vector3 vector3 = (goalDir + transform.forward);
                //球速与蓄力时间有关
                if (timer < 3.3f)
                {
                    vector3 *= 3 + timer;

                }
                else if (timer < 6.6f)
                {
                    vector3 *= 3.5f + timer;
                }
                else
                {
                    vector3 *= 4 + timer;
                }
                vector3.y = -vector3.z * 0.35f;
                GameManager.Instance.ballRgd.velocity = vector3;
                Invoke("Zero", 1f);
                GameManager.isShootUp = false;
            }
//        }       
    }
    //传球  
    void Pass()
    {
        if (ETCInput.GetButton("Pass") && playerState == PlayerState.HOLDING)
        {
            transform.LookAt(passPlayer);
            //蓄力力度计算，最大为10
            if (timer < 10)
            {
                timer += Time.deltaTime * 22; 
            }
            else
            {
                timer = 10; 
                animator.SetTrigger("Pass");
                AudioManager.Instance.audioSources[0].PlayOneShot(AudioManager.Instance.kick);
                GameManager.Instance.ballRgd.MovePosition(transform.position + transform.forward * 2f);//球脱离过近距离
                GameManager.Instance.ballRgd.velocity = (passPlayer.position - transform.position).normalized * 25;//传球速度
                Invoke("Zero", 1f);
            }
        }
        if (playerState == PlayerState.HOLDING && GameManager.isPassUp)
        {
            float passSpeed;
            animator.SetTrigger("Pass");
            AudioManager.Instance.audioSources[0].PlayOneShot(AudioManager.Instance.kick);
            GameManager.Instance.ballRgd.MovePosition(transform.position + transform.forward * 2f);//球脱离过近距离
            //球速与蓄力时间有关
            if (timer < 3.3f)
            {
                passSpeed = 14 + timer;

            }
            else if (timer < 6.6f)
            {
                passSpeed = 16 + timer;
            }
            else
            {
                passSpeed = 18 + timer;
            }
            GameManager.Instance.ballRgd.velocity = (passPlayer.position - transform.position).normalized * passSpeed;//传球速度
            Invoke("Zero", 1f);
            GameManager.isPassUp = false;
        }
    }
    //铲球
    void Tackle()
    {
        if (playerState==PlayerState.DEFENCE&&GameManager.isTackleDown) 
        {
            animator.SetTrigger("Tackle");
            GameManager.isTackleDown = false;
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
    public void Zero()
    {
        timer = 0;
    }
    void AI()
    {
        if (transform.name=="CenterForward")
        {
            if(playerState == PlayerState.ATTACK)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                if (GameManager.Instance.TeamAllAttack()&& transform.name == GameManager.Instance.TeamDefensePlayer().name)
                {
                        rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                        transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                }
                else
                {
                    Vector3 targetPos = (GameManager.Instance.Player1.position + GameManager.Instance.Player2.position) / 2;
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
            else if (playerState == PlayerState.DEFENCE)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                Vector3 targetPos;
                if (GameManager.Instance.insBall.transform.position.z < -10)
                {
                    targetPos= new Vector3(0, 0, -5);
                }
                else
                {
                    targetPos = new Vector3(0, 0, 5);
                }
                if (transform.name==GameManager.Instance.TeamDefensePlayer().name)
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
        if ( transform.name == "RightForward")
        {
            if(playerState == PlayerState.ATTACK)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                if (GameManager.Instance.TeamAllAttack() && transform.name == GameManager.Instance.TeamDefensePlayer().name)
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                    transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                }
                else
                {
                    Vector3 targetPos = new Vector3(-10, 0, -15);
                    rgd.MovePosition(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 0.25f));
                    transform.LookAt(targetPos);
                    if (Vector3.Distance(transform.position, targetPos) <= 1f)
                    {
                        animator.SetBool("Alert", true);
                        animator.SetBool("Run", false);
                    }
                }               
            }
            else if (playerState == PlayerState.DEFENCE)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                Vector3 targetPos;
                if (GameManager.Instance.insBall.transform.position.z < -10)
                {
                    targetPos = new Vector3(-10, 0, -5);
                }
                else
                {
                    targetPos = new Vector3(-10, 0, 5);
                }
                if (transform.name == GameManager.Instance.TeamDefensePlayer().name)
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
            if(playerState == PlayerState.ATTACK)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                if (GameManager.Instance.TeamAllAttack() && transform.name == GameManager.Instance.TeamDefensePlayer().name)
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                    transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                }
                else
                {
                    Vector3 targetPos = new Vector3(10, 0, -15);
                    rgd.MovePosition(Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 0.25f));
                    transform.LookAt(targetPos);
                    if (Vector3.Distance(transform.position, targetPos) <= 1f)
                    {
                        animator.SetBool("Alert", true);
                        animator.SetBool("Run", false);
                    }
                }               
            }
            else if (playerState == PlayerState.DEFENCE)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                Vector3 targetPos;
                if (GameManager.Instance.insBall.transform.position.z < -10)
                {
                    targetPos = new Vector3(10, 0, -5);
                }
                else
                {
                    targetPos = new Vector3(10, 0, 5);
                }
                if (transform.name == GameManager.Instance.TeamDefensePlayer().name)
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
            if(playerState == PlayerState.ATTACK)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                if (GameManager.Instance.TeamAllAttack() && transform.name == GameManager.Instance.TeamDefensePlayer().name)
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                    transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                }
                else
                {
                    Vector3 targetPos = GameManager.Instance.Player1.position / 2;
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
            else if (playerState == PlayerState.DEFENCE)
            {
                Vector3 targetPos;
                if (GameManager.Instance.insBall.transform.position.z < -10)
                {
                    targetPos = new Vector3(-5, 0, 5);
                }
                else
                {
                    targetPos = new Vector3(-5, 0, 10);
                }
                if (transform.name == GameManager.Instance.TeamDefensePlayer().name)
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
        if ( transform.name == "LeftGuard")
        {
            if(playerState == PlayerState.ATTACK)
            {
                animator.SetBool("Alert", false);
                animator.SetBool("Run", true);
                if (GameManager.Instance.TeamAllAttack() && transform.name == GameManager.Instance.TeamDefensePlayer().name)
                {
                    rgd.MovePosition(Vector3.Lerp(transform.position, GameManager.Instance.insBall.transform.position, Time.deltaTime * 0.5f));
                    transform.LookAt(new Vector3(GameManager.Instance.insBall.transform.position.x, 0, GameManager.Instance.insBall.transform.position.z));
                }
                else
                {
                    Vector3 targetPos = GameManager.Instance.Player2.position / 2;
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
            else if (playerState == PlayerState.DEFENCE)
            {
                Vector3 targetPos;
                if (GameManager.Instance.insBall.transform.position.z < -10)
                {
                    targetPos = new Vector3(5, 0, 5);
                }
                else
                {
                    targetPos = new Vector3(5, 0, 10);
                }
                if (transform.name == GameManager.Instance.TeamDefensePlayer().name)
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
