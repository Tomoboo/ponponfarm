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
        //////////////////// //Nasubi_info�̈琬���Ԃƌ��C�x�Q�[�W///////////////////////
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
                //TimerText & EneGauge�̐e�I�u�W�F�N�g
                GameObject Msfram = Monster_infoPrefab.transform.Find("Monster_Fram").gameObject;
                /////////////////////////// //TimerText////////////////////////////
                /*timerText = Msfram.transform.Find("Text_Time").GetComponent<TextMeshProUGUI>();
                timerText.text =
                    hour.ToString("00") + "����" + minute.ToString("00") + "��" +
                      ((int)second).ToString("00") + "�b";*/
                /////////////////////////////EneGauge/////////////////////////////
                EneGauge = Msfram.transform.Find("EnergyGauge").GetComponent<Slider>();
                valuetext = EneGauge.transform.Find("Text_Value").GetComponent<TextMeshProUGUI>();
                //decrease_flg = Monster.GetComponent<Nasubi>().decrease_flg;

                //�X���C�_�[�̍ő�l�̐ݒ�
                EneGauge.maxValue = maxGauge;
                //�X���C�_�[�̍ŏ��l�̐ݒ�
                EneGauge.minValue = minGauge;
                //�X���C�_�[�̌��ݒl�̐ݒ�
                EneGauge.value = nowGauge;
                Debug.Log("nowGauge" + nowGauge);
                valuetext.text = nowGauge.ToString("00") + "<color=#b3bedb>/</color>" + maxGauge.ToString("000");

                if (EneGauge.value >= NowPercent)
                    Enepower_flg = true; //���C�Q�[�W��NowPercent�ȏ�̎�
                else
                    Enepower_flg = false;//���C�Q�[�W��NowPercent�ȉ��̎�

            }
        }
    }
}
