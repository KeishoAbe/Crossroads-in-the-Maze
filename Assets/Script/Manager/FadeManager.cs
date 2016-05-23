using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{

    #region Singleton

    private static FadeManager instance;

    public static FadeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (FadeManager)FindObjectOfType(typeof(FadeManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(FadeManager) + "is nothing");
                }
            }
            return instance;
        }
    }

    #endregion Singleton

    //デバッグモード
    public bool m_DebugMode = true;

    //フェード中の透明度
    private float m_FadeAlpha = 0;

    //フェード中かどうか
    private bool m_IsFading = false;

    //フェードの色
    public Color m_FadeColor = Color.black;


    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    //　DontDestroyOnLoadについて
    //Unityではシーンを切り替えるとGameObject等は全部破棄される。
    //しかし、ゲームのスコア、キャラクターの情報はとっておきたい
    //ケースは多々ある。
    //GameObjectの場合はDontDestroyOnLoadを使用。
    //引数に指定したGameObjectは破棄されなくなりSceneの切り替え時
    //自動で引き継ぐ
    //参照URL(http://qiita.com/srtkmsw/items/bf6a33d6bb2987c74936)

        //DebugMode用GUIメニュー
    public void OnGUI()
    {
        if (this.m_IsFading)
        {
            this.m_FadeColor.a = this.m_FadeAlpha;
            GUI.color = this.m_FadeColor;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
        }

        if (this.m_DebugMode)
        {
            if (!this.m_IsFading)
            {
                //Scene一覧の作成
                List<string> scenes = new List<string>();
                scenes.Add("Title");
                scenes.Add("Menu");
                scenes.Add("Game");
                scenes.Add("Result");

                //Sceneが1つもない時
                if (scenes.Count == 0)
                {
                    GUI.Box(new Rect(10, 10, 200, 50), "Fade Manager(Debug Mode)");
                    GUI.Label(new Rect(20, 35, 180, 20), "Scene not found.");
                    return;
                }

                GUI.Box(new Rect(10, 10, 300, 50 + scenes.Count * 25), "Fade Manager(Debug Mode)");
                GUI.Label(new Rect(20, 30, 280, 20), "Current Scene : " + SceneManager.GetActiveScene().name);

                int i = 0;
                foreach (string sceneName in scenes)
                {

                    if (Input.GetKeyDown(KeyCode.Q) && sceneName == "Game")
                    {
                        LoadLevel("Result", 1.0f);
                    }




                    if(GUI.Button(new Rect(20, 55 + i * 25, 100, 20), "Load Level"))
                    {
                        LoadLevel(sceneName, 1.0f);
                    }
                    GUI.Label(new Rect(125, 55 + i * 25, 1000, 20), sceneName);
                    i++;
                }
            }
        }
    }


    public void LoadLevel(string scene, float interval)
    {
        StartCoroutine(TransScene(scene, interval));
    }

    private IEnumerator TransScene(string scene, float interval)
    {
        //フェードアウト
        this.m_IsFading = true;
        float time = 0;
        while (time <= interval)
        {
            this.m_FadeAlpha = Mathf.Lerp(0.0f, 1.0f, time / interval);   //Mathf.Lerpについて　http://docs.unity3d.com/jp/current/ScriptReference/Mathf.Lerp.html
            time += Time.deltaTime;
            yield return 0;
        }

        //シーン切り替え
        SceneManager.LoadScene(scene);

        //フェードイン
        time = 0;
        while (time <= interval)
        {
            this.m_FadeAlpha = Mathf.Lerp(1.0f, 0.0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }
        this.m_IsFading = false;
    }

}
