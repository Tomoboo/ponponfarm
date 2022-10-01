using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    [SerializeField] int index = 10;
    [SerializeField] GameObject[] Coin_array;
    [SerializeField] GameObject Coin_Prefab;
    [SerializeField] GameObject Nasubi_Prefab;
    [SerializeField] GameObject Orange_Prefab;
    [System.NonSerialized] public GameObject Lock_item1;
    [System.NonSerialized] public GameObject Lock_item2;
    [System.NonSerialized] public GameObject Lock_item3;
    [System.NonSerialized] public GameObject Lock_item4;
    TextMeshProUGUI coin_text;
    TextMeshProUGUI kansha_text;
    //BuyMonster buyMonster;
    public Transform parentTran;
    public int HoldCoin = 0;
    public int pick_monster = -1;
    [Header("感謝の涙")] public int kanshanonamida = 0;
    private Transform UiTran;
    private GameObject coin;
    private GameObject kansha;
    private GameObject MsScreen1;
    private GameObject MsScreen2;
    private GameObject MsScreen3;
    private GameObject ItemScreen;
    private GameObject Nasubi;
    private GameObject Notice_nasubi;
    private GameObject Orange;
    private GameObject Notice_orange;
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
        UiTran = GameObject.Find("UICanvas").transform;
        Lock_item1 = ItemScreen.transform.Find("item1_locked").gameObject;
        Lock_item2 = ItemScreen.transform.Find("item2_locked").gameObject;
        Lock_item3 = ItemScreen.transform.Find("item3_locked").gameObject;
        Lock_item4 = ItemScreen.transform.Find("item4_locked").gameObject;
        Coin_array = new GameObject[index];
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
        Debug.Log("感謝の涙ゲット");
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

    private void Coin_Get()
    {
        Vector3 scale = new(80, 80, 0);
        for (int i = 0; i < index; i++)
        {
            float x = 64.0f;//5 - Mathf.Sqrt(Mathf.Sqrt(Random.Range(0, 200)));
            float y = -370.0f;//1.0f - Mathf.Sqrt(Mathf.Sqrt(Random.Range(0, 200)));
            Coin_array[i] = Instantiate(Coin_Prefab, new Vector3(x, y, 0.0f), Quaternion.identity);
            Coin_array[i].transform.SetParent(UiTran);
            Coin_array[i].transform.localScale = scale;
        }
    }
    public void FarmComplete(int num)
    {
        Debug.Log("育成 Complete");
        switch (num)
        { 
            
            case 0:  //Nasubi
                Debug.Log("nasubi Farm Complete");
                //Coin_Get();
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
#pragma warning disable IDE0060 // 未使用のパラメーターを削除します
    private void Instantiate_Monster(GameObject monster, GameObject monster_prefab, GameObject notice_obj, string notice_obj_name)
#pragma warning restore IDE0060 // 未使用のパラメーターを削除します
    {
        MsScreen1.SetActive(false);
        monster = Instantiate(monster_prefab);
        SpriteRenderer Renderer =  monster.GetComponent<SpriteRenderer>();
        Renderer.sortingOrder = 1;
        monster.layer = 5;
        monster.name = monster_prefab.name;
        monster.transform.SetParent(parentTran);
        monster.transform.localPosition = new Vector3(30, -111, 0);
        monster.transform.localScale = new Vector3(1, 1, 1);
        monster.transform.SetAsLastSibling();
        notice_obj = GameObject.Find(notice_obj_name);
        Destroy(notice_obj);
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
                Instantiate_Monster(Nasubi, Nasubi_Prefab, Notice_nasubi, "Notice_Nasubi");
                break;
            case 1: //Orange
                Instantiate_Monster(Orange, Orange_Prefab, Notice_orange, "Notice_Orange");
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
