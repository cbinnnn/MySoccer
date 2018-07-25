using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject ball;
	// Use this for initialization
	void Start () {
        BallReset();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void BallReset()
    {
        Instantiate(ball);
    }
}
