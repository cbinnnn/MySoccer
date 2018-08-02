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
        if (other.tag == "Ball")
        {
            if(other.transform.position.x - transform.position.x > 0)
            {
                goalKeeperAnimator.SetTrigger("Right_down");
                goalKeeperRgd.velocity = new Vector3(5, 0, 0);
            }
            else if(other.transform.position.x - transform.position.x < 0)
            {
                goalKeeperAnimator.SetTrigger("Left_down");
                goalKeeperRgd.velocity = new Vector3(-5, 0, 0);
            }
        }
    }
}
