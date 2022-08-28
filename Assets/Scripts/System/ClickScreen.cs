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
    [SerializeField] private GameObject Ms_Prefab;

    void Awake()
    {
        this.MsScreen1 = GameObject.Find("UICanvas").transform.Find("MonsterScreen").gameObject;
        this.MsScreen2 = GameObject.Find("UICanvas").transform.Find("MonsterScreen – 1").gameObject;
        this.MsScreen3 = GameObject.Find("UICanvas").transform.Find("MonsterScreen – 2").gameObject;
        this.ItemScreen = GameObject.Find("UICanvas").transform.Find("FarmItemScene").gameObject;
        //Resources.Load("Monster1_info") as GameObject;
    }

    void Start()
    {
        gameObject.AddComponent<EventTrigger>();//EventTriggerコンポーネントを取得
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();//イベントの設定に入る
        entry.eventID = EventTriggerType.PointerDown;//押した瞬間

        entry.callback.AddListener((x) =>
        {
            if (this.MsScreen1.activeSelf == true)
                S_Trigger1();

            if (this.MsScreen2.activeSelf == true)
                S_Trigger2();

            if (this.MsScreen3.activeSelf == true)
                S_Trigger3();

            if(this.Ms_Prefab != null)
                S_Trigger4();

            if (this.ItemScreen.activeSelf == true)
                S_Trigger5();
        });

        //イベントの設定をEventTriggerに反映
        eventTrigger.triggers.Add(entry);
    }

    private void S_Trigger1()
    {
        this.MsScreen1.SetActive(false);
    }

    private void S_Trigger2()
    {
        this.MsScreen2.SetActive(false);
    }

    private void S_Trigger3()
    {
        this.MsScreen3.SetActive(false);
    }

    private void S_Trigger4()
    {
        Ms_Prefab.GetComponent<Info_AnimateDialog>().Close();
    }

    private void S_Trigger5()
    {
        this.ItemScreen.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
