using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SerializeField]
public static class Game
{
    /*
     * 線上文件
     * https://docs.google.com/spreadsheets/d/1Ezv-0Ndg4h2v-jrBHpLttiL95uEAYYi1Yspp56oddzE/edit?usp=sharing
     */
    public static UITime UITime { get; private set; }
    public static UIOver UIOver { get; private set; }

    public static PieSpawner Spwaner { get; private set; }
    public static Camera Camera { get; private set; }

    public static Schedule Schedule { get; private set; }
    public static GameObject MainObject { get; private set; }


    public static GameObject Player { get; private set; }
    public static PlayerEvent PlayerEvent { get; private set; }
    public static PlayerState PlayerState { get; private set; }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoad()
    {
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnAfterSceneLoad()
    {
        UITime = Object.FindObjectOfType<UITime>();
        UIOver = GameObject.FindObjectOfType<UIOver>();
        Spwaner = GameObject.FindObjectOfType<PieSpawner>();
        Camera = Camera.main;
        MainObject = new GameObject("Main");
        GameObject.DontDestroyOnLoad(MainObject);
        Schedule = MainObject.GetComponent<Schedule>() ?? MainObject.AddComponent<Schedule>();

        var go = GameObject.FindObjectsOfType<GameObject>();
        var obj = System.Array.Find(go, item => item.name == "Ball");

        GameObject.DontDestroyOnLoad(obj);
        Player = obj;


        PlayerEvent = new PlayerEvent();
        PlayerState = Player.GetComponent<PlayerState>();
        PlayerState.player = Player;
        PlayerState.playerEvent = PlayerEvent;
        Schedule.UpdateEvent += UITime.UpdateTime2;
        Cursor.visible = false;
        if (Spwaner) 
        {
            Spwaner.StartEvent += StartGame;
            Spwaner.EndEvent += EndGame;
        }
        if (UIOver) 
        {
            UIOver.gameObject.SetActive(false);
            UIOver.ResetClick += () => 
            {
                Cursor.visible = false;
                UIOver.gameObject.SetActive(false);
                Spwaner.ResetPosition();
                PlayerState.CurrentState = State.General;
                Camera.GetComponent<LookAt>().ResetY();
            };
        }
        
    }

    static void EndGame() 
    {
        if (Schedule)
        {
            Schedule.StopTimer();
            Schedule.Delay(() =>
            {
                Cursor.visible = true;
                UIOver.gameObject.SetActive(true);

               var lastTime = PlayerPrefs.GetFloat("LastTime");
                PlayerPrefs.SetFloat("LastTime", Schedule.Current);

                string v = "恭喜你獲得最佳分數 \n" + "分數: {0}";
                string v2 = "並未取得最佳分數 \n" + "最佳分數: {0}";
                Debug.Log(lastTime + "  " + Schedule.Current);
                if (lastTime > Schedule.Current)
                {
                    
                    UIOver.SetText(string.Format(v, Schedule.Current.ToString("0.00")));
                }
                else
                {
                    UIOver.SetText(string.Format(v2, lastTime.ToString("0.00")));
                }
            }, 2);
           
        }

    }


    static void StartGame()
    {
        if (Schedule) 
        {
            Schedule.StartTimer();
        }
    }
}
