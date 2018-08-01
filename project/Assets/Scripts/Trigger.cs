using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Trigger : MonoBehaviour {
    private Text score1Text;
    private Text score2Text;
    public Transform Player1;
    public Transform Player2;
    public Transform Player3;
    public Transform Player4;
    public Transform Player5;
    public Transform GoalKeeper;
    private Animator player1Animator;
    private Animator player2Animator;
    private Animator player3Animator;
    private Animator player4Animator;
    private Animator player5Animator;
    private Animator goalKeeperAnimator;
    private int score1;
    private int score2;
    private void Start()
    {
        score1Text = GameObject.FindGameObjectWithTag("Score1").GetComponent<Text>();
        score2Text = GameObject.FindGameObjectWithTag("Score2").GetComponent<Text>();
        //取得球员身上的脚本
        player1Animator = Player1.GetComponent<Animator>();
        player2Animator = Player2.GetComponent<Animator>();
        player3Animator = Player3.GetComponent<Animator>();
        player4Animator = Player4.GetComponent<Animator>();
        player5Animator = Player5.GetComponent<Animator>();
        goalKeeperAnimator = GoalKeeper.GetComponent<Animator>();
    }
    private void Update()
    {
        score1Text.text = score1.ToString();
        score2Text.text = score2.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball"&&transform.parent.name=="GoalMeshRight")
        {
            score1++;
            player1Animator.SetTrigger("Win");
            player2Animator.SetTrigger("Win");
            player3Animator.SetTrigger("Win");
            player4Animator.SetTrigger("Win");
            player5Animator.SetTrigger("Win");
            goalKeeperAnimator.SetTrigger("Win");
        }
        if (other.tag == "Ball" &&transform.parent.name == "GoalMeshLeft")
        {
            score2++;
        }
    }
}
