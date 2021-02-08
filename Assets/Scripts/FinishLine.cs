using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FinishLine : MonoBehaviour
{
    [Tooltip("isFinish")]
    public bool isGameFinish = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            print("Finish!");
            GameObject parentObj = GameObject.Find("Canvas");
            GameObject finishMsg = parentObj.transform.Find("Finish").gameObject;
            finishMsg.SetActive(true);
            this.isGameFinish = true;
        }
    }
}