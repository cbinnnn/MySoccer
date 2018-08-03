using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalKeeper : MonoBehaviour {
    public GameObject goalKeeper;
    private Animator goalKeeperAnimator;
    private Rigidbody goalKeeperRgd;
    private void Start()
    {
        goalKeeperAnimator = goalKeeper.GetComponent<Animator>();
        goalKeeperRgd = goalKeeper.GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball" && transform.parent.name == "GoalKeeper_Team1")
        {
            if(other.transform.position.x - transform.position.x > 0)
            {
                goalKeeperAnimator.SetTrigger("Right_down");
            }
            else if(other.transform.position.x - transform.position.x < 0)
            {
                goalKeeperAnimator.SetTrigger("Left_down");
            }

        }
        if (other.tag == "Ball" && transform.parent.name == "GoalKeeper_Opponent")
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
