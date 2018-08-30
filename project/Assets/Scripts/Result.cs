using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
    public class Result : MonoBehaviour {
    public GameObject[] prefabs;
    private GameObject team;
    private GameObject oppo;
    private Animator teamAnim;
    private Animator oppoAnim;
    public Text Score;
    public Text MatchTime;
    public Text teamName;
    public Text oppoName;
    public Text press;
    private Tweener tweener;
    // Use this for initialization
    void Start()
    {
        teamName.text = TeamCreation.Instance.teamPrefabs[PlayerPrefs.GetInt("Team")].name;
        oppoName.text = TeamCreation.Instance.teamPrefabs[PlayerPrefs.GetInt("Oppo")].name;
        Score.text = Trigger.score1 + "   :   " + Trigger.score2; 
        MatchTime.text = string.Format("{0:D2}:{1:D2}", (int)GameManager.Instance.matchTime / 60, (int)GameManager.Instance.matchTime - (int)GameManager.Instance.matchTime / 60 * 60);
        Time.timeScale = 1;
        if (PlayerPrefs.GetInt("Team") == 0)
        {
            team=Instantiate(prefabs[0], new Vector3(-10, -2, -2), Quaternion.Euler(0, 150, 0));
        }
        if (PlayerPrefs.GetInt("Team") == 1)
        {
            team = Instantiate(prefabs[1], new Vector3(-10, -2, -2), Quaternion.Euler(0, 150, 0));
        }
        if (PlayerPrefs.GetInt("Team") == 2)
        {
            team = Instantiate(prefabs[2], new Vector3(-10, -2, -2), Quaternion.Euler(0, 150, 0));
        }
        if (PlayerPrefs.GetInt("Oppo") == 0)
        {
            oppo = Instantiate(prefabs[0], new Vector3(10, -2, -2), Quaternion.Euler(0, 210, 0));
        }
        if (PlayerPrefs.GetInt("Oppo") == 1)
        {
            oppo = Instantiate(prefabs[1], new Vector3(10, -2, -2), Quaternion.Euler(0, 210, 0));
        }
        if (PlayerPrefs.GetInt("Oppo") == 2)
        {
            oppo = Instantiate(prefabs[2], new Vector3(10, -2, -2), Quaternion.Euler(0, 210, 0));
        }
        teamAnim = team.GetComponent<Animator>();
        oppoAnim = oppo.GetComponent<Animator>();
        if (PlayerPrefs.GetString("Win") == "Team")
        {
            teamAnim.SetTrigger("Win");
            oppoAnim.SetTrigger("Lose");
        }
        else if(PlayerPrefs.GetString("Win") == "None")
        {
            teamAnim.SetTrigger("None");
            oppoAnim.SetTrigger("None");
        }
        else
        {
            teamAnim.SetTrigger("Lose");
            oppoAnim.SetTrigger("Win");
        }
       tweener = press.DOFade(1, 1).SetDelay(2.5f).SetLoops(-1,LoopType.Yoyo);
    }
    public void OnPress()
    {
        SceneManager.LoadScene("Menu");
    }
    public void OnAgain()
    {
        SceneManager.LoadScene("Select");
    }
}
