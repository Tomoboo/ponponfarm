using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Info_Monsters : MonoBehaviour
{
    protected TextMeshProUGUI valuetext;
    protected TextMeshProUGUI timerText;
    protected GameObject Monster_infoPrefab;
    protected GameObject director;
    protected GameObject Monster;
    protected Slider EneGauge;
    public bool Enepower_flg = false;
    protected float second;
    protected int minute;
    protected int hour;
    protected float maxGauge = 100f;
    protected float minGauge = 0;
    protected float nowGauge = 100f;

    protected virtual void Awake()
    {
        director = GameObject.Find("GameDirector");
        Monster = GameObject.Find("Monster");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        int NowPercent = 50; 
        //////////////////// //Monster_info�̈琬���Ԃƌ��C�x�Q�[�W///////////////////////
         Monster = GameObject.Find("Monster");
        if (Monster != null) { 
            Monster_infoPrefab = GameObject.Find("Monster_info");
            second = Monster.GetComponent<MonsterController>().second;
            minute = Monster.GetComponent<MonsterController>().minute;
            hour = Monster.GetComponent<MonsterController>().hour;
            nowGauge = Monster.GetComponent<MonsterController>().nowGauge;
            if (Monster_infoPrefab != null)
            {
                //TimerText & EneGauge�̐e�I�u�W�F�N�g
                GameObject Msfram = Monster_infoPrefab.transform.Find("Monster_Fram").gameObject;
                /////////////////////////// //TimerText////////////////////////////
                timerText = Msfram.transform.Find("Text_Time").GetComponent<TextMeshProUGUI>();
                 timerText.text =
                     hour.ToString("00") + "����" + minute.ToString("00") + "��" +
                       ((int)second).ToString("00") + "�b";
                /////////////////////////////EneGauge/////////////////////////////
                EneGauge = Msfram.transform.Find("EnergyGauge").GetComponent<Slider>();
                valuetext = EneGauge.transform.Find("Text_Value").GetComponent<TextMeshProUGUI>();

                //�X���C�_�[�̍ő�l�̐ݒ�
                EneGauge.maxValue = maxGauge;
                //�X���C�_�[�̍ŏ��l�̐ݒ�
                EneGauge.minValue = minGauge;
                //�X���C�_�[�̌��ݒl�̐ݒ�
                EneGauge.value = nowGauge;
                Debug.Log("nowGauge" + nowGauge);
                valuetext.text = nowGauge.ToString("00") + "<color=#b3bedb>/</color>" + maxGauge.ToString("000");
                //�������X�^�[����
                if (EneGauge.value >= NowPercent)
                    Enepower_flg = true; //���C�Q�[�W��50���ȏ�̎�
                else
                    Enepower_flg = false;//���C�Q�[�W��50���ȉ��̎�

            }
        }
    }

}
