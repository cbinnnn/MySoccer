using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour {
    private Slider slider;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    // Update is called once per frame
    void Update () {
        //判断体力条长度，设置颜色和球员加速后的速度
        if (slider.value > 0.66f)
        {
            slider.fillRect.GetComponent<Image>().color = Color.green;
            transform.parent.parent.GetComponent<Player>().upSpeed = 12;
        }
        else if (slider.value > 0.33)
        {
            slider.fillRect.GetComponent<Image>().color = Color.yellow;
            transform.parent.parent.GetComponent<Player>().upSpeed = 8;
        }
        else if(slider.value>0.01)
        {               
            slider.fillRect.GetComponent<Image>().color = Color.red;
            transform.parent.parent.GetComponent<Player>().upSpeed = 6;
        }
        else
        {
            transform.parent.parent.GetComponent<Player>().upSpeed = 0;
        }
        slider.value = transform.parent.parent.GetComponent<Player>().power / 100;//体力条的长度与球员体力属性对应起来
        //保持体力条一直正对着屏幕
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180 - transform.parent.parent.localEulerAngles.y, transform.localEulerAngles.z);
    }
    
}
