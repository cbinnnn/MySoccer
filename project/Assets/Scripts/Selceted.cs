using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selceted : MonoBehaviour {
    public Canvas power;
    public Transform Player1;
    public Transform Player2;
    public Transform Player3;
    public Transform Player4;
    public Transform Player5;
    private Player player1Script;
    private Player player2Script;
    private Player player3Script;
    private Player player4Script;
    private Player player5Script;
    private void Start()
    {
        //取得球员身上的脚本
        player1Script = Player1.GetComponent<Player>();
        player2Script = Player2.GetComponent<Player>();
        player3Script = Player3.GetComponent<Player>();
        player4Script = Player4.GetComponent<Player>();
        player5Script = Player5.GetComponent<Player>();
    }
    void Update ()
    {
        //无球状态时           
        if (transform.parent == Player1) //Player1被选中
        {
            player1Script.isSelected = true;
            player2Script.isSelected = false;
            player3Script.isSelected = false;
            player4Script.isSelected = false;
            player5Script.isSelected = false;
            //箭头始终朝向足球
            ArrowToBall(Player1);
            
            if (player1Script.playerState != Player.PlayerState.HOLDING)//如果处于防守状态
            {
                if (ETCInput.GetButtonDown("Change"))//按切换
                {
                    BallNearest(Player2, Player3, Player4, Player5);
                }
            }
        }
        else if (transform.parent == Player2)
        {
            player2Script.isSelected = true;
            player1Script.isSelected = false;
            player3Script.isSelected = false;
            player4Script.isSelected = false;
            player5Script.isSelected = false;

            ArrowToBall(Player2);

            if (player2Script.playerState != Player.PlayerState.HOLDING)
            {
                if (ETCInput.GetButtonDown("Change"))
                {
                    BallNearest(Player1, Player3, Player4, Player5);
                }
            }
        }
        else if (transform.parent == Player3)
        {
            player3Script.isSelected = true;
            player1Script.isSelected = false;
            player2Script.isSelected = false;
            player4Script.isSelected = false;
            player5Script.isSelected = false;

            ArrowToBall(Player3);

            if (player3Script.playerState != Player.PlayerState.HOLDING)
            {
                if (ETCInput.GetButtonDown("Change"))
                {
                     BallNearest(Player2, Player1, Player4, Player5);
                }
            }
        }
        else if (transform.parent == Player4)
        {
            player4Script.isSelected = true;
            player1Script.isSelected = false;
            player2Script.isSelected = false;
            player3Script.isSelected = false;
            player5Script.isSelected = false;

            ArrowToBall(Player4);

            if (player4Script.playerState != Player.PlayerState.HOLDING)
            {
                if (ETCInput.GetButtonDown("Change"))
                {
                     BallNearest(Player2, Player3, Player1, Player5);
                }
            }
        }
        else if (transform.parent == Player5)
        {
            player5Script.isSelected = true;
            player1Script.isSelected = false;
            player2Script.isSelected = false;
            player3Script.isSelected = false;
            player4Script.isSelected = false;

            ArrowToBall(Player5);

            if (player5Script.playerState != Player.PlayerState.HOLDING)
            {
                if (ETCInput.GetButtonDown("Change"))
                {
                    BallNearest(Player2, Player3, Player4, Player1);
                }
            }
        }
        //持球状态切换
        if (player1Script.playerState == Player.PlayerState.HOLDING)//如果持球
        {
            player1Script.passPlayer = Nearest(Player1,Player2,Player3,Player4,Player5);//设置Player脚本passPlayer参数表示该传给谁
            //把光标移到接球者身上
            transform.position = Player1.position;
            transform.SetParent(Player1);
            //把体力条移到接球者身上
            power.transform.position = Player1.position;
            power.transform.SetParent(Player1);
            power.transform.localEulerAngles = new Vector3(power.transform.localEulerAngles.x, -90, power.transform.localEulerAngles.z);
            //设置isSelcted属性
            player1Script.isSelected = true;
            player2Script.isSelected = false;
            player3Script.isSelected = false;
            player4Script.isSelected = false;
            player5Script.isSelected = false;
        }
        else if (player2Script.playerState == Player.PlayerState.HOLDING)
        {
            player2Script.passPlayer = Nearest(Player2,Player1, Player3, Player4, Player5);
            transform.position = Player2.position;
            transform.SetParent(Player2);
            power.transform.position = Player2.position;
            power.transform.SetParent(Player2);
            power.transform.localEulerAngles = new Vector3(power.transform.localEulerAngles.x, -90, power.transform.localEulerAngles.z);
            player2Script.isSelected = true;
            player1Script.isSelected = false;
            player3Script.isSelected = false;
            player4Script.isSelected = false;
            player5Script.isSelected = false;
        }
        else if (player3Script.playerState == Player.PlayerState.HOLDING)
        {
            player3Script.passPlayer = Nearest(Player3,Player2, Player1, Player4, Player5);
            transform.position = Player3.position;
            transform.SetParent(Player3);
            power.transform.position = Player3.position;
            power.transform.SetParent(Player3);
            power.transform.localEulerAngles = new Vector3(power.transform.localEulerAngles.x, -90, power.transform.localEulerAngles.z);
            player3Script.isSelected = true;
            player1Script.isSelected = false;
            player2Script.isSelected = false;
            player4Script.isSelected = false;
            player5Script.isSelected = false;
        }
        else if (player4Script.playerState == Player.PlayerState.HOLDING)
        {
            player4Script.passPlayer = Nearest(Player4,Player2, Player3, Player1, Player5);
            transform.position = Player4.position;
            transform.SetParent(Player4);
            power.transform.position = Player4.position;
            power.transform.SetParent(Player4);
            power.transform.localEulerAngles = new Vector3(power.transform.localEulerAngles.x, -90, power.transform.localEulerAngles.z);
            player4Script.isSelected = true;
            player1Script.isSelected = false;
            player2Script.isSelected = false;
            player3Script.isSelected = false;
            player5Script.isSelected = false;
        }
        else if (player5Script.playerState == Player.PlayerState.HOLDING)
        {
            player5Script.passPlayer = Nearest(Player5,Player2, Player3, Player4, Player1);
            transform.position = Player5.position;
            transform.SetParent(Player5);
            power.transform.position = Player5.position;
            power.transform.SetParent(Player5);
            power.transform.localEulerAngles = new Vector3(power.transform.localEulerAngles.x, -90, power.transform.localEulerAngles.z);
            player5Script.isSelected = true;
            player1Script.isSelected = false;
            player2Script.isSelected = false;
            player3Script.isSelected = false;
            player4Script.isSelected = false;
        }
    }
    //持球时计算接球者应该是谁
    Transform Nearest(Transform self,Transform player1,Transform player2,Transform player3,Transform player4)//输入自己和其他四个球员作为参数 
    {
        Transform minAnglePlayer=player1;//默认最适合的球员是Player1
        Transform[] players = {  player2, player3, player4 };
        for(int i = 0; i < players.Length; i++)
        {
            //与自己角度越小的球员就越适合
            if(Vector3.Angle(self.forward,players[i].position-self.position) < Vector3.Angle(self.forward, minAnglePlayer.position - self.position))
            {
                minAnglePlayer = players[i];
            }
        }
        return minAnglePlayer;
    }
    //无球是始终切换离球最近的球员
    void BallNearest(Transform player1, Transform player2, Transform player3, Transform player4)//输入其他四个球员
    {
        Transform nearestPlayer = player1;
        Transform[] players = { player2, player3, player4 };
        for (int i = 0; i < players.Length; i++)
        {
            //找出与足球距离最近的球员
            if (Vector3.Distance(GameManager.Instance.insBall.transform.position,players[i].position)<Vector3.Distance(GameManager.Instance.insBall.transform.position,nearestPlayer.position))
            {
                nearestPlayer = players[i];
            }
        }
        //把光标移到最近的球员身上
        transform.position = nearestPlayer.position;
        transform.SetParent(nearestPlayer);
        //把体力条移到最近的球员身上
        power.transform.position = nearestPlayer.position;
        power.transform.SetParent(nearestPlayer);
        power.transform.localEulerAngles = new Vector3(power.transform.localEulerAngles.x, -90, power.transform.localEulerAngles.z);
    }
    //箭头指向足球
    void ArrowToBall(Transform player)
    {
        Vector3 ballDir = GameManager.Instance.insBall.transform.position - player.position;//足球方向
        float angle = Vector3.Angle(player.forward, ballDir);//计算球员前方与足球方向的夹角
        angle *= Mathf.Sign(Vector3.Cross(player.forward, ballDir).y);//解决角度只能计算到180的限制
        transform.localEulerAngles = new Vector3(-90, 0, angle);//改变箭头在Inspector的Rotation;
    }
}
