using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Clock : MonoBehaviour
{
    #region Global Variable Declaration

    public static Clock Instance { get; set; }


    public DateTime CurrentTime
    {
        get { return currentInternalTime; }
        set
        {
            currentInternalTime = value;
            if(currentInternalTime.Hour != previousInternalTime.Hour)
            {
                HourEvent();                
            }
            previousInternalTime = currentInternalTime;
        }
    }
    private DateTime currentInternalTime;
    private DateTime previousInternalTime;
    public string currentTimeRep;

    public Transform hoursHandTrans;
    public Transform minutesHandTrans;
    public Transform secondsHandTrans;

    public TextMeshProUGUI currentEraText;

    public bool alreadyStarted = false;
    public bool keepingNormalTime;

    public float timeScale;

    public TMP_InputField hoursInput;
    public TMP_InputField minutesInput;
    public TMP_InputField secondsInput;

    #endregion

    void Awake()
    {
        if (Instance == null)
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
        CurrentTime = DateTime.MinValue;
        previousInternalTime = CurrentTime;
    }
    void Update()
    {
        HandleTime();
    }

    void HandleTime()
    {
        if (keepingNormalTime == true)
        {
            CurrentTime = CurrentTime.AddSeconds(Time.deltaTime);
            if(hoursInput != null)
            {
                hoursInput.text = CurrentTime.ToString("HH");
            }
            if (minutesInput != null)
            {
                minutesInput.text = CurrentTime.ToString("mm");
            }
            if (secondsInput != null)
            {
                secondsInput.text = CurrentTime.ToString("ss");
            }
        }
        else
        {

        }

        float secondsdegree = (-((CurrentTime.Second / 60f) * 360));
        float minutesdegree = (-((CurrentTime.Minute / 60f) * 360));
        float hoursdegree = (-((CurrentTime.Hour / 12f) * 360));

        if (secondsHandTrans != null)
        {
            secondsHandTrans.transform.localRotation = Quaternion.Euler(0, 0, secondsdegree);
        }
        if (minutesHandTrans != null)
        {
            minutesHandTrans.transform.localRotation = Quaternion.Euler(0, 0, minutesdegree);
        }
        if (hoursHandTrans != null)
        {
            hoursHandTrans.transform.localRotation = Quaternion.Euler(0, 0, hoursdegree);
        }
    }
    public bool StartStopTime()
    {
        keepingNormalTime = !keepingNormalTime;
        if(keepingNormalTime == false)
        {
            GameMasterObject.Instance.SetTimeScale(1);
        }
        if(alreadyStarted == false)
        {
            HourEvent();
            alreadyStarted = true;
        }
        return keepingNormalTime;
    }

    public void TryToSetTime()
    {
        if(keepingNormalTime == true)
        {
            return;
        }

        // Debug.Log("editted");

        string h = string.Empty;
        string m = string.Empty;
        string s = string.Empty;

        if (hoursInput != null)
        {
            h = hoursInput.text;
        }
        if (minutesInput != null)
        {
            m = minutesInput.text;
        }
        if (secondsInput != null)
        {
            s = secondsInput.text;
        }

        SetTime(h, m, s);
    }
    public void SetTime(string h, string m, string s)
    {
        if (m == string.Empty)
        {
            m = "0";
        }
        if (s == string.Empty)
        {
            s = "0";
        }

        int hour = -1;
        int minute = 0;
        int second = 0;

        int.TryParse(h, out hour);
        int.TryParse(m, out minute);
        int.TryParse(s, out second);
        
        if (hour > -1 &&
            minute > -1 &&
            second > -1)
        {
            CurrentTime = new DateTime(1, 1, 1, hour, minute, second);
        }
        else
        {
            Debug.Log("Please enter a valid date");
        }
    }

    public void HourEvent()
    {
        Debug.Log("Tick Tock : The Clock Strikes " + currentInternalTime.Hour.ToString("00"));
    }

}