using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckPoint : MonoBehaviour
{
    [Tooltip("isChecked")]
    public bool isChecked = false;
    public static int checkNum;

    void Start()
    {
        //countDownTime = 3;
        checkNum = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            this.isChecked = true;
            checkNum++;
            print("Finish!");
            GameObject parentObj = GameObject.Find("Canvas");
            GameObject checkPoint = parentObj.transform.Find("Checked").gameObject;
            checkPoint.GetComponent<TMP_Text>().text = "Checkpoints: " + checkNum.ToString();
            
        }
    }
}
