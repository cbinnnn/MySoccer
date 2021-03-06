﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public Canvas canvas;
    public RectTransform match;
    public ETCButton shoot;
    public ETCButton run;
    public ETCButton pass;
    public ETCButton tackle;
    public ETCButton change;
    public static bool isShootUp;
    public static bool isPassUp;
    public static bool isTackleDown;
    public Transform Player1;
    public Transform Player2;
    public Transform Player3;
    public Transform Player4;
    public Transform Player5;
    public Player player1Script;
    public Player player2Script;
    public Player player3Script;
    public Player player4Script;
    public Player player5Script;
    public Opponent oppo1Script;
    public Opponent oppo2Script;
    public Opponent oppo3Script;
    public Opponent oppo4Script;
    public Opponent oppo5Script;
    public GameObject[] players;
    public GameObject[] oppoPlayers;
    public Material[] team1Materials;
    public Material[] team2Materials;
    public Material[] team3Materials;
    private SkinnedMeshRenderer[] skms;
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
    private GameObject team1;
    private GameObject map;
    public Text timeText;
    public float matchTime=60;
    private float timeRest;
    public Rigidbody ballRgd;
    public GameObject insBall;
    public GameObject ball;
    private static GameManager _instance;
    public static GameManager Instance
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
    void Start()
    {
        AudioManager.Instance.audioSources[0].PlayOneShot(AudioManager.Instance.start);
        timeRest = matchTime;
        match.DOLocalMoveY(-20, 1).SetEase(Ease.OutBounce);
        match.DOLocalMoveY(160,1).SetDelay(1);
        BallIns();
        //取得球员身上的脚本
        player1Script = Player1.GetComponent<Player>();
        player2Script = Player2.GetComponent<Player>();
        player3Script = Player3.GetComponent<Player>();
        player4Script = Player4.GetComponent<Player>();
        player5Script = Player5.GetComponent<Player>();
        oppo1Script = Opponent1.GetComponent<Opponent>();
        oppo2Script = Opponent2.GetComponent<Opponent>();
        oppo3Script = Opponent3.GetComponent<Opponent>();
        oppo4Script = Opponent4.GetComponent<Opponent>();
        oppo5Script = Opponent5.GetComponent<Opponent>();
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
        skms = new SkinnedMeshRenderer[players.Length];
        if (PlayerPrefs.GetInt("Team") == 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                skms[i] = players[i].GetComponent<SkinnedMeshRenderer>();
                skms[i].materials = team1Materials;
            }
        }
        if (PlayerPrefs.GetInt("Team") == 1)
        {
            for (int i = 0; i < players.Length; i++)
            {
                skms[i] = players[i].GetComponent<SkinnedMeshRenderer>();
                skms[i].materials = team2Materials;
            }
        }
        if (PlayerPrefs.GetInt("Team") == 2)
        {
            for (int i = 0; i < players.Length; i++)
            {
                skms[i] = players[i].GetComponent<SkinnedMeshRenderer>();
                skms[i].materials = team3Materials;
            }
        }
        if (PlayerPrefs.GetInt("Oppo") == 0)
        {
            for (int i = 0; i < oppoPlayers.Length; i++)
            {
                skms[i] = oppoPlayers[i].GetComponent<SkinnedMeshRenderer>();
                skms[i].materials = team1Materials;
            }
        }
        if (PlayerPrefs.GetInt("Oppo") == 1)
        {
            for (int i = 0; i < oppoPlayers.Length; i++)
            {
                skms[i] = oppoPlayers[i].GetComponent<SkinnedMeshRenderer>();
                skms[i].materials = team2Materials;
            }
        }
        if (PlayerPrefs.GetInt("Oppo") == 2)
        {
            for (int i = 0; i < oppoPlayers.Length; i++)
            {
                skms[i] = oppoPlayers[i].GetComponent<SkinnedMeshRenderer>();
                skms[i].materials = team3Materials;
            }
        }
    }
    private void FixedUpdate()
    {
        if (timeRest > 0)
        {
            MatchTime();
        }
        ControllUI();
    }
    private void BallIns()
    {
        insBall = Instantiate(ball, BallRandom(), Quaternion.identity);//实例化足球
        ballRgd = insBall.GetComponent<Rigidbody>();//获取足球上的刚体组件
    }
    void MatchTime()
    {
        if (timeRest < 1&&timeRest>0)
        {
            timeRest = 0;
        }
        else if(timeRest>=1)
        {
            timeRest -= Time.deltaTime;
        }       
        timeText.text = string.Format("{0:D2}:{1:D2}", (int)timeRest / 60, (int)timeRest - (int)timeRest / 60 * 60);
        if (timeRest < 60& timeRest >= 1)
        {
            AudioManager.Instance.audioSources[0].PlayOneShot(AudioManager.Instance.hurryUp,0.3f);            
        }
        else if (timeRest ==0)
        {
            timeRest = -0.1f;
            AudioManager.Instance.audioSources[0].PlayOneShot(AudioManager.Instance.end);
            canvas.gameObject.SetActive(false);
            StartCoroutine(End());
        }
    }
    public IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);
        PositionReset();
    }
    IEnumerator End()
    {
        if (Trigger.score1 > Trigger.score2)
        {
            TeamWin();
            PlayerPrefs.SetString("Win", "Team");
        }
        else if (Trigger.score1 == Trigger.score2)
        {
            NoWin();
            PlayerPrefs.SetString("Win", "None");
        }
        else
        {
            OppoWin();
            PlayerPrefs.SetString("Win", "Oppo");
        }
        yield return new WaitForSeconds(4f);
        AudioManager.Instance.audioSources[0].Stop();        
        SceneManager.LoadScene("Result");
    }
    public void TeamWin()
    {
        player1Animator.SetTrigger("Win");
        player2Animator.SetTrigger("Win");
        player3Animator.SetTrigger("Win");
        player4Animator.SetTrigger("Win");
        player5Animator.SetTrigger("Win");
        goalKeeperAnimator.SetTrigger("Win");
        opponent1Animator.SetTrigger("Lose");
        opponent2Animator.SetTrigger("Lose");
        opponent3Animator.SetTrigger("Lose");
        opponent4Animator.SetTrigger("Lose");
        opponent5Animator.SetTrigger("Lose");
        opponentGoalKeeperAnimator.SetTrigger("Lose");
    }
    public void OppoWin()
    {
        player1Animator.SetTrigger("Lose");
        player2Animator.SetTrigger("Lose");
        player3Animator.SetTrigger("Lose");
        player4Animator.SetTrigger("Lose");
        player5Animator.SetTrigger("Lose");
        goalKeeperAnimator.SetTrigger("Lose");
        opponent1Animator.SetTrigger("Win");
        opponent2Animator.SetTrigger("Win");
        opponent3Animator.SetTrigger("Win");
        opponent4Animator.SetTrigger("Win");
        opponent5Animator.SetTrigger("Win");
        opponentGoalKeeperAnimator.SetTrigger("Win");
    }
    void NoWin()
    {
        player1Animator.SetTrigger("Rest");
        player2Animator.SetTrigger("Rest");
        player3Animator.SetTrigger("Rest");
        player4Animator.SetTrigger("Rest");
        player5Animator.SetTrigger("Rest");
        goalKeeperAnimator.SetTrigger("Rest");
        opponent1Animator.SetTrigger("Rest");
        opponent2Animator.SetTrigger("Rest");
        opponent3Animator.SetTrigger("Rest");
        opponent4Animator.SetTrigger("Rest");
        opponent5Animator.SetTrigger("Rest");
        opponentGoalKeeperAnimator.SetTrigger("Rest");
    }
    Vector3 BallRandom()
    {
        Vector3 ballRandom;
        if (Random.Range(0, 2) == 0)
        {
            ballRandom = new Vector3(0, 0, 7);
        }
        else
        {
            ballRandom = new Vector3(0, 0, -7);
        }
        return ballRandom;
    }
    private void PositionReset()
    {
        insBall.transform.position = BallRandom();
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
        player1Animator.SetBool("Alert",true);
        player2Animator.SetBool("Alert", true);
        player3Animator.SetBool("Alert", true);
        player4Animator.SetBool("Alert", true);
        player5Animator.SetBool("Alert", true);
        goalKeeperAnimator.SetBool("Alert", true);
        opponent1Animator.SetBool("Alert", true);
        opponent2Animator.SetBool("Alert", true);
        opponent3Animator.SetBool("Alert", true);
        opponent4Animator.SetBool("Alert", true);
        opponent5Animator.SetBool("Alert", true);
        opponentGoalKeeperAnimator.SetBool("Alert", true);
    }
    public bool TeamAttackState()
    {
        if (player1Script.playerState == Player.PlayerState.HOLDING|| player2Script.playerState == Player.PlayerState.HOLDING|| player3Script.playerState == Player.PlayerState.HOLDING|| player4Script.playerState == Player.PlayerState.HOLDING|| player5Script.playerState == Player.PlayerState.HOLDING)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool TeamDefenseState()
    {
        if (oppo1Script.oppoState == Opponent.OppoState.HOLDING|| oppo2Script.oppoState == Opponent.OppoState.HOLDING||oppo3Script.oppoState == Opponent.OppoState.HOLDING|| oppo4Script.oppoState == Opponent.OppoState.HOLDING|| oppo5Script.oppoState == Opponent.OppoState.HOLDING)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool TeamAllAttack()
    {
        if(player1Script.playerState==Player.PlayerState.ATTACK&& player2Script.playerState == Player.PlayerState.ATTACK&& player3Script.playerState == Player.PlayerState.ATTACK&& player4Script.playerState == Player.PlayerState.ATTACK&& player5Script.playerState == Player.PlayerState.ATTACK)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool OppoAllAttack()
    {
        if (oppo1Script.oppoState == Opponent.OppoState.ATTACK && oppo2Script.oppoState == Opponent.OppoState.ATTACK && oppo3Script.oppoState == Opponent.OppoState.ATTACK && oppo4Script.oppoState == Opponent.OppoState.ATTACK && oppo5Script.oppoState == Opponent.OppoState.ATTACK)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public Transform TeamDefensePlayer()
    {
        Transform defensePlayer = Player1;
        Transform[] players = { Player2, Player3, Player4, Player5 };
        for (int i = 0; i < players.Length; i++)
        {
            //找出与足球距离最近的球员
            if (Vector3.Distance(insBall.transform.position, players[i].position) < Vector3.Distance(insBall.transform.position, defensePlayer.position))
            {
                defensePlayer = players[i];
            }
        }
        return defensePlayer;
    }
    public Transform OppoDefensePlayer()
    {
        Transform defensePlayer = Opponent1;
        Transform[] oppos = { Opponent2, Opponent3, Opponent4, Opponent5 };
        for (int i = 0; i < oppos.Length; i++)
        {
            //找出与足球距离最近的球员
            if (Vector3.Distance(insBall.transform.position, oppos[i].position) < Vector3.Distance(insBall.transform.position, defensePlayer.position))
            {
                defensePlayer = oppos[i];
            }
        }
        return defensePlayer;
    }
    public void ShootUp()
    {
        if (player1Script.playerState != Player.PlayerState.HOLDING && player2Script.playerState != Player.PlayerState.HOLDING && player3Script.playerState != Player.PlayerState.HOLDING && player4Script.playerState != Player.PlayerState.HOLDING && player5Script.playerState != Player.PlayerState.HOLDING)
        {
            isShootUp = false;
        }
        else
        {
            isShootUp = true;
        }
    }
    public void PassUp()
    {
        if (player1Script.playerState != Player.PlayerState.HOLDING && player2Script.playerState != Player.PlayerState.HOLDING && player3Script.playerState != Player.PlayerState.HOLDING && player4Script.playerState != Player.PlayerState.HOLDING && player5Script.playerState != Player.PlayerState.HOLDING)
        {
            isPassUp = false;
        }
        else
        {
            isPassUp = true;
        }
    }
    public void TackleDown()
    {
        isTackleDown = true;
    }
    void ControllUI()
    {
        if (player1Script.playerState == Player.PlayerState.HOLDING || player2Script.playerState == Player.PlayerState.HOLDING || player3Script.playerState == Player.PlayerState.HOLDING || player4Script.playerState == Player.PlayerState.HOLDING || player5Script.playerState == Player.PlayerState.HOLDING)
        {
            shoot.activated = true;
            shoot.visible = true;
            pass.activated = true;
            pass.visible = true;
            run.activated = true;
            run.visible = true;
            change.activated = false;
            change.visible = false;
            tackle.activated = false;
            tackle.visible = false;
        }
        if (player1Script.playerState == Player.PlayerState.ATTACK && player2Script.playerState == Player.PlayerState.ATTACK && player3Script.playerState == Player.PlayerState.ATTACK && player4Script.playerState == Player.PlayerState.ATTACK && player5Script.playerState == Player.PlayerState.ATTACK)
        {
            shoot.activated = false;
            shoot.visible = false;
            pass.activated = false;
            pass.visible = false;
            run.activated = true;
            run.visible = true;
            change.activated = true;
            change.visible = true;
        }
        if (player1Script.playerState == Player.PlayerState.DEFENCE && player2Script.playerState == Player.PlayerState.DEFENCE && player3Script.playerState == Player.PlayerState.DEFENCE && player4Script.playerState == Player.PlayerState.DEFENCE && player5Script.playerState == Player.PlayerState.DEFENCE)
        {
            shoot.activated = false;
            shoot.visible = false;
            pass.activated = false;
            pass.visible = false;
            run.activated = true;
            run.visible = true;
            change.activated = true;
            change.visible = true;
            tackle.activated = true;
            tackle.visible = true;
        }
    }
}
