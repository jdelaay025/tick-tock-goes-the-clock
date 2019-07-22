using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class GameMasterObject : MonoBehaviour
{
    #region Global Variable Declaration

    public static GameMasterObject Instance { get; set; }

    public TextMeshProUGUI startStopTimeText;


    #endregion

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        if (startStopTimeText != null)
        {
            startStopTimeText.text = (((Clock.Instance.keepingNormalTime == true) ? 
                                        "Stop" : "Start") + Environment.NewLine +
                                        "Time");
        }
    }
    void Update()
    {
        
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
        Clock.Instance.timeScale = scale;
    }
    public void SetTimeKeeping()
    {
        bool keepingtime = Clock.Instance.StartStopTime();

        if (startStopTimeText != null)
        {
            startStopTimeText.text = (((keepingtime == true) ? "Stop" : "Start") + Environment.NewLine +
                                                               "Time");
        }
    }
    /// <summary>
    /// triggered from a button press
    /// sets the time on the input fields during normal
    /// time keeping
    /// </summary>
    public void TryToSetTime()
    {
        Clock.Instance.TryToSetTime();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
