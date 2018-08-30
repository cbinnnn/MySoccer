using System.Collections;
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
    public float matchTime=10;
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
        yield return new WaitForSeconds(3f);
        AudioManager.Instance.audioSources[0].Stop();
        if (Trigger.score1 > Trigger.score2)
        {
            PlayerPrefs.SetString("Win", "Team");
        }
        else if (Trigger.score1 == Trigger.score2)
        {
            PlayerPrefs.SetString("Win", "None");
        }
        else
        {
            PlayerPrefs.SetString("Win", "Oppo");
        }
        SceneManager.LoadScene("Result");
    }
    Vector3 BallRandom()
    {
        Vector3 ballRandom;
        if (Random.Range(0, 1) == 0)
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
    public bool AttackState()
    {
        if (player1Script.playerState == Player.PlayerState.HOLDING)
        {
            return true;
        }
        else if (player2Script.playerState == Player.PlayerState.HOLDING)
        {
            return true;
        }
        else if (player3Script.playerState == Player.PlayerState.HOLDING)
        {
            return true;
        }
        else if (player4Script.playerState == Player.PlayerState.HOLDING)
        {
            return true;
        }
        else if (player5Script.playerState == Player.PlayerState.HOLDING)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool DefenseState()
    {
        return false;
    }
    public bool AllAttack()
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
    public Transform DefensePlayer()
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
