using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeamCreation : MonoBehaviour {
    public GameObject[] teamPrefabs;
    private GameObject[] teamGameobjs;
    private GameObject[] oppoGameobjs;
    private int teamSelectIndex=0;
    private int oppoSelectIndex = 0;
    private int length;
    public Sprite sprite;
    public Button teamReady;
    public Button oppoReady;
    private Sprite normal;
    private bool isTeamReady;
    private bool isOppoReday;
	// Use this for initialization
	void Start () {
        normal = teamReady.image.sprite;
        length = teamPrefabs.Length;
        teamGameobjs = new GameObject[length];
        oppoGameobjs = new GameObject[length];
        for(int i = 0; i < length; i++)
        {
            teamGameobjs[i] = Instantiate(teamPrefabs[i], new Vector3 (-5,-1,-2),transform.rotation);
            oppoGameobjs[i] = Instantiate(teamPrefabs[i], new Vector3 (5, -1, -2), transform.rotation);
        }
        Show();
	}
	void Show()
    {
        teamGameobjs[teamSelectIndex].SetActive(true);
        oppoGameobjs[oppoSelectIndex].SetActive(true);
        for (int i = 0; i < length; i++)
        {
            if (i != teamSelectIndex)
            {
                teamGameobjs[i].SetActive(false);
            }
        }
        for (int i = 0; i < length; i++)
        {
            if (i != oppoSelectIndex)
            {
                oppoGameobjs[i].SetActive(false);
            }
        }
    }
    public void OnTeamNext()
    {
        teamSelectIndex++;
        teamSelectIndex %= length;
        Show();
    }
    public void OnTeamPrev()
    {
        teamSelectIndex--;
        if (teamSelectIndex == -1)
        {
            teamSelectIndex = length - 1;
        }
        Show();
    }
    public void OnOppoNext()
    {
        oppoSelectIndex++;
        oppoSelectIndex %= length;
        Show();
    }
    public void OnOppoPrev()
    {
        oppoSelectIndex--;
        if (oppoSelectIndex == -1)
        {
            oppoSelectIndex = length - 1;
        }
        Show();
    }
    public void OnTeamReady()
    {
        if (teamReady.image.sprite == normal)
        {
            teamReady.image.sprite = sprite;
            teamGameobjs[teamSelectIndex].GetComponent<Animator>().SetBool("Select", true);
            teamGameobjs[teamSelectIndex].GetComponent<Animator>().SetBool("Cancel", false);
            isTeamReady = true;
        }
        else
        {
            teamReady.image.sprite = normal;
            teamGameobjs[teamSelectIndex].GetComponent<Animator>().SetBool("Select", false);
            teamGameobjs[teamSelectIndex].GetComponent<Animator>().SetBool("Cancel", true);
            isTeamReady = false;
        }           
    }
    public void OnOppoReady()
    {
        if (oppoReady.image.sprite == normal)
        {
            oppoReady.image.sprite = sprite;
            oppoGameobjs[oppoSelectIndex].GetComponent<Animator>().SetBool("Select", true);
            oppoGameobjs[oppoSelectIndex].GetComponent<Animator>().SetBool("Cancel", false);
            isOppoReday = true;
        }            
        else
        {
            oppoReady.image.sprite = normal;
            oppoGameobjs[oppoSelectIndex].GetComponent<Animator>().SetBool("Select", false);
            oppoGameobjs[oppoSelectIndex].GetComponent<Animator>().SetBool("Cancel", true);
            isOppoReday = false;
        }            
    }
    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Go()
    {
        if (isTeamReady && isOppoReday)
        {
            PlayerPrefs.SetInt("Team", teamSelectIndex);
            PlayerPrefs.SetInt("Oppo", oppoSelectIndex);
            AudioManager.Instance.audioSources[0].Play();
            Globe.nextSceneName = "Game";
            SceneManager.LoadScene("Loading");
            Time.timeScale = 1;
        }
        else
        {
            Debug.Log("双方还没准备好");
        }
    }
}
