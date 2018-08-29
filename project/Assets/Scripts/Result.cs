using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour {
    public GameObject[] prefabs;
    private GameObject team;
    private GameObject oppo;
    private Animator teamAnim;
    private Animator oppoAnim;
    // Use this for initialization
    void Start()
    {
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
    }
}
