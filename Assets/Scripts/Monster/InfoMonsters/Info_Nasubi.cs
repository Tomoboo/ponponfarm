using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Info_Nasubi : Info_Monsters

{
    new void Awake()
    {
        base.Awake();
        Monster = GameObject.Find("Nasubi");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        int NowPercent = 50;
        //////////////////// //Nasubi_infoの育成時間と元気度ゲージ///////////////////////
        Monster = GameObject.Find("Nasubi");
        if (Monster != null)
        {
            Monster_infoPrefab = GameObject.Find("Nasubi_info");
            second = Monster.GetComponent<Nasubi>().second;
            minute = Monster.GetComponent<Nasubi>().minute;
            hour = Monster.GetComponent<Nasubi>().hour;
            nowGauge = Monster.GetComponent<Nasubi>().nowGauge;
            if (Monster_infoPrefab != null)
            {
                //TimerText & EneGaugeの親オブジェクト
                GameObject Msfram = Monster_infoPrefab.transform.Find("Monster_Fram").gameObject;
                /////////////////////////// //TimerText////////////////////////////
                /*timerText = Msfram.transform.Find("Text_Time").GetComponent<TextMeshProUGUI>();
                timerText.text =
                    hour.ToString("00") + "時間" + minute.ToString("00") + "分" +
                      ((int)second).ToString("00") + "秒";*/
                /////////////////////////////EneGauge/////////////////////////////
                EneGauge = Msfram.transform.Find("EnergyGauge").GetComponent<Slider>();
                valuetext = EneGauge.transform.Find("Text_Value").GetComponent<TextMeshProUGUI>();
                //decrease_flg = Monster.GetComponent<Nasubi>().decrease_flg;

                //スライダーの最大値の設定
                EneGauge.maxValue = maxGauge;
                //スライダーの最小値の設定
                EneGauge.minValue = minGauge;
                //スライダーの現在値の設定
                EneGauge.value = nowGauge;
                Debug.Log("nowGauge" + nowGauge);
                valuetext.text = nowGauge.ToString("00") + "<color=#b3bedb>/</color>" + maxGauge.ToString("000");

                if (EneGauge.value >= NowPercent)
                    Enepower_flg = true; //元気ゲージがNowPercent以上の時
                else
                    Enepower_flg = false;//元気ゲージがNowPercent以下の時

            }
        }
    }
}
