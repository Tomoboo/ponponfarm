using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickRare3 : MonoBehaviour
{
    private GameObject MsScreen1;
    private GameObject MsScreen2;
    private GameObject MsScreen3;

    void Awake()
    {
        GameObject UICanvas = GameObject.FindWithTag("UICanvas");
        MsScreen1 = UICanvas.transform.Find("MonsterScreens").transform.GetChild(0).gameObject;
        MsScreen2 = UICanvas.transform.Find("MonsterScreens").transform.GetChild(1).gameObject;
        MsScreen3 = UICanvas.transform.Find("MonsterScreens").transform.GetChild(2).gameObject;
    }

    void Start()
    {
        gameObject.AddComponent<EventTrigger>();//EventTriggerコンポーネントを取得
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();//イベントの設定に入る
        entry.eventID = EventTriggerType.PointerDown;//押した瞬間

        entry.callback.AddListener((x) =>
        {
            SwitchScreen3();
        });

        //イベントの設定をEventTriggerに反映
        eventTrigger.triggers.Add(entry);
    }

    private void SwitchScreen3()
    {
        MsScreen3.SetActive(true);
        if (MsScreen2.activeSelf == true)
            MsScreen2.SetActive(false);

        else if (MsScreen1.activeSelf == true)
            MsScreen1.SetActive(false);
    }

}

