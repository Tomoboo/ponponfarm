using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterController : MonoBehaviour
{
    [SerializeField] protected  float rate = 2;
    [SerializeField] protected int dropprate = 1;
    [SerializeField] public float span = 0;
    [SerializeField] protected float item3speed;
    [SerializeField] protected float speed = 75.0f;
    [System.NonSerialized] public bool Ms1Complete_flg = false;
    [System.NonSerialized] public bool item1start_flg = false;
    [System.NonSerialized] public bool item2start_flg = false;
    [System.NonSerialized] public bool item3start_flg = false;
    [System.NonSerialized] public bool item4start_flg = false;
    [System.NonSerialized] public float oldSecond = 0;
    [System.NonSerialized] public float nowGauge = 100.0f;
    protected GameObject target;
    protected GameObject UICanvas;
    protected GameObject director;
    protected Vector3 targetpos;
    protected Vector3 currentpos;
    protected SpriteRenderer Renderer;
    protected bool Enepower_flg, isStop, isScale;
    protected float stoptimer = 0;
    protected float diagonaltimer = 0;
    protected float item1start = 0, item2start = 0, item3start = 0, item4start = 0;
    protected float decreasePoint = 0.05f;
    protected int num = -1;
    protected int direct;
    protected int limit =2;
    protected int kanshanonamida = 0;
    protected int flipCount = 0;
    public bool isTouch;
    public float second;
    public int minute, hour;
    //proteced int oldMinute = 0;
    //protected int oldHour = 0;
    //protected float passSecond = 0;
    //protected int passMinute = 0

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //ランダムシード値を時刻により初期化
        Random.InitState(System.DateTime.Now.Millisecond);
        isTouch = false;
        isStop = true;
        isScale = false;
        Enepower_flg = false;
        direct = Random.Range(-1, 3);
        item3speed = 0.1f;
        this.span = 8.0f;
        target = GameObject.Find("target");
        UICanvas = GameObject.Find("UICanvas");
        director = GameObject.Find("GameDirector");
        Renderer = GetComponent<SpriteRenderer>();
        targetpos = moveRandomPosition();
        currentpos = gameObject.transform.localPosition;
        Vector3 kero = new Vector3(50, 50, 1);
        kero.x = 60;
        kero.y = 60;
        kero.z = 1;
        this.transform.localScale = kero;
        Rigidbody2D rbody;
        gameObject.AddComponent<Rigidbody2D>();
        rbody = gameObject.GetComponent<Rigidbody2D>();
        //重力・回転無効化
        rbody.gravityScale = 0;
        rbody.freezeRotation = true;
    }
    ////////////////////////////育成時間////////////////////////////
    protected virtual void farmTime()
    {
        second += Time.deltaTime;
            if (second > 60f)
            {
                minute += 1;
                second = 0;
            }
            if (minute > 60)
            {
                hour++;
                minute = 0;
            }
            //育成時間に達したら
            if (second > this.span && Ms1Complete_flg == false)   //本来はsecond →　hour
            {
                Ms1Complete_flg = true;
                // モンスターごとに引数設定
                director.GetComponent<GameDirector>().FarmComplete(-1);
                // ランダム確率で感謝の涙GET
                if (Random.Range(0, 1000) < dropprate)
                    director.GetComponent<GameDirector>().kanshaGet();
            }
        
            else if (second > (this.span/2))
            {
                isScale = true;
                Vector3 kero = new Vector3(80, 80, 1);
                kero.x = 70;
                kero.y = 70;
                kero.z = 1;
                this.transform.localScale = kero;
            }
    }
    //////////////////////ストップ動作/////////////////////
    protected virtual void Stop()
    {
        if ((Random.Range(0, 1000.0f) < rate) &&
            (GameObject.FindWithTag("item1") == null) && (GameObject.FindWithTag("item2") == null) &&
           (GameObject.FindWithTag("item3") == null) && (GameObject.FindWithTag("item4") == null))
       { 
            isStop = true;
        }
        if (isStop == true)
        {
            stoptimer += Time.deltaTime;
                if (Renderer.flipX == false)
                {
                    flipCount -= 1;
                    if (flipCount < 0)
                        Renderer.flipX = true;
                }
                else
                {
                    flipCount += 1;
                    if (flipCount > 50)
                        Renderer.flipX = false;
                }
                if (stoptimer > limit)
                {
                    isStop = false;
                }
        }
    }
    /////////////////////歩行動作////////////////////
    protected virtual void Move()
    {
        //左右移動
        currentpos = gameObject.transform.localPosition;
        if (targetpos == currentpos)
            targetpos = moveRandomPosition();
        this.gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, targetpos, speed * Time.deltaTime);
        //print(this.gameObject.transform.localPosition);
    }

    protected virtual  Vector3 moveRandomPosition() // 目的地を生成、xとｙのポジションをランダムにを取得
    {
        Vector3 randomPosi; 
        if (isScale == false)
            randomPosi = new Vector3(Random.Range(-465, 465), Random.Range(-90, -500), 5);
        else
            randomPosi = new Vector3(Random.Range(-386, 386), Random.Range(-80, -400), 5);

        return randomPosi;
    }

    /////////////////////////Update is called once per frame///////////////////////////
    protected virtual void Update()
    {
        DetectCollider detectCollider = GetComponent<DetectCollider>();
        Enepower_flg = UICanvas.GetComponent<Info_Monsters>().Enepower_flg; // ※モンスター共通
        float spanPercent = this.span * decreasePoint; //元気ゲージの減速量

        farmTime();
        UseItem1();
        UseItem2();
        UseItem3();
        UseItem4();
        Stop();
        //元気ゲージが50％以上のみ育成速度を上昇
        if (Enepower_flg == true) 
        {
            second += Time.deltaTime * 1.002f;
        }

        //育成時間が10％進むにつれて元気ゲージを10％ずつ減少
        if (second - oldSecond > spanPercent)
        {
            nowGauge -= 10;
            oldSecond = second;
            if (nowGauge < 0)
                nowGauge = 0;
        }

        if ((detectCollider.item3_flg == false || detectCollider.item4_flg == false) && isStop == false  &&
           (GameObject.FindWithTag("item1") == null) && (GameObject.FindWithTag("item2") == null) &&
           (GameObject.FindWithTag("item3") == null) && (GameObject.FindWithTag("item4") == null))
        {
            // ストップタイマリセット
            stoptimer = 0;
            Move();
            //イラスト左右方向転換
                if (currentpos.x < targetpos.x)
                    Renderer.flipX = true;  //右向き
                else
                    Renderer.flipX = false;//左向き
            }
    }
    /// <summary>
    /// アイテム関連
    /// </summary>
    protected void UseItem1()
    {
        float speed = 0.005f;
        if (GameObject.FindWithTag("item1") != null)
        {
            GameObject item = GameObject.FindWithTag("item1");
            var itemPos = item.transform.position;
            var currenPos = gameObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindWithTag("item1").transform.position, speed);
            //イラスト左右方向転換
            if (currenPos.x < itemPos.x)
                Renderer.flipX = true;  //右向き
            else
                Renderer.flipX = false;//左向き
        }
        DetectCollider detectCollider = GetComponent<DetectCollider>();
        //アイテム１の効果適用
        if (detectCollider.item1_flg == true)
        {
            int limit = 30;
            float timer = 0;
                Debug.Log("Valid item1");
                if (item1start_flg == false)
                {
                    item1start = Time.time;
                    item1start_flg = true;
                }
                timer = Time.time - item1start;
                Debug.Log("アイテム①経過時間　" + timer);
                //経過時間になったら
                if (timer > limit)
                {
                    detectCollider.item1_flg = false;
                    Debug.Log("Invalid item1");
                }
            //育成スピード増加（アイテム1の効果）
            second += Time.deltaTime * 1.004f;
        }
    }

    protected void UseItem2()
    {
        float speed = 0.005f;
        if (GameObject.FindWithTag("item2") != null)
        {
            GameObject item = GameObject.FindWithTag("item2");
            var itemPos = item.transform.position;
            var currenPos = gameObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindWithTag("item2").transform.position, speed);
            //イラスト左右方向転換
            if (currenPos.x < itemPos.x)
                Renderer.flipX = true;  //右向き
            else
                Renderer.flipX = false;//左向き
        }
        DetectCollider detectCollider = GetComponent<DetectCollider>();
        //アイテム2の効果適用
        if (detectCollider.item2_flg == true)
        {
            float spanPercent = span * decreasePoint; //元気ゲージの減速量
            int limit = 30;
            float timer = 0;
            Debug.Log("Valid item2");
                if (item2start_flg == false)
                {
                    item2start = Time.time;
                    item2start_flg = true;
                }
                timer = Time.time - item2start;
                Debug.Log("アイテム②経過時間　" + timer);
                //経過時間になったら
                if (timer > limit)
                {
                    detectCollider.item2_flg = false;
                    Debug.Log("Invalid item2");
                }
            //育成時間が10％進むにつれて元気ゲージを増加（アイテム2の効果）
            if (second - oldSecond > spanPercent)
            {
                nowGauge += 11;
                oldSecond = second;
                if (nowGauge > 100)
                    nowGauge = 100;
            }
        }
    }

    protected void UseItem3()
    {
        float speed = 0.005f;
        if (GameObject.FindWithTag("item3") != null)
        {
            GameObject item = GameObject.FindWithTag("item3");
            var itemPos = item.transform.position;
            var currenPos = gameObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindWithTag("item3").transform.position, speed);
            //イラスト左右方向転換
            if (currenPos.x < itemPos.x)
                Renderer.flipX = true;  //右向き
            else
                Renderer.flipX = false;//左向き
        }
        DetectCollider detectCollider = GetComponent<DetectCollider>();
        //アイテム3の効果適用
        if (detectCollider.item3_flg == true)
        {
            int limit = 30;
            float timer = 0;
            target.GetComponent<MousePoint>().isTarget = true;
            Debug.Log("Valid item3");
                if (item3start_flg == false)
                {
                    item3start = Time.time;
                    item3start_flg = true;
                }
                timer = Time.time - item3start;
                Debug.Log("アイテム③経過時間　" + timer);
                //経過時間になったら
                if (timer > limit)
                {
                    target.GetComponent<MousePoint>().isTarget = false;
                    detectCollider.item3_flg = false;
                    Debug.Log("Invalid item3");
                }
            //アイテム3の効果    
            if (isTouch == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, item3speed);
            }
        }
    }

    protected void UseItem4()
    {
        float speed = 0.005f;
        if (GameObject.FindWithTag("item4") != null)
        {
            GameObject item = GameObject.FindWithTag("item4");
            var itemPos = item.transform.position;
            var currenPos = gameObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindWithTag("item4").transform.position, speed);
            //イラスト左右方向転換
            if (currenPos.x < itemPos.x)
                Renderer.flipX = true;  //右向き
            else
                Renderer.flipX = false;//左向き
        }
        DetectCollider detectCollider = GetComponent<DetectCollider>();
        //アイテム4の効果適用
        if (detectCollider.item4_flg == true)
        {
            GameObject walls;
            walls = UICanvas.transform.Find("GravityWalls").gameObject;
            int limit = 30;
            float timer = 0;
            Rigidbody2D rBody;
            walls.SetActive(true);
            Debug.Log("Valid item4");
            //アイテム4の効果
            rBody = gameObject.GetComponent<Rigidbody2D>();
            //重力無効化
            rBody.gravityScale = -0.005f;

            if (item4start_flg == false)
            {
                item4start = Time.time;
                item4start_flg = true;
            }
            timer = Time.time - item4start;
            Debug.Log("アイテム➃経過時間　" + timer);
            //経過時間になったら
            if (timer > limit)
            {
                detectCollider.item4_flg = false;
                walls.SetActive(false);
                Debug.Log("Invalid item4");
            }
        }
    }

}
