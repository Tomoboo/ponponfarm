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
    public float second;
    public int minute;
    public int hour;

    

    protected virtual void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Judge_monster (string name)
    {
        switch (name)
        {
            case "Nasubi":
                Debug.Log("Nasubi selected");
                monster  = GameObject.Find("Nasubi");
                second = monster.GetComponent<Nasubi>().second;
                minute = monster.GetComponent<Nasubi>().minute;
                hour = monster.GetComponent<Nasubi>().hour;
                break;
            case "Orange":
                Debug.Log("Orange selected");
                monster  = GameObject.Find("Orange");
                second = monster.GetComponent<Orange>().second;
                minute = monster.GetComponent<Orange>().minute;
                hour = monster.GetComponent<Orange>().hour;
                break;
            case "Carot":
                Debug.Log("Carot selected");
                break;
            case "Cabbage":
                Debug.Log("Cabbage selected");
                break;
            case "Banana":
                Debug.Log("Banana selected");
                break;
        }
        
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if(GameObject.FindWithTag("Monster")){
                Judge_monster(monsterPrefab.name);
                timerText.text =
                    hour.ToString("00") + "時間" + minute.ToString("00") + "分" +
                    ((int)second).ToString("00") + "秒";
            }
    }
}

