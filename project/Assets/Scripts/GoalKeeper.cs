using UnityEngine;

public class GoalKeeper : MonoBehaviour {
    public GameObject goalKeeper;
    private Animator goalKeeperAnimator;
    private Rigidbody goalKeeperRgd;
    private Vector3 targetPos;
    private void Start()
    {
        goalKeeperAnimator = goalKeeper.GetComponent<Animator>();
        goalKeeperRgd = goalKeeper.GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball" && transform.name == "OpponentTrigger")
        {
            if(other.transform.position.x - transform.position.x > 0)
            {
                if (other.transform.position.y < 1)
                {
                    goalKeeperAnimator.SetTrigger("Right_down");
                    targetPos = new Vector3(3, goalKeeperRgd.position.y, goalKeeperRgd.position.z);
                }
                else
                {
                    goalKeeperAnimator.SetTrigger("Right_Up");
                    targetPos = new Vector3(3, 2, goalKeeperRgd.position.z);
                }                    
            }
            else if(other.transform.position.x - transform.position.x < 0)
            {
                if (other.transform.position.y < 1)
                {
                    goalKeeperAnimator.SetTrigger("Left_down");
                    targetPos = new Vector3(-3, goalKeeperRgd.position.y, goalKeeperRgd.position.z);
                }
                else
                {
                    goalKeeperAnimator.SetTrigger("Left_Up");
                    targetPos = new Vector3(-3, 2, goalKeeperRgd.position.z);
                }                   
            }
            goalKeeperRgd.MovePosition(Vector3.Lerp(goalKeeperRgd.position, targetPos, 0.5f));
        }
        if (other.tag == "Ball" && transform.name == "Team1Trigger")
        {
            if (other.transform.position.x - transform.position.x < 0)
            {
                if (other.transform.position.y < 1)
                {
                    goalKeeperAnimator.SetTrigger("Right_down");
                    targetPos = new Vector3(-3, goalKeeperRgd.position.y, goalKeeperRgd.position.z);
                }
                else
                {
                    goalKeeperAnimator.SetTrigger("Right_Up");
                    targetPos = new Vector3(-3, 2, goalKeeperRgd.position.z);
                }
            }
            else if (other.transform.position.x - transform.position.x > 0)
            {
                if (other.transform.position.y < 1)
                {
                    goalKeeperAnimator.SetTrigger("Left_down");
                    targetPos = new Vector3(3, goalKeeperRgd.position.y, goalKeeperRgd.position.z);
                }
                else
                {
                    goalKeeperAnimator.SetTrigger("Left_Up");
                    targetPos = new Vector3(3, 2, goalKeeperRgd.position.z);
                }
            }
            goalKeeperRgd.MovePosition(Vector3.Lerp(goalKeeperRgd.position, targetPos, 0.5f));
        }
    }
}
