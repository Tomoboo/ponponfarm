using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickScreen : MonoBehaviour
{
    private GameObject MsScreen1;
    private GameObject MsScreen2;
    private GameObject MsScreen3;
    private GameObject ItemScreen;
    [SerializeField] GameObject notice_info;

    void Awake()
    {
        MsScreen1 = GameObject.Find("UICanvas").transform.Find("MonsterScreen").gameObject;
        MsScreen2 = GameObject.Find("UICanvas").transform.Find("MonsterScreen – 1").gameObject;
        MsScreen3 = GameObject.Find("UICanvas").transform.Find("MonsterScreen – 2").gameObject;
        ItemScreen = GameObject.Find("UICanvas").transform.Find("FarmItemScene").gameObject;
        //Resources.Load("Monster1_info") as GameObject;
    }

    void Start()
    {
        gameObject.AddComponent<EventTrigger>();//EventTriggerコンポーネントを取得
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();//イベントの設定に入る
        entry.eventID = EventTriggerType.PointerDown;//押した瞬間

        //イベントの設定をEventTriggerに反映
        eventTrigger.triggers.Add(entry);
        entry.callback.AddListener((x) =>
        {
            if (MsScreen1.activeSelf == true)
                S_Trigger1();

            if (MsScreen2.activeSelf == true)
                S_Trigger2();

            if (MsScreen3.activeSelf == true)
                S_Trigger3();
         
            if (ItemScreen.activeSelf == true)
                S_Trigger4();
        });

    }

    private void S_Trigger1()
    {
        MsScreen1.SetActive(false);
    }

    private void S_Trigger2()
    {
        MsScreen2.SetActive(false);
    }

    private void S_Trigger3()
    {
        MsScreen3.SetActive(false);
    }

    private void S_Trigger4()
    {
        ItemScreen.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
