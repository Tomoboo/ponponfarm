using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Monster;

public class Info_Monsters : MonoBehaviour
{
    protected TextMeshProUGUI valueText;
    protected GameObject monsterInfo = null;
    protected GameObject monster = null;
    protected Slider eneGauge;
    protected float maxGauge = 100f;
    protected float minGauge = 0;
    protected float nowGauge = 100f;
    string[] monsterInfoName;
    protected virtual void Awake()
    {
        monsterInfoName = new string[] { "Nasubi_info", "Orange_info", "Carot_info", "Cabbage_info", "Banana_info",
                                           "Corn_info","Pieman_info", "Peach_info", "Melon_info", "Suika_info" };
    }

    void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        int nowPercent = 50;
        GameObject tmp = GameObject.FindWithTag("Monster_Info");
        //////////////////////Monster_info///////////////////////
        if (GameObject.FindWithTag("Monster_Info"))
        {
            JudgeMonsterGauge(tmp.name);
            if (monsterInfo != null)
            {
                GameObject Msfram = monsterInfo.transform.Find("Monster_Fram").gameObject;

                eneGauge = Msfram.transform.Find("EnergyGauge").GetComponent<Slider>();
                valueText = eneGauge.transform.Find("Text_Value").GetComponent<TextMeshProUGUI>();

                eneGauge.maxValue = maxGauge;

                eneGauge.minValue = minGauge;

                eneGauge.value = nowGauge;
                valueText.text = nowGauge.ToString("00") + "<color=#b3bedb>/</color>" + maxGauge.ToString("000");

                if (eneGauge.value >= nowPercent)
                {
                    monster.GetComponent<MonsterController>().DecreaseEnePower();
                }
            }
        }

    }

    public void FindMonster(string scriptName)
    {
        monster = GameObject.Find(scriptName);
        monsterInfo = GameObject.Find(scriptName + "_info");
    }

    void JudgeMonsterGauge(string name)
    {
        switch (name)
        {
            case "Nasubi_info":
                FindMonster("Nasubi");
                if (monster != null)
                {
                    if (monster.name == "Nasubi")
                    {
                        nowGauge = monster.GetComponent<Nasubi>().param.nowGauge;
                    }
                }
                break;
            case "Orange_info":
                FindMonster("Orange");
                if (monster != null)
                {
                    if (monster.name == "Orange")
                    {
                        nowGauge = monster.GetComponent<Orange>().param.nowGauge;
                    }
                }
                break;
            case "Carot_info":
                FindMonster("Carot");
                if (monster != null)
                {
                    if (monster.name == "Carot")
                    {
                        nowGauge = monster.GetComponent<Carot>().param.nowGauge;
                    }
                }

                break;
            case "Cabbage_info":
                FindMonster("Cabbage");
                if (monster != null)
                {
                    if (monster.name == "Cabbage")
                    {
                        Debug.Log("Cabbage selected");
                        nowGauge = monster.GetComponent<Cabbage>().param.nowGauge;
                    }
                }
                break;
            case "Banana_info":
                FindMonster("Banana");
                if (monster != null)
                {
                    if (monster.name == "Banana")
                    {
                        nowGauge = monster.GetComponent<Banana>().param.nowGauge;
                    }
                }
                break;
            case "Corn_info":
                FindMonster("Corn");
                if (monster != null)
                {
                    if (monster.name == "Corn")
                    {
                        Debug.Log("Corn selected");
                        nowGauge = monster.GetComponent<Corn>().param.nowGauge;
                    }
                }
                break;
            case "Pieman_info":
                FindMonster("Pieman");
                if (monster != null)
                {
                    if (monster.name == "Pieman")
                    {
                        nowGauge = monster.GetComponent<Pieman>().param.nowGauge;
                    }
                }
                break;
            case "Peach_info":
                FindMonster("Peach");
                if (monster != null)
                {
                    if (monster.name == "Peach")
                    {
                        Debug.Log("Peach selected");
                        nowGauge = monster.GetComponent<Peach>().param.nowGauge;
                    }
                }
                break;
            case "Melon_info":
                FindMonster("Melon");
                if (monster != null)
                {
                    if (monster.name == "Melon")
                    {
                        Debug.Log("Melon selected");
                        nowGauge = monster.GetComponent<Melon>().param.nowGauge;
                    }
                }
                break;
            case "Suika_info":
                FindMonster("Suika");
                if (monster != null)
                {
                    if (monster.name == "Suika")
                    {
                        Debug.Log("Suika selected");
                        nowGauge = monster.GetComponent<Suika>().param.nowGauge;
                    }
                }
                break;
            default:
                break;
        }
    }
}


