using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour {
    private float smoothing = 5;
    private Vector3 offset;
    private Vector3 endPos;
	// Use this for initialization
	void Start () {
        offset = transform.position - Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        if((GameManager.Instance.insBall.transform.position + offset ).x< -25)
        {
            if ((GameManager.Instance.insBall.transform.position + offset).z > -8.5f)
            {
                if ((GameManager.Instance.insBall.transform.position + offset).z > 7f)
                    endPos = new Vector3(-25, (GameManager.Instance.insBall.transform.position + offset).y, 7);
                else
                    endPos = new Vector3(-25, (GameManager.Instance.insBall.transform.position + offset).y, (GameManager.Instance.insBall.transform.position + offset).z);
            }
            else
                endPos = new Vector3(-25, (GameManager.Instance.insBall.transform.position + offset).y, -8.5f);
            
        }
        else
        {
            if ((GameManager.Instance.insBall.transform.position + offset).z < -8.5f)
                endPos = new Vector3((GameManager.Instance.insBall.transform.position + offset).x, (GameManager.Instance.insBall.transform.position + offset).y, -8.5f);
            else
            {
                if ((GameManager.Instance.insBall.transform.position + offset).z > 7f)
                    endPos = new Vector3((GameManager.Instance.insBall.transform.position + offset).x, (GameManager.Instance.insBall.transform.position + offset).y, 7f);
                else
                    endPos = (GameManager.Instance.insBall.transform.position + offset);
            }           
        }
            transform.position = Vector3.Lerp(transform.position, endPos, Time.deltaTime*smoothing);
	}
}
