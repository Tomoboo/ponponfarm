using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickScreen : MonoBehaviour
{
    private GameObject MsScreen;
    private GameObject MsScreen1;
    private GameObject MsScreen2;
    private GameObject MsScreen3;
    private GameObject ItemScreen;
    [SerializeField] private GameObject MusicPlayListScreen = null;
    [SerializeField] private GameObject TutorialScreen = null;
    void Awake()
    {
        GameObject UICanvas = GameObject.FindWithTag("UICanvas");
        MsScreen = UICanvas.transform.Find("MonsterScreens").gameObject;
        MsScreen1 = MsScreen.transform.GetChild(0).gameObject;
        MsScreen2 = MsScreen.transform.GetChild(1).gameObject;
        MsScreen3 = MsScreen.transform.GetChild(2).gameObject;
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
            if (MsScreen1.activeSelf)
                S_Trigger1();

            if (MsScreen2.activeSelf)
                S_Trigger2();

            if (MsScreen3.activeSelf)
                S_Trigger3();

            if (ItemScreen.activeSelf)
                S_Trigger4();

            if (MusicPlayListScreen.activeSelf)
                S_Trigger5();

            if (TutorialScreen.activeSelf)
                S_Trigger6();
        });

    }

    private void S_Trigger1()
    {
        MsScreen1.SetActive(false);
        MsScreen.SetActive(false);
    }

    private void S_Trigger2()
    {
        MsScreen2.SetActive(false);
        MsScreen.SetActive(false);
    }

    private void S_Trigger3()
    {
        MsScreen3.SetActive(false);
        MsScreen.SetActive(false);
    }

    private void S_Trigger4()
    {
        ItemScreen.SetActive(false);
    }

    private void S_Trigger5()
    {
        MusicPlayListScreen.SetActive(false);
    }

    private void S_Trigger6()
    {
        TutorialScreen.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
