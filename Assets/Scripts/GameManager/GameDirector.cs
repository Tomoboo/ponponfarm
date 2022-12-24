using UnityEngine;
using TMPro;
using Monster;

public class GameDirector : MonoBehaviour
{
    [SerializeField] private AudioDirector audioDirector;
    [SerializeField] private FarmDirector farmDirector;
    [SerializeField] private CoinController coinController;
    [SerializeField] private int holdCoin;
    public int HoldCoin
    {
        get { return this.holdCoin; }
        set { this.holdCoin = value; }
    }
    [Header("感謝の涙")] public int kanshanonamida = 0;
    public GameObject MsScreen;
    [SerializeField] private GameObject MsScreen1;
    [SerializeField] private GameObject MsScreen2;
    [SerializeField] private GameObject MsScreen3;
    [SerializeField] private GameObject ItemScreen;
    [SerializeField] private GameObject MusicPlayListScreen;
    [SerializeField] private GameObject TutorialScreen;
    public TextMeshProUGUI coin_text;
    private TextMeshProUGUI kansha_text;
    private Transform UiTran;
    private GameObject coin;
    private GameObject kansha;
    void Start()
    {
        coin = GameObject.Find("StatusBar_Group_Light_GrayButton").transform.Find("Status_Coin").gameObject;
        coin_text = coin.transform.Find("Text").GetComponent<TextMeshProUGUI>();

    }
    // Update is called once per frame
    void Update()
    {
        coin_text.text = HoldCoin.ToString();
        //kansha_text.text = kanshanonamida.ToString();
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
        MsScreen.SetActive(true);
        MsScreen1.SetActive(true);
    }
    public void FarmItemButtonDown()
    {
        ItemScreen.SetActive(true);
    }
    public void MusicPlayerButton()
    {
        MusicPlayListScreen.SetActive(true);
    }
    public void TutorialButton()
    {
        TutorialScreen.SetActive(true);
    }
    public void IsStartCoinAnimation()
    {
        coinController.isStartCoinAnimation = true;
        coinController.rt = 0;
        audioDirector.CoinSound();
    }
}
