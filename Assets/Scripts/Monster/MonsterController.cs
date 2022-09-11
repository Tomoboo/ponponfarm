using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterController : MonoBehaviour
{ 

    [SerializeField] protected float Stoprate = 4;
    [SerializeField] protected float Sleeprate = 2;
    [SerializeField] protected int dropprate = 2;
    [SerializeField] public float span = 0;
    [SerializeField] protected int limitStop = 2;
    [SerializeField] protected int limitSleep = 10;
    [SerializeField] protected float item3speed;
    [SerializeField] protected float speed = 75.0f;
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
    protected bool  isStop, isSleep, isScale,  Complete_flg, isMove, Enepower_flg;
    protected float stopTimer = 0;
    protected float sleepTimer = 0;
    protected float diagonaltimer = 0;
    protected float item1start = 0, item2start = 0, item3start = 0, item4start = 0;
    protected float decreasePoint = 0.05f;
    protected int num = -1;
    protected int monster_num = -1;
    protected int col_size = 0;
    protected int kanshanonamida = 0;
    protected int flipCount = 0;
    protected int currentpos_count;
    public bool isTouch;
    public float second;
    public int minute, hour;
    //proteced int oldMinute = 0;
    //protected int oldHour = 0;
    //protected float passSecond = 0;
    //protected int passMinute = 0

    protected virtual void Start()
    {
        isTouch = false;
        isStop = true;
        isSleep = false;
        isScale = false;
        isMove = false;
        Enepower_flg = false;
        Complete_flg = false;
        item3speed = 0.1f;
        col_size = 6;
        monster_num = -1;
        span = 8.0f;
        target = (GameObject)Resources.Load("target");
        UICanvas = GameObject.Find("UICanvas");
        director = GameObject.Find("GameDirector");
        Renderer = GetComponent<SpriteRenderer>();
        targetpos = MoveRandomPosition();
        currentpos = gameObject.transform.localPosition;
        Vector3 kero = new (50, 50, 1);
        kero.x = 60;
        kero.y = 60;
        kero.z = 1;
        transform.localScale = kero;
        Rigidbody2D rbody;
        gameObject.AddComponent<Rigidbody2D>();
        rbody = gameObject.GetComponent<Rigidbody2D>();
        //重力・回転無効化
        rbody.gravityScale = 0;
        rbody.freezeRotation = true;

    }

    ////////////////////////////育成時間////////////////////////////
    protected void FarmTime()
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
            if (second > span && Complete_flg == false)   //本来はsecond →　hour
            {
                Complete_flg = true;
                // モンスターごとに引数設定
                director.GetComponent<GameDirector>().FarmComplete(monster_num);
                // ランダム確率で感謝の涙GET
                if (Random.Range(0, 60) < dropprate)
                    director.GetComponent<GameDirector>().kanshaGet();
            }
        
            else if (second > (span/2))
            {
                isScale = true;
                Vector3 kero = new (80, 80, 1);
                kero.x = 80;
                kero.y = 80;
                kero.z = 1;
                this.transform.localScale = kero;
                var col = gameObject.GetComponent<CapsuleCollider2D>();
                col.size = new Vector3(col_size, col_size, col_size);
        }
    }

    /// <summary>
    /// Now Check >> isStop or isSleep 
    /// </summary>
    /// <param name="key">条件</param>
    /// <param name="value">止まる or 寝る</param>
    /// <param name="rate">止まる・寝る確率</param>
    /// <returns></returns>
    protected  bool GetJudge(bool key, bool value, float rate)
    {
        // モンスターが止まっている時
        if ((Random.Range(0, 100) < rate) && (key == false) && isMove == false &&
            (GameObject.FindWithTag("item1") == null) && (GameObject.FindWithTag("item2") == null) &&
           (GameObject.FindWithTag("item3") == null) && (GameObject.FindWithTag("item4") == null))
        {
            value = true;
        }
        return value;
    }

    protected void Stop()
    {
        isStop = GetJudge(isSleep, isStop, Stoprate);
        if (isStop == true)
        {
            Debug.Log("モンスタが止まっている" + stopTimer);
            stopTimer += Time.deltaTime;
            if (stopTimer > limitStop)
                isStop = false;
        }
    }
    protected void Sleep()
    {
        isSleep = GetJudge(isStop, isSleep, Sleeprate);
        if (isSleep == true)
        {
            Debug.Log("モンスタが寝ている" + sleepTimer);
            sleepTimer += Time.deltaTime;
            if (sleepTimer > limitSleep)
                isSleep = false;
        }
    }



    /// <summary>
    /// 歩行動作
    /// </summary>
    protected void Move()
    {
        //左右移動
        var prev_currenpos = currentpos;
        currentpos = gameObject.transform.localPosition;
        currentpos.x = Mathf.Round(currentpos.x);
        currentpos.y = Mathf.Round(currentpos.y);
        currentpos.z = Mathf.Round(currentpos.z);
        gameObject.transform.localPosition = currentpos;
        if (targetpos == currentpos || currentpos_count > 100)
        {
            Debug.Log("Update targetpos");
            targetpos = MoveRandomPosition();
            currentpos_count = 0;
            isMove = false;
        }
        else if (prev_currenpos == currentpos)
        {
            currentpos_count += 1;
            isMove = true;
        }
        else
            isMove = true;
        gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, targetpos, speed * Time.deltaTime);
    }

    protected  Vector3 MoveRandomPosition() // 目的地を生成、xとｙのポジションをランダムにを取得
    {
        Vector3 randomPosi;
        if (isScale == false)
            randomPosi = new Vector3(Random.Range(-465 ,465), Random.Range(-500, -90), 5);
        else
            randomPosi = new Vector3(Random.Range(-386, 386), Random.Range(-400, -80), 5);

        return randomPosi;
    }

    /////////////////////////Update is called once per frame///////////////////////////
    protected virtual void Update()
    {
        //ランダムシード値を時刻により初期化
        Random.InitState(System.DateTime.Now.Millisecond);
        DetectCollider detectCollider = GetComponent<DetectCollider>();
        Enepower_flg = UICanvas.GetComponent<Info_Monsters>().Enepower_flg; // ※モンスター共通
        float spanPercent = span * decreasePoint; //元気ゲージの減速量

        FarmTime();
        UseItem1();
        UseItem2();
        UseItem3();
        UseItem4();
        Stop();
        Sleep();

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
         
        if ((detectCollider.item3_flg == false || detectCollider.item4_flg == false) && isStop == false && isSleep == false &&
           (GameObject.FindWithTag("item1") == null) && (GameObject.FindWithTag("item2") == null) &&
           (GameObject.FindWithTag("item3") == null) && (GameObject.FindWithTag("item4") == null))
        {
            // stopTimer & sleepTimer Reset
            stopTimer = 0;
            sleepTimer = 0;
            Move();
            //イラスト左右方向転換
           if (currentpos.x < targetpos.x)
                Renderer.flipX = true;  //右向き
           else
                Renderer.flipX = false;//左向き
            }
    }

    protected void Chase_Item(string tag_name)
    {
        float speed = 0.005f;
        if (GameObject.FindWithTag(tag_name) != null)
        {
            GameObject item = GameObject.FindWithTag(tag_name);
            var itemPos = item.transform.position;
            var currenPos = gameObject.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, GameObject.FindWithTag(tag_name).transform.position, speed);
            //イラスト左右方向転換
            if (currenPos.x < itemPos.x)
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
        Chase_Item("item1");
        DetectCollider detectCollider = GetComponent<DetectCollider>();
        //アイテム１の効果適用
        if (detectCollider.item1_flg == true)
        {
            int limit = 30;
            Debug.Log("Valid item1");
                if (item1start_flg == false)
                {
                    item1start = Time.time;
                    item1start_flg = true;
                }
            float timer = Time.time - item1start;
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
        Chase_Item("item2");
        DetectCollider detectCollider = GetComponent<DetectCollider>();
        //アイテム2の効果適用
        if (detectCollider.item2_flg == true)
        {
            float spanPercent = span * decreasePoint; //元気ゲージの減速量
            int limit = 30;
            Debug.Log("Valid item2");
                if (item2start_flg == false)
                {
                    item2start = Time.time;
                    item2start_flg = true;
                }
            float timer = Time.time - item2start;
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
        Chase_Item("item3");
        DetectCollider detectCollider = GetComponent<DetectCollider>();
        //アイテム3の効果適用
        if (detectCollider.item3_flg == true)
        {
            int limit = 30;
            GameObject target_obj = Instantiate(target);
            target.GetComponent<MousePoint>().isTarget = true;
            Debug.Log("Valid item3");
                if (item3start_flg == false)
                {
                    item3start = Time.time;
                    item3start_flg = true;
                }
            float timer = Time.time - item3start;
            Debug.Log("アイテム③経過時間　" + timer);
                //経過時間になったら
                if (timer > limit)
                {
                target.GetComponent<MousePoint>().isTarget = false;
                detectCollider.item3_flg = false;
                Destroy(target_obj);
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
        Chase_Item("item4");
        DetectCollider detectCollider = GetComponent<DetectCollider>();
        //アイテム4の効果適用
        if (detectCollider.item4_flg == true)
        {
            GameObject walls;
            walls = UICanvas.transform.Find("GravityWalls").gameObject;
            int limit = 30;
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
            float timer = Time.time - item4start;
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
