using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FarmTimer : MonoBehaviour
{
    protected GameObject Ms;
    protected TextMeshProUGUI timerText;
    public float second;
    public int minute;
    public int hour;

    protected virtual void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        second = Ms.GetComponent<MonsterController>().second;
        minute = Ms.GetComponent<MonsterController>().minute;
        hour = Ms.GetComponent<MonsterController>().hour;
        timerText.text =
                       hour.ToString("00") + "ŽžŠÔ" + minute.ToString("00") + "•ª" +
                      ((int)second).ToString("00") + "•b";
    }
}
