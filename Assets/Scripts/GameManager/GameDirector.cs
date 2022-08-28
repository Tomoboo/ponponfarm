using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameDirector : MonoBehaviour
{
    [SerializeField] GameObject NasubiPrefab;
    [SerializeField] GameObject OrangePrefab;
    [System.NonSerialized] public GameObject Lock_item1;
    [System.NonSerialized] public GameObject Lock_item2;
    [System.NonSerialized] public GameObject Lock_item3;
    [System.NonSerialized] public GameObject Lock_item4;
    TextMeshProUGUI coin_text;
    TextMeshProUGUI kansha_text;
    //BuyMonster buyMonster;
    private GameObject coin;
    private GameObject kansha;
    private GameObject Ms1;
    private GameObject MsScreen1;
    private GameObject MsScreen2;
    private GameObject MsScreen3;
    private GameObject ItemScreen;
    public Transform parentTran;
    public int HoldCoin = 0;
    public int pick_monster = -1;
    [Header("感謝の涙")] public int kanshanonamida = 0;

    void Start()
    {
        coin = GameObject.Find("StatusBar_Group_Light_GrayButton").transform.Find("Status_Coin").gameObject;
        kansha = GameObject.Find("StatusBar_Group_Light_GrayButton").transform.Find("Status_Kansha").gameObject;
        coin_text = coin.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        kansha_text = kansha.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        MsScreen1 = GameObject.Find("UICanvas").transform.Find("MonsterScreen").gameObject;
        MsScreen2 = GameObject.Find("UICanvas").transform.Find("MonsterScreen – 1").gameObject;
        MsScreen3 = GameObject.Find("UICanvas").transform.Find("MonsterScreen – 2").gameObject;
        ItemScreen = GameObject.Find("UICanvas").transform.Find("FarmItemScene").gameObject;
        Lock_item1 = ItemScreen.transform.Find("item1_locked").gameObject;
        Lock_item2 = ItemScreen.transform.Find("item2_locked").gameObject;
        Lock_item3 = ItemScreen.transform.Find("item3_locked").gameObject;
        Lock_item4 = ItemScreen.transform.Find("item4_locked").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // CompleteButton = Nasubi.transform.Find("Complete").gameObject;
        coin_text.text = HoldCoin.ToString();
        kansha_text.text = kanshanonamida.ToString();
        Paynow();
    }

    public void kanshaGet()
    {
        kanshanonamida += 1;
    }

    public void CancelButtonDown()
    {
        ItemScreen.SetActive(false);
    }

    public void MonsterButtonDown()
    {
        MsScreen1.SetActive(true);
    }

    public void FarmComplete(int num)
    {
        Debug.Log("育成 Complete");
        switch (num)
        { 
           
            case 0:  //Nasubi
                Debug.Log("nasubi Farm Complete");
                HoldCoin += 3000;
                break;
            case 1: //Orange
                Debug.Log("orange Farm Complete");
                HoldCoin += 3000;
                break;
        }
    }

    public void FarmItemButtonDown()
    {
        ItemScreen.SetActive(true);
    }

    public void CompleteButtonDown()
    {
        HoldCoin += 1000;
        coin_text.text = HoldCoin.ToString();
        Debug.Log("CoinGet");
    }

    public void Paynow()
    {
        if (GameObject.Find("Notice_Nasubi") != null)
            pick_monster = 0;
        else if (GameObject.Find("Notice_Orange") != null)
            pick_monster = 1;
        else
            pick_monster = -1;

        switch (pick_monster)
        {
            case 0: //Nasubi
                MsScreen1.SetActive(false);
                GameObject Nasubi = Instantiate(NasubiPrefab);
                Nasubi.name = NasubiPrefab.name;
                Nasubi.transform.SetParent(parentTran);
                Nasubi.transform.localPosition = new Vector3(30, -111, 0);
                Nasubi.transform.localScale = new Vector3(1, 1, 1);
                Nasubi.transform.SetAsLastSibling();
                GameObject Notice_nasubi = GameObject.Find("Notice_Nasubi");
                Destroy(Notice_nasubi);
                break;
            case 1: //Orange
                MsScreen1.SetActive(false);
                GameObject Orange = Instantiate(OrangePrefab);
                Orange.name = OrangePrefab.name;
                Orange.transform.SetParent(parentTran);
                Orange.transform.localPosition = new Vector3(30, -111, 0);
                Orange.transform.localScale = new Vector3(1, 1, 1);
                Orange.transform.SetAsLastSibling();
                GameObject Notice_orange = GameObject.Find("Notice_Orange");
                Destroy(Notice_orange);
                break;
        }
    }

    public void BuyButtonDown(int num)
    {
        int price1 = 100;
        int price2 = 200;
        int price3 = 300;
        int price4 = 400;

        switch (num)
        {
            case 0:
                if (HoldCoin > price1)
                {
                    HoldCoin -= price1;
                    coin_text.text = HoldCoin.ToString();
                    Lock_item1.SetActive(false);
                }
                break;
            case 1:
                if (HoldCoin > price2) 
                { 
                    HoldCoin -= price2;
                    coin_text.text = HoldCoin.ToString();
                    Lock_item2.SetActive(false);
                }
                break;
            case 2:
                if (HoldCoin > price3)
                {
                    HoldCoin -= price3;
                    coin_text.text = HoldCoin.ToString();
                    Lock_item3.SetActive(false);
                }
                break;
            case 3:
                if (HoldCoin > price4)
                {
                    HoldCoin -= price4;
                    coin_text.text = HoldCoin.ToString();
                    Lock_item4.SetActive(false);
                }
                break;
        }
    }
}
