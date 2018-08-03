using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Trigger : MonoBehaviour {
    public Text score1Text;
    public Text score2Text;
    
    private static  int score1;
    private static int score2;

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
        else if (other.tag == "Ball" &&transform.parent.name == "GoalMeshLeft")
        {
            score2++;
            GameManager.Instance.player1Animator.SetTrigger("Lose");
            GameManager.Instance.player2Animator.SetTrigger("Lose");
            GameManager.Instance.player3Animator.SetTrigger("Lose");
            GameManager.Instance.player4Animator.SetTrigger("Lose");
            GameManager.Instance.player5Animator.SetTrigger("Lose");
            GameManager.Instance.goalKeeperAnimator.SetTrigger("Lose");
            GameManager.Instance.opponent1Animator.SetTrigger("Win");
            GameManager.Instance.opponent2Animator.SetTrigger("Win");
            GameManager.Instance.opponent3Animator.SetTrigger("Win");
            GameManager.Instance.opponent4Animator.SetTrigger("Win");
            GameManager.Instance.opponent5Animator.SetTrigger("Win");
            GameManager.Instance.opponentGoalKeeperAnimator.SetTrigger("Win");
            StartCoroutine(GameManager.Instance.Restart());
        }
    }
}
