using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootPower : MonoBehaviour {
    public Slider powerSlider;
    public Slider shootSlider;
    private Player playerScript;
    // Update is called once per frame
    void Update () {
        playerScript = transform.parent.GetComponent<Player>();
        powerSlider.value = playerScript.power / 100;//体力条的长度与球员体力属性对应起来
        shootSlider.value = playerScript.timer / 10;
        Power();
        //保持一直正对着屏幕
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 90 - transform.parent.localEulerAngles.y, transform.localEulerAngles.z);
    }
    void Power()
    {
        if (powerSlider.value > 0.66f)
        {
            powerSlider.fillRect.GetComponent<Image>().color = Color.green;
            transform.parent.GetComponent<Player>().upSpeed = 12;
        }
        else if (powerSlider.value > 0.33)
        {
            powerSlider.fillRect.GetComponent<Image>().color = Color.yellow;
            transform.parent.GetComponent<Player>().upSpeed = 8;
        }
        else if (powerSlider.value > 0.01)
        {
            powerSlider.fillRect.GetComponent<Image>().color = Color.red;
            transform.parent.GetComponent<Player>().upSpeed = 6;
        }
        else
        {
            transform.parent.GetComponent<Player>().upSpeed = 0;
        }
    }
}
