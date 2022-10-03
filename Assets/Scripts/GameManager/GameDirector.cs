using UnityEngine;
using TMPro;
using Monster;

public class GameDirector : MonoBehaviour
{
    [SerializeField] int index = 10;
    [SerializeField] GameObject[] Coin_array;
    [SerializeField] GameObject Coin_Prefab;
    [SerializeField] GameObject Nasubi_Prefab;
    [SerializeField] GameObject Orange_Prefab;
    [SerializeField] GameObject Carot_Prefab;
    [SerializeField] GameObject Cabbage_Prefab;
    [SerializeField] GameObject Banana_Prefab;
    [SerializeField] GameObject Corn_Prefab;
    [SerializeField] GameObject Pieman_Prefab;
    [SerializeField] GameObject Peach_Prefab;
    [SerializeField] GameObject Melon_Prefab;
    [SerializeField] GameObject Suika_Prefab;
    [System.NonSerialized] public GameObject Lock_item1;
    [System.NonSerialized] public GameObject Lock_item2;
    [System.NonSerialized] public GameObject Lock_item3;
    [System.NonSerialized] public GameObject Lock_item4;
    TextMeshProUGUI coin_text, kansha_text;
    public TextMeshProUGUI complete_text;
    //BuyMonster buyMonster;
    public Transform parentTran;
    public int HoldCoin = 0;
    public monster_type pick_monster;
    [Header("感謝の涙")] public int kanshanonamida = 0;
    private Transform UiTran;
    private GameObject coin;
    private GameObject kansha;
    private GameObject MsScreen1, MsScreen2, MsScreen3, ItemScreen;
    private GameObject Nasubi, Orange, Carot, Cabbage, Banana, Corn, Pieman, Peach, Melon, Suika;
    private GameObject Notice_nasubi, Notice_orange, Notice_carot, Notice_cabbage, Notice_banana,
                       Notice_corn, Notice_pieman, Notice_peach, Notice_melon, Notice_suika;
 
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
    public void FarmComplete(monster_type type)
    {
        Debug.Log("育成 Complete");
        switch (type)
        {
            case monster_type.nasubi:  
                Debug.Log("nasubi Farm Complete");
                complete_text.text = "育成が終わったよ";
                HoldCoin += 3000;
                break;
            case monster_type.orange: 
                Debug.Log("orange Farm Complete");
                complete_text.text = "育成が終わったよ";
                HoldCoin += 3000;
                break;
            case monster_type.carot: 
                Debug.Log("carot Farm Complete");
                complete_text.text = "育成が終わったよ";
                HoldCoin += 3000;
                break;
            case monster_type.cabbage: 
                Debug.Log("cabbage Farm Complete");
                complete_text.text = "育成が終わったよ";
                HoldCoin += 3000;
                break;
            case monster_type.banana: 
                Debug.Log("banana Farm Complete");
                complete_text.text = "育成が終わったよ";
                HoldCoin += 3000;
                break;
            case monster_type.corn: 
                Debug.Log("corn Farm Complete");
                complete_text.text = "育成が終わったよ";
                HoldCoin += 3000;
                break;
            case monster_type.pieman: 
                Debug.Log("pieman Farm Complete");
                complete_text.text = "育成が終わったよ";
                HoldCoin += 3000;
                break;
            case monster_type.peach: 
                Debug.Log("peach Farm Complete");
                complete_text.text = "育成が終わったよ";
                HoldCoin += 3000;
                break;
            case monster_type.melon: 
                Debug.Log("melon Farm Complete");
                complete_text.text = "育成が終わったよ";
                HoldCoin += 3000;
                break;
            case monster_type.suika: 
                Debug.Log("suika Farm Complete");
                complete_text.text = "育成が終わったよ";
                HoldCoin += 3000;
                break;
        }
    }

    public void FarmItemButtonDown()
    {
        ItemScreen.SetActive(true);
    }

#pragma warning disable IDE0060 // 未使用のパラメーターを削除します
    private void Instantiate_Monster(GameObject monster, GameObject monster_prefab, GameObject notice_obj, string notice_obj_name)
#pragma warning restore IDE0060 // 未使用のパラメーターを削除します
    {
        MsScreen1.SetActive(false);
        monster = Instantiate(monster_prefab);
        SpriteRenderer Renderer = monster.GetComponent<SpriteRenderer>();
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
            pick_monster = monster_type.nasubi;
        else if (GameObject.Find("Notice_Orange") != null)
            pick_monster = monster_type.orange;
        else
            pick_monster = monster_type.num;

        switch (pick_monster)
        {
            case monster_type.nasubi: 
                Instantiate_Monster(Nasubi, Nasubi_Prefab, Notice_nasubi, "Notice_Nasubi");
                break;
            case monster_type.orange: 
                Instantiate_Monster(Orange, Orange_Prefab, Notice_orange, "Notice_Orange");
                break;
            case monster_type.carot:
                Instantiate_Monster(Carot, Carot_Prefab, Notice_carot, "Notice_Carot");
                break;
            case monster_type.cabbage:
                Instantiate_Monster(Cabbage, Cabbage_Prefab, Notice_cabbage, "Notice_Cabbage");
                break;
            case monster_type.banana:
                Instantiate_Monster(Banana, Banana_Prefab, Notice_banana, "Notice_Banana");
                break;
            case monster_type.corn:
                Instantiate_Monster(Corn, Corn_Prefab, Notice_corn, "Notice_Corn");
                break;
            case monster_type.pieman:
                Instantiate_Monster(Pieman, Pieman_Prefab, Notice_pieman, "Notice_Pieman");
                break;
            case monster_type.peach:
                Instantiate_Monster(Peach, Peach_Prefab, Notice_peach, "Notice_Peach");
                break;
            case monster_type.melon:
                Instantiate_Monster(Melon, Melon_Prefab, Notice_melon, "Notice_Melon");
                break;
            case monster_type.suika: 
                Instantiate_Monster(Suika, Suika_Prefab, Notice_suika, "Notice_Suika");
                break;
        }
    }

    public void BuyButtonDown(int num)
    {
        int bubbly_meat_price = 100;
        int sweat_juice_price = 200;
        int touch_light_price = 300;
        int talk_grass_price = 400;

        switch (num)
        {
            case 0:
                if (HoldCoin > bubbly_meat_price)
                {
                    HoldCoin -= bubbly_meat_price;
                    coin_text.text = HoldCoin.ToString();
                    Lock_item1.SetActive(false);
                }
                break;
            case 1:
                if (HoldCoin > sweat_juice_price)
                {
                    HoldCoin -= sweat_juice_price;
                    coin_text.text = HoldCoin.ToString();
                    Lock_item2.SetActive(false);
                }
                break;
            case 2:
                if (HoldCoin > touch_light_price)
                {
                    HoldCoin -= touch_light_price;
                    coin_text.text = HoldCoin.ToString();
                    Lock_item3.SetActive(false);
                }
                break;
            case 3:
                if (HoldCoin > talk_grass_price)
                {
                    HoldCoin -= talk_grass_price;
                    coin_text.text = HoldCoin.ToString();
                    Lock_item4.SetActive(false);
                }
                break;
        }
    }
}
