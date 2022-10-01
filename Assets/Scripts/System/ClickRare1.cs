﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickRare1 : MonoBehaviour
{
    private GameObject MsScreen1;
    private GameObject MsScreen2;
    private GameObject MsScreen3;

    void Awake()
    {
        this.MsScreen1 = GameObject.Find("UICanvas").transform.Find("MonsterScreen").gameObject;
        this.MsScreen2 = GameObject.Find("UICanvas").transform.Find("MonsterScreen – 1").gameObject;
        this.MsScreen3 = GameObject.Find("UICanvas").transform.Find("MonsterScreen – 2").gameObject;
    }

    void Start()
    {
        gameObject.AddComponent<EventTrigger>();//EventTriggerコンポーネントを取得
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();//イベントの設定に入る
        entry.eventID = EventTriggerType.PointerDown;//押した瞬間

        entry.callback.AddListener((x) =>
        {
                SwitchScreen1();
        });

        //イベントの設定をEventTriggerに反映
        eventTrigger.triggers.Add(entry);
    }

    private void SwitchScreen1()
    {
        MsScreen1.SetActive(true);
        if (MsScreen2.activeSelf == true)
            MsScreen2.SetActive(false);

        else if (MsScreen3.activeSelf == true)
            MsScreen3.SetActive(false);
    }

}

