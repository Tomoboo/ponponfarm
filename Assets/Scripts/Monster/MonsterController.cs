using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Item_farm;


namespace Monster
{
    public class MonsterController : MonoBehaviour
    {
        
        [SerializeField] protected int Moverate = 8;
        [SerializeField] protected float Stoprate = 4;
        [SerializeField] protected float Sleeprate = 2;
        [SerializeField] protected int dropprate = 2;
        [SerializeField] public float span = 0;
        [SerializeField] protected int limitStop = 2;
        [SerializeField] protected int limitSleep = 10;
        [SerializeField] protected float speed = 75.0f;
        [System.NonSerialized] public bool isStart_bubbly = false;
        [System.NonSerialized] public bool isStart_sweat = false;
        [System.NonSerialized] public bool isStart_touch = false;
        [System.NonSerialized] public bool isStart_talk = false;
        [System.NonSerialized] public float oldSecond = 0;
        [System.NonSerialized] public float nowGauge = 100.0f;
        protected GameObject target;
        protected GameObject UICanvas;
        protected GameObject director;
        protected Vector3 targetpos;
        protected Vector3 currentpos;
        protected Vector3 prev_currenpos;
        protected SpriteRenderer Renderer;
        protected DetectCollider detectCollider;
        protected bool isStop, isSleep, isScale, isMove, isTarget_paramChange, Complete_flg, Enepower_flg;
        protected float stopTimer = 0;
        protected float sleepTimer = 0;
        protected float diagonaltimer = 0;
        protected float item1start = 0, item2start = 0, item3start = 0, item4start = 0;
        protected int num = -1;
        protected int monster_num = -1;
        protected int col_size = 0;
        protected int kanshanonamida = 0;
        protected int flipCount = 0;
        protected int currentpos_count;
        public bool isTouch;
        public float second;
        public int minute, hour;
        public float decreasePoint = 0.05f;
        Rigidbody2D rbody;
        //proteced int oldMinute = 0;
        //protected int oldHour = 0;
        //protected float passSecond = 0;
        //protected int passMinute = 0

