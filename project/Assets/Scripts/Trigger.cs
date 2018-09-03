using UnityEngine;
using UnityEngine.UI;
public class Trigger : MonoBehaviour {
    public Text score1Text;
    public Text score2Text;
    
    public static  int score1;
    public static int score2;
    
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
            GameManager.Instance.TeamWin();
            AudioManager.Instance.audioSources[0].PlayOneShot(AudioManager.Instance.applause);
            StartCoroutine(GameManager.Instance.Restart());
        }
        else if (other.tag == "Ball" &&transform.parent.name == "GoalMeshLeft")
        {
            score2++;
            GameManager.Instance.OppoWin();
            StartCoroutine(GameManager.Instance.Restart());
        }
    }
}
