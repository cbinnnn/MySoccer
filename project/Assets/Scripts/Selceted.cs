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
        player1Script = Player1.GetComponent<Player>();
        player2Script = Player2.GetComponent<Player>();
        player3Script = Player3.GetComponent<Player>();
        player4Script = Player4.GetComponent<Player>();
        player5Script = Player5.GetComponent<Player>();
    }
    void Update () {
        if (transform.parent == Player1)
        {
            player1Script.isSelected = true;
            player2Script.isSelected = false;
            player3Script.isSelected = false;
            player4Script.isSelected = false;
            player5Script.isSelected = false;
            if (player1Script.playerState == Player.PlayerState.ATTACK)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.position = Player2.position;
                    transform.parent = Player2;
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
            if (player2Script.playerState == Player.PlayerState.ATTACK)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.position = Player3.position;
                    transform.parent = Player3;
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
            if (player3Script.playerState == Player.PlayerState.ATTACK)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.position = Player4.position;
                    transform.parent = Player4;
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
            if (player4Script.playerState == Player.PlayerState.ATTACK)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.position = Player5.position;
                    transform.parent = Player5;
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
            if (player5Script.playerState == Player.PlayerState.ATTACK)
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.position = Player1.position;
                    transform.parent = Player1;
                }
            }
        }

    }
}