        protected virtual void Start()
        {
            span = 8.0f;
            col_size = 6;
            monster_num = -1;
            Stoprate = 100;
            isTouch = false;
            isStop = true;
            isSleep = false;
            isScale = false;
            isMove = false;
            isTarget_paramChange = false;
            Enepower_flg = false;
            Complete_flg = false;
            target = (GameObject)Resources.Load("target");
            UICanvas = GameObject.Find("UICanvas");
            director = GameObject.Find("GameDirector");
            Renderer = GetComponent<SpriteRenderer>();
            targetpos = MoveRandomPosition(-465, -165, -500, -205, -386, -186, -400, -200);
            currentpos = gameObject.transform.localPosition;
            Vector3 kero = new(50, 50, 1);
            kero.x = 60;
            kero.y = 60;
            kero.z = 1;
            transform.localScale = kero;
            gameObject.AddComponent<Rigidbody2D>();
            rbody = gameObject.GetComponent<Rigidbody2D>();
            //重力無効化
            rbody.gravityScale = 0;
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

            else if (second > (span / 2))
            {
                isScale = true;
                Vector3 kero = new(80, 80, 1);
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
        protected bool GetJudge(bool key, bool value, float rate)
        {
            if ((Random.Range(0, 100) < rate) && (key == false)&& detectCollider.isTouch == false && isMove == false &&
                (GameObject.FindWithTag("item1") == null) && (GameObject.FindWithTag("item2") == null) &&
               (GameObject.FindWithTag("item3") == null) && (GameObject.FindWithTag("item4") == null))
            {
                value = true;
            }
            return value;
        }

        protected void Stop()
        {
            Stoprate = 20;
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
            currentpos = gameObject.transform.localPosition;
            currentpos.x = Mathf.Round(currentpos.x);
            currentpos.y = Mathf.Round(currentpos.y);
            currentpos.z = Mathf.Round(currentpos.z);
            gameObject.transform.localPosition = currentpos;
            if (targetpos == currentpos || currentpos_count > 1000)
            {
                Debug.Log("Update targetpos");
                if (isTarget_paramChange == false)
                {
                    //(x_min_isScale_false, x_max_isScale_false) , (y_min_isScale_false,  y_max_isScale_false),( x_min_isScale_true,  x_max_isScale_true), (y_min_isScale_true, y_max_isScale_true)
                    targetpos = MoveRandomPosition(-465, -165, -500, -205, -386, -186, -400, -200);
                }
                else
                {
                    //(x_min_isScale_false, x_max_isScale_false) , (y_min_isScale_false,  y_max_isScale_false),( x_min_isScale_true,  x_max_isScale_true), (y_min_isScale_true, y_max_isScale_true)
                    targetpos = MoveRandomPosition(165, 465, -205, -90, 186, 386, -200, -80);
                }
                currentpos_count = 0;
                isMove = false;
                if (isTarget_paramChange == false)
                    isTarget_paramChange = true;
                else
                    isTarget_paramChange = false;
            }
            else if (prev_currenpos == currentpos)
            {
                currentpos_count += 1;
                isMove = true;
            }
            else
            {
                isMove = true;
            }
            gameObject.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, targetpos, speed * Time.deltaTime);
            prev_currenpos = currentpos;
        }

        protected Vector3 MoveRandomPosition(int x_min_isScale_false, int x_max_isScale_false, int y_min_isScale_false, int y_max_isScale_false,
                                                                  int x_min_isScale_true, int x_max_isScale_true, int y_min_isScale_true, int y_max_isScale_true)
        {
            // 目的地を生成、xとｙのポジションをランダムにを取得
            Vector3 randomPosi;
            float _x_isScale_false = Random.Range(x_min_isScale_false, x_max_isScale_false);// >> Random.Range(-465, -165), Random.Range(165,465)
            float _y_isScale_false = Random.Range(y_min_isScale_false, y_max_isScale_false);// >>  Random.Range(-500, -205), Random.Range(-205, -90)
            float _x_isScale_true = Random.Range(x_min_isScale_true, x_max_isScale_true);// >>  Random.Range(-386, -186), Random.Range(186,386)
            float _y_isScale_true = Random.Range(y_min_isScale_true, y_max_isScale_true);// >> Random.Range(-400, -200), Random.Range(-200, -80)
            if (isScale == false)
                randomPosi = new Vector3(_x_isScale_false, _y_isScale_false, 5);
            else
                randomPosi = new Vector3(_x_isScale_true, _y_isScale_true, 5);
            Debug.Log("x,y, z =" + randomPosi);

            return randomPosi;
        }

        /////////////////////////Update is called once per frame///////////////////////////
        protected virtual void Update()
        {
            //ランダムシード値を時刻により初期化
            Random.InitState(System.DateTime.Now.Millisecond);
            detectCollider = GetComponent<DetectCollider>();
            Enepower_flg = UICanvas.GetComponent<Info_Monsters>().Enepower_flg; // ※モンスター共通
            float spanPercent = span * decreasePoint; //元気ゲージの減速量
            ItemController itemController = GetComponent<ItemController>();

            FarmTime();
            itemController.Use_item(_item_type.bubbly_meat);
            itemController.Use_item(_item_type.sweat_juice);
            itemController.Use_item(_item_type.touch_light);
            //UseItem4();
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

            if (detectCollider.isTouch == false && isStop == false && isSleep == false &&
               (GameObject.FindWithTag("item1") == null) && (GameObject.FindWithTag("item2") == null) &&
               (GameObject.FindWithTag("item3") == null) && (GameObject.FindWithTag("item4") == null))
            {
                // stopTimer & sleepTimer Reset
                stopTimer = 0;
                sleepTimer = 0;
                if (Random.Range(0, 100) < Moverate && isMove == false)
                    Move();
                else if (isMove == true)
                    Move();
                //イラスト左右方向転換
                if (currentpos.x < targetpos.x)
                {
                    Renderer.flipX = true;  //右向き
                }
                else
                {
                    Renderer.flipX = false;//左向き
                }

            }
        }

        public void Chase_Item(string tag_name)
        {
            float speed = 0.0005f;
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
       /* protected void UseItem1()
        {
            Chase_Item("item1");
            DetectCollider detectCollider = GetComponent<DetectCollider>();
            //アイテム１の効果適用
            if (detectCollider.isBubbly == true)
            {
                int limit = 30;
                Debug.Log("Valid item1");
                if (isStart_bubbly == false)
                {
                    item1start = Time.time;
                    isStart_bubbly = true;
                }
                float timer = Time.time - item1start;
                Debug.Log("アイテム①経過時間　" + timer);
                //経過時間になったら
                if (timer > limit)
                {
                    detectCollider.isBubbly = false;
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
            if (detectCollider.isSweat == true)
            {
                float spanPercent = span * decreasePoint; //元気ゲージの減速量
                int limit = 30;
                Debug.Log("Valid item2");
                if (isStart_sweat == false)
                {
                    item2start = Time.time;
                    isStart_sweat = true;
                }
                float timer = Time.time - item2start;
                Debug.Log("アイテム②経過時間　" + timer);
                //経過時間になったら
                if (timer > limit)
                {
                    detectCollider.isSweat = false;
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
            if (detectCollider.isTouch == true)
            {
                int limit = 30;
                GameObject target_obj = Instantiate(target);
                target.GetComponent<MousePoint>().isTarget = true;
                Debug.Log("Valid item3");
                if (isStart_touch == false)
                {
                    item3start = Time.time;
                    isStart_touch = true;
                }
                float timer = Time.time - item3start;
                Debug.Log("アイテム③経過時間　" + timer);
                //経過時間になったら
                if (timer > limit)
                {
                    target.GetComponent<MousePoint>().isTarget = false;
                    detectCollider.isTouch = false;
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
            if (detectCollider.isTalk == true)
            {
                GameObject walls;
                walls = UICanvas.transform.Find("GravityWalls").gameObject;
                int limit = 30;
                Rigidbody2D rBody;
                walls.SetActive(true);
                Debug.Log("Valid item4");
                //アイテム4の効果
                rBody = gameObject.GetComponent<Rigidbody2D>();

                if (isStart_talk == false)
                {
                    item4start = Time.time;
                    isStart_talk = true;
                }
                float timer = Time.time - item4start;
                Debug.Log("アイテム➃経過時間　" + timer);
                //経過時間になったら
                if (timer > limit)
                {
                    detectCollider.isTalk = false;
                    walls.SetActive(false);
                    Debug.Log("Invalid item4");
                }
            }
        }*/

    }
}

