using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreText : MonoBehaviour
{
    public static int coinAmount;
    void Start()
    {
        coinAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject scoreText = GameObject.Find("Score");
        scoreText.GetComponent<TMP_Text>().text = "score: " + coinAmount.ToString();
    }
}
