using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : MonoBehaviour {
    public GameObject goalKeeper;
    private Animator goalKeeperAnimator;
    private Rigidbody goalKeeperRgd;
    public float smoothing = 20f;
    private void Start()
    {
        goalKeeperAnimator = goalKeeper.GetComponent<Animator>();
        goalKeeperRgd = goalKeeper.GetComponent<Rigidbody>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ball" && transform.parent.name == "GoalKeeper_Opponent")
        {
            if(other.transform.position.x - transform.position.x > 0)
            {
                if(other.transform.position.y<2)
                    goalKeeperAnimator.SetTrigger("Right_down");
                else
                    goalKeeperAnimator.SetTrigger("Right_Up");
            }
            else if(other.transform.position.x - transform.position.x < 0)
            {
                if (other.transform.position.y < 2)
                    goalKeeperAnimator.SetTrigger("Left_down");
                else
                    goalKeeperAnimator.SetTrigger("Left_Up");
            }
            goalKeeperRgd.MovePosition(Vector3.Lerp(goalKeeperRgd.position, other.transform.position,smoothing* Time.deltaTime));
        }
        if (other.tag == "Ball" && transform.parent.name == "GoalKeeper_Team1")
        {
            if (other.transform.position.x - transform.position.x < 0)
            {
                goalKeeperAnimator.SetTrigger("Right_down");
            }
            else if (other.transform.position.x - transform.position.x > 0)
            {
                goalKeeperAnimator.SetTrigger("Left_down");
            }

        }
    }
}
