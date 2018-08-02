using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Trigger : MonoBehaviour {
    private Text score1Text;
    private Text score2Text;
    
    private int score1;
    private int score2;
    private void Start()
    {
        score1Text = GameObject.FindGameObjectWithTag("Score1").GetComponent<Text>();
        score2Text = GameObject.FindGameObjectWithTag("Score2").GetComponent<Text>();
       
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
            GameManager.Instance.player1Animator.SetTrigger("Win");
            GameManager.Instance.player2Animator.SetTrigger("Win");
            GameManager.Instance.player3Animator.SetTrigger("Win");
            GameManager.Instance.player4Animator.SetTrigger("Win");
            GameManager.Instance.player5Animator.SetTrigger("Win");
            GameManager.Instance.goalKeeperAnimator.SetTrigger("Win");
            GameManager.Instance.opponent1Animator.SetTrigger("Lose");
            GameManager.Instance.opponent2Animator.SetTrigger("Lose");
            GameManager.Instance.opponent3Animator.SetTrigger("Lose");
            GameManager.Instance.opponent4Animator.SetTrigger("Lose");
            GameManager.Instance.opponent5Animator.SetTrigger("Lose");
            GameManager.Instance.opponentGoalKeeperAnimator.SetTrigger("Lose");
            StartCoroutine(GameManager.Instance.Restart());
        }
        if (other.tag == "Ball" &&transform.parent.name == "GoalMeshLeft")
        {
            score2++;
        }
    }
}
