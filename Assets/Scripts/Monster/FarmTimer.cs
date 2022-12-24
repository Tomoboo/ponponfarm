using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Monster;

public class FarmTimer : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    private GameObject monster;
    protected TextMeshProUGUI timerText;
    [System.NonSerialized] public float timerSecond;
    [System.NonSerialized] public int timerMinute;
    [System.NonSerialized] public int timerHour;


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
        if (GameObject.FindWithTag("Monster"))
        {
            Judge_monster_timer(monsterPrefab.name);
            timerText.text =
                timerHour.ToString("00") + "時間" + timerMinute.ToString("00") + "分" +
                ((int)timerSecond).ToString("00") + "秒";
        }
    }

    public void Judge_monster_timer(string name)
    {
        switch (name)
        {
            case "Nasubi":
                if (monster == null)
                {
                    monster = GameObject.Find("Nasubi");
                }
                timerSecond = monster.GetComponent<Nasubi>().param.second;
                timerMinute = monster.GetComponent<Nasubi>().param.minute;
                timerHour = monster.GetComponent<Nasubi>().param.hour;
                break;
            case "Orange":
                if (monster == null)
                {
                    monster = GameObject.Find("Orange");
                }
                timerSecond = monster.GetComponent<Orange>().param.second;
                timerMinute = monster.GetComponent<Orange>().param.minute;
                timerHour = monster.GetComponent<Orange>().param.hour;
                break;
            case "Carot":
                if (monster == null)
                {
                    monster = GameObject.Find("Carot");
                }
                timerSecond = monster.GetComponent<Carot>().param.second;
                timerMinute = monster.GetComponent<Carot>().param.minute;
                timerHour = monster.GetComponent<Carot>().param.hour;
                break;
            case "Cabbage":
                if (monster == null)
                {
                    monster = GameObject.Find("Cabbage");
                }
                timerSecond = monster.GetComponent<Cabbage>().param.second;
                timerMinute = monster.GetComponent<Cabbage>().param.minute;
                timerHour = monster.GetComponent<Cabbage>().param.hour;
                break;
            case "Banana":
                if (monster == null)
                {
                    monster = GameObject.Find("Banana");
                }
                timerSecond = monster.GetComponent<Banana>().param.second;
                timerMinute = monster.GetComponent<Banana>().param.minute;
                timerHour = monster.GetComponent<Banana>().param.hour;
                break;
            case "Corn":
                if (monster == null)
                {
                    monster = GameObject.Find("Corn");
                }
                timerSecond = monster.GetComponent<Corn>().param.second;
                timerMinute = monster.GetComponent<Corn>().param.minute;
                timerHour = monster.GetComponent<Corn>().param.hour;
                break;
            case "Pieman":
                if (monster == null)
                {
                    monster = GameObject.Find("Pieman");
                }
                timerSecond = monster.GetComponent<Pieman>().param.second;
                timerMinute = monster.GetComponent<Pieman>().param.minute;
                timerHour = monster.GetComponent<Pieman>().param.hour;
                break;
            case "Peach":
                if (monster == null)
                {
                    monster = GameObject.Find("Peach");
                }
                timerSecond = monster.GetComponent<Peach>().param.second;
                timerMinute = monster.GetComponent<Peach>().param.minute;
                timerHour = monster.GetComponent<Peach>().param.hour;
                break;
            case "Melon":
                if (monster == null)
                {
                    monster = GameObject.Find("Melon");
                }
                timerSecond = monster.GetComponent<Melon>().param.second;
                timerMinute = monster.GetComponent<Melon>().param.minute;
                timerHour = monster.GetComponent<Melon>().param.hour;
                break;
            case "Suika":
                if (monster == null)
                {
                    monster = GameObject.Find("Suika");
                }
                timerSecond = monster.GetComponent<Suika>().param.second;
                timerMinute = monster.GetComponent<Suika>().param.minute;
                timerHour = monster.GetComponent<Suika>().param.hour;
                break;
        }

    }

}

