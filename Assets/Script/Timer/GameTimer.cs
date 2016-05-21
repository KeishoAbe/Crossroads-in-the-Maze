using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{

    [SerializeField]
    Image[] images = new Image[4];

    [SerializeField]
    Sprite[] numberSprites = new Sprite[10];

    [SerializeField]
    private float gameTimeMinute;
    private float changeTimeSecond;

    public float TimeCount
    {
        get;
        private set;
    }

    void Start()
    {
        changeTimeSecond = gameTimeMinute * 60;
        SetTime(changeTimeSecond);
    }

    void Update()
    {

    }

    public void SetTime(float time)
    {
        TimeCount = time;
        StartCoroutine(TimerStart());
    }

    void SetTimeNumbers(int sec, int value1, int value2)
    {
        string str = string.Format("{0:00}", sec);
        images[value1].sprite = numberSprites[Convert.ToInt32(str.Substring(0, 1))];
        images[value2].sprite = numberSprites[Convert.ToInt32(str.Substring(1, 1))];
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
