using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    Image[] m_Images = new Image[4];

    [SerializeField]
    Sprite[] m_NumberSprites = new Sprite[10];

    [SerializeField]
    private float m_GameTimeMinute;     //分数格納用変数
    private float m_ChangeTimeSecond;   //秒数格納用変数

    public float TimeCount              
    {
        get;
        private set;
    }

    void Start()
    {
        m_ChangeTimeSecond = m_GameTimeMinute * 60;
        SetTime(m_ChangeTimeSecond);
    }
    
    public void SetTime(float time)
    {
        TimeCount = time;
        StartCoroutine(TimerStart());
    }

    //残り時間の表示
    void SetTimeNumbers(int sec, int value1, int value2)
    {
        string str = string.Format("{0:00}", sec);
        m_Images[value1].sprite = m_NumberSprites[Convert.ToInt32(str.Substring(0, 1))];
        m_Images[value2].sprite = m_NumberSprites[Convert.ToInt32(str.Substring(1, 1))];
    }

    IEnumerator TimerStart()
    {
        while (TimeCount >= 0)
        {
            int sec = Mathf.FloorToInt(TimeCount % 60);
            SetTimeNumbers(sec, 2, 3);
            int minu = Mathf.FloorToInt((TimeCount - sec) / 60);
            SetTimeNumbers(minu, 0, 1);
            yield return new WaitForSeconds(1.0f);
            TimeCount -= 1.0f;
        }
        TimeOver();
    }

    void TimeOver()
    {
        //ゲームが終わったときの処理
        SceneManager.LoadScene("Result");
    }

}
