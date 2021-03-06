using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDown : MonoBehaviour
{
    public static bool isGameStart = false;
    private int countOver = 0;
    public static float m_Timer;
    private int m_Minute;//分
    private int m_Second;//秒
    public TMP_Text countDownDisplay;

    IEnumerator CountdownToStart(int countDownTime)
    {
        GameObject countDown = GameObject.Find("CountDown");
        countDown.SetActive(true);
        while (countDownTime >= 0)
        {
            if (countDownTime > 0)
            {
                countDown.GetComponent<TMP_Text>().text = countDownTime.ToString();
                yield return new WaitForSeconds(1f);
                countDownTime--;
            }
            else
            {
                countDown.GetComponent<TMP_Text>().text = "GO!";
                yield return new WaitForSeconds(1f);
                countDownTime--;
                isGameStart = true;
            }
        }
        countDown.GetComponent<TMP_Text>().text = "";
        //countDown.GetComponent<TMP_Text>().SetActive(false);
        countOver = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        //countDownTime = 3;
        StartCoroutine(CountdownToStart(3));
    }

    // Update is called once per frame
    void Update()
    {
        bool isFinish = FinishLine.isGameFinish;
        if ((countOver == 1) && (!isFinish) && (isGameStart))
        {
            m_Timer += Time.deltaTime;
            m_Second = (int)m_Timer;
            if (m_Second > 59.0f)
            {
                m_Second = (int)(m_Timer - (m_Minute * 60));
            }
            m_Minute = (int)(m_Timer / 60);
            GameObject m_ClockText = GameObject.Find("TimeCounter");
            //m_ClockText.GetComponent<TMP_Text>().text = "1111";
            m_ClockText.GetComponent<TMP_Text>().text = string.Format("Time: {0:d2}:{1:d2}", m_Minute, m_Second);
        }

    }
}