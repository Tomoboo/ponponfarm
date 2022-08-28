using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NasubiTimer : FarmTimer
{
    new void Awake()
    {
        base.Awake();
        Ms = GameObject.Find("Nasubi");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        second = Ms.GetComponent<Nasubi>().second;
        minute = Ms.GetComponent<Nasubi>().minute;
        hour = Ms.GetComponent<Nasubi>().hour;
        timerText.text =
                       hour.ToString("00") + "ŽžŠÔ" + minute.ToString("00") + "•ª" +
                      ((int)second).ToString("00") + "•b";
    }
}
