using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selceted : MonoBehaviour {
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
        //防守时
        //Player1被选中
        if (transform.parent == Player1)
        {
            player1Script.isSelected = true;
            player2Script.isSelected = false;
            player3Script.isSelected = false;
            player4Script.isSelected = false;
            player5Script.isSelected = false;
            //箭头始终朝向足球
            Vector3 ballDir = GameManager.Instance.insBall.transform.position - Player1.position;//足球方向
            float angle = Vector3.Angle(Player1.forward, ballDir);//计算球员前方与足球方向的夹角
            angle *= Mathf.Sign(Vector3.Cross(Player1.forward, ballDir).y);//解决角度只能计算到180的限制
            transform.localEulerAngles = new Vector3(-90, 0, angle);//改变箭头在Inspector的Rotation;
            
            if (player1Script.playerState == Player.PlayerState.DEFENCE)//如果处于防守状态
            {
                if (Input.GetKeyDown(KeyCode.S))//按切换
                {                    
                    transform.position = Player2.position;
                    transform.SetParent(Player2);
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

            Vector3 ballDir = GameManager.Instance.insBall.transform.position - Player2.position;
            float angle = Vector3.Angle(Player2.forward, ballDir);
            angle *= Mathf.Sign(Vector3.Cross(Player2.forward, ballDir).y);
            transform.localEulerAngles = new Vector3(-90, 0, angle);

            if (player2Script.playerState == Player.PlayerState.DEFENCE)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.position = Player3.position;
                    transform.SetParent(Player3);

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

            Vector3 ballDir = GameManager.Instance.insBall.transform.position - Player3.position;
            float angle = Vector3.Angle(Player3.forward, ballDir);
            angle *= Mathf.Sign(Vector3.Cross(Player3.forward, ballDir).y);
            transform.localEulerAngles = new Vector3(-90, 0, angle);

            if (player3Script.playerState == Player.PlayerState.DEFENCE)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.position = Player4.position;
                    transform.SetParent(Player4);
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

            Vector3 ballDir = GameManager.Instance.insBall.transform.position - Player4.position;
            float angle = Vector3.Angle(Player4.forward, ballDir);
            angle *= Mathf.Sign(Vector3.Cross(Player4.forward, ballDir).y);
            transform.localEulerAngles = new Vector3(-90, 0, angle);

            if (player4Script.playerState == Player.PlayerState.DEFENCE)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.position = Player5.position;
                    transform.SetParent(Player5);

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

            Vector3 ballDir = GameManager.Instance.insBall.transform.position - Player5.position;
            float angle = Vector3.Angle(Player5.forward, ballDir);
            angle *= Mathf.Sign(Vector3.Cross(Player5.forward, ballDir).y);
            transform.localEulerAngles = new Vector3(-90, 0, angle);

            if (player5Script.playerState == Player.PlayerState.DEFENCE)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.position = Player1.position;
                    transform.SetParent(Player1);

                }
            }
        }
        if (player1Script.isPassed)
        {
            transform.position = Player1.position;
            transform.SetParent(Player1);
            player1Script.isSelected = true;
            player2Script.isSelected = false;
            player3Script.isSelected = false;
            player4Script.isSelected = false;
            player5Script.isSelected = false;
        }
        else if (player2Script.isPassed)
        {
            transform.position = Player2.position;
            transform.SetParent(Player2);
            player2Script.isSelected = true;
            player1Script.isSelected = false;
            player3Script.isSelected = false;
            player4Script.isSelected = false;
            player5Script.isSelected = false;
        }
        else if (player3Script.isPassed)
        {
            transform.position = Player3.position;
            transform.SetParent(Player3);
            player3Script.isSelected = true;
            player1Script.isSelected = false;
            player2Script.isSelected = false;
            player4Script.isSelected = false;
            player5Script.isSelected = false;
        }
        else if (player4Script.isPassed)
        {
            transform.position = Player4.position;
            transform.SetParent(Player4);
            player4Script.isSelected = true;
            player1Script.isSelected = false;
            player2Script.isSelected = false;
            player3Script.isSelected = false;
            player5Script.isSelected = false;
        }
        else if (player5Script.isPassed)
        {
            transform.position = Player5.position;
            transform.SetParent(Player5);
            player5Script.isSelected = true;
            player1Script.isSelected = false;
            player2Script.isSelected = false;
            player3Script.isSelected = false;
            player4Script.isSelected = false;
        }
        if (player1Script.playerState == Player.PlayerState.HOLDING)
        {
            transform.position = Player1.position;
            transform.SetParent(Player1);
            player1Script.isSelected = true;
            player2Script.isSelected = false;
            player3Script.isSelected = false;
            player4Script.isSelected = false;
            player5Script.isSelected = false;
            player1Script.isPassed = false;
            player2Script.isPassed = false;
            player3Script.isPassed = false;
            player4Script.isPassed = false;
            player5Script.isPassed = false;
        }
        else if (player2Script.playerState == Player.PlayerState.HOLDING)
        {
            transform.position = Player2.position;
            transform.SetParent(Player2);
            player2Script.isSelected = true;
            player1Script.isSelected = false;
            player3Script.isSelected = false;
            player4Script.isSelected = false;
            player5Script.isSelected = false;
            player1Script.isPassed = false;
            player2Script.isPassed = false;
            player3Script.isPassed = false;
            player4Script.isPassed = false;
            player5Script.isPassed = false;
        }
        else if (player3Script.playerState == Player.PlayerState.HOLDING)
        {
            transform.position = Player3.position;
            transform.SetParent(Player3);
            player3Script.isSelected = true;
            player1Script.isSelected = false;
            player2Script.isSelected = false;
            player4Script.isSelected = false;
            player5Script.isSelected = false;
            player1Script.isPassed = false;
            player2Script.isPassed = false;
            player3Script.isPassed = false;
            player4Script.isPassed = false;
            player5Script.isPassed = false;
        }
        else if (player4Script.playerState == Player.PlayerState.HOLDING)
        {
            transform.position = Player4.position;
            transform.SetParent(Player4);
            player4Script.isSelected = true;
            player1Script.isSelected = false;
            player2Script.isSelected = false;
            player3Script.isSelected = false;
            player5Script.isSelected = false;
            player1Script.isPassed = false;
            player2Script.isPassed = false;
            player3Script.isPassed = false;
            player4Script.isPassed = false;
            player5Script.isPassed = false;
        }
        else if (player5Script.playerState == Player.PlayerState.HOLDING)
        {
            transform.position = Player5.position;
            transform.SetParent(Player5);
            player5Script.isSelected = true;
            player1Script.isSelected = false;
            player2Script.isSelected = false;
            player3Script.isSelected = false;
            player4Script.isSelected = false;
            player1Script.isPassed = false;
            player2Script.isPassed = false;
            player3Script.isPassed = false;
            player4Script.isPassed = false;
            player5Script.isPassed = false;
        }
    }
}
