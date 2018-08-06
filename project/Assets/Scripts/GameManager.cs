using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public Transform Player1;
    public Transform Player2;
    public Transform Player3;
    public Transform Player4;
    public Transform Player5;
    public Transform GoalKeeper;
    public Transform Opponent1;
    public Transform Opponent2;
    public Transform Opponent3;
    public Transform Opponent4;
    public Transform Opponent5;
    public Transform OpponentGoalKeeper;
    public Animator player1Animator;
    public Animator player2Animator;
    public Animator player3Animator;
    public Animator player4Animator;
    public Animator player5Animator;
    public Animator goalKeeperAnimator;
    public Animator opponent1Animator;
    public Animator opponent2Animator;
    public Animator opponent3Animator;
    public Animator opponent4Animator;
    public Animator opponent5Animator;
    public Animator opponentGoalKeeperAnimator;
    public Transform position1;
    public Transform position2;
    public Transform position3;
    public Transform position4;
    public Transform position5;
    public Transform position6;
    public Transform position7;
    public Transform position8;
    public Transform position9;
    public Transform position10;
    public Transform position11;
    public Transform position12;
    public Canvas canvas;
    private GameObject team1;
    private GameObject map;
    public Text timeText;
    private float timeSpend;
    private int minute;
    private int second;
    public Rigidbody ballRgd;
    public GameObject insBall;
    public GameObject ball;
    public AudioSource audioSource;
    public AudioClip applause;
    private static GameManager _instance;
    public  static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
 //       team1 = Resources.Load("Team1") as GameObject;
 //       Instantiate(team1);
 //       map = Resources.Load("NewCity") as GameObject;
//        Instantiate(map);
    }
    // Use this for initialization
    void Start () {
        BallIns();
        timeSpend = 0;
        //取得球员身上的脚本
        player1Animator = Player1.GetComponent<Animator>();
        player2Animator = Player2.GetComponent<Animator>();
        player3Animator = Player3.GetComponent<Animator>();
        player4Animator = Player4.GetComponent<Animator>();
        player5Animator = Player5.GetComponent<Animator>();
        goalKeeperAnimator = GoalKeeper.GetComponent<Animator>();
        opponent1Animator = Opponent1.GetComponent<Animator>();
        opponent2Animator = Opponent2.GetComponent<Animator>();
        opponent3Animator = Opponent3.GetComponent<Animator>();
        opponent4Animator = Opponent4.GetComponent<Animator>();
        opponent5Animator = Opponent5.GetComponent<Animator>();
        opponentGoalKeeperAnimator = OpponentGoalKeeper.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        MatchTime();
    }
    private void BallIns()
    {
        insBall=Instantiate(ball);//实例化足球
        ballRgd = insBall.GetComponent<Rigidbody>();//获取足球上的刚体组件
    }
    void MatchTime()
    {
        timeSpend += Time.deltaTime;
        // GlobalSetting.timeSpent = timeSpend;

        minute = ((int)timeSpend) / 60;
        second = (int)timeSpend - minute * 60;

        timeText.text = string.Format("{0:D2}:{1:D2}", minute, second);
    }
    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);
        PositionReset();
    }
    private void PositionReset()
    {
        insBall.transform.position = new Vector3(0, 0, 0);
        Player1.position = position1.position;
        Player1.rotation = position1.rotation;
        Player2.position = position2.position;
        Player2.rotation = position2.rotation;
        Player3.position = position3.position;
        Player3.rotation = position3.rotation;
        Player4.position = position4.position;
        Player4.rotation = position4.rotation;
        Player5.position = position5.position;
        Player5.rotation = position5.rotation;
        GoalKeeper.position = position6.position;
        GoalKeeper.rotation = position6.rotation;
        Opponent1.position = position7.position;
        Opponent1.rotation = position7.rotation;
        Opponent2.position = position8.position;
        Opponent2.rotation = position8.rotation;
        Opponent3.position = position9.position;
        Opponent3.rotation = position9.rotation;
        Opponent4.position = position10.position;
        Opponent4.rotation = position10.rotation;
        Opponent5.position = position11.position;
        Opponent5.rotation = position11.rotation;
        OpponentGoalKeeper.position = position12.position;
        OpponentGoalKeeper.rotation = position12.rotation;
        player1Animator.SetTrigger("Alert");
        player2Animator.SetTrigger("Alert");
        player3Animator.SetTrigger("Alert");
        player4Animator.SetTrigger("Alert");
        player5Animator.SetTrigger("Alert");
        goalKeeperAnimator.SetTrigger("Alert");
        opponent1Animator.SetTrigger("Alert");
        opponent2Animator.SetTrigger("Alert");
        opponent3Animator.SetTrigger("Alert");
        opponent4Animator.SetTrigger("Alert");
        opponent5Animator.SetTrigger("Alert");
        opponentGoalKeeperAnimator.SetTrigger("Alert");
    }
}
