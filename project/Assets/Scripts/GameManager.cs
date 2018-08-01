using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private GameObject team1;
    private GameObject map;
    public Text timeText;
    private float timeSpend;
    private int minute;
    private int second;
    public Text score1Text;
    public Text score2Text;
    private int score1;
    private int score2;
    public Rigidbody ballRgd;
    public GameObject insBall;
    public GameObject ball;
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
        team1 = Resources.Load("Team1") as GameObject;
        Instantiate(team1);
        map = Resources.Load("NewCity") as GameObject;
        Instantiate(map);
    }
    // Use this for initialization
    void Start () {
        BallReset();
        timeSpend = 0;
	}
    private void FixedUpdate()
    {
        Goal();
        MatchTime();
    }
    private void BallReset()
    {
        insBall=Instantiate(ball);//实例化足球
        ballRgd = insBall.GetComponent<Rigidbody>();//获取足球上的刚体组件
    }
    void Goal()
    {
        if (insBall.transform.position.z <= -21.5f)
        {
            score1++;
        }
        if (insBall.transform.position.z >= 21.5f)
        {
            score2++;
        }
        score1Text.text = score1.ToString();
        score2Text.text = score2.ToString();
    }
    void MatchTime()
    {
        timeSpend += Time.deltaTime;
        // GlobalSetting.timeSpent = timeSpend;

        minute = ((int)timeSpend) / 60;
        second = (int)timeSpend - minute * 60;

        timeText.text = string.Format("{0:D2}:{1:D2}", minute, second);
    }
}
