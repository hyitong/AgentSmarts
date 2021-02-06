using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Counter : MonoBehaviour
{
    private float m_Timer;
    private int m_Minute;//分
    private int m_Second;//秒
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        m_ClockText.GetComponent<TMP_Text>().text = string.Format("{0:d2}:{1:d2}", m_Minute, m_Second);
    }
}
