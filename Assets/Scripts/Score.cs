using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public static int scoreNum;
    private GameObject scoretext;
    void Start()
    {
        scoreNum = 0;
        scoretext = GameObject.Find("Canvas").transform.Find("Score").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            scoreNum++;
            scoretext.GetComponent<TMP_Text>().text = "Stars: " + scoreNum.ToString();
            CarAgent carAgent = other.transform.GetComponent<CarAgent>();
            carAgent.AddReward(1f);
        }
        //ScoreText.coinAmount += 1;
    }
}
