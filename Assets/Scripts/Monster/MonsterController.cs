using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UniRx;
using UnityEngine.Advertisements;
using Item_farm;
using MonsterState;

/// <summary>
/// 元気度50％以下の対応は関数呼び出し
/// </summary>
namespace Monster
{
    public class MonsterController : MonoBehaviour
    {
        protected static bool isPop_info { get; set; } = false;
        protected const int MinLane = -2;
        protected const int MaxLane = 2;
        protected const float LaneWidth = 1.0f;
        protected int targetLane;
        public float speedX;
        public float speedY;
        public float accelerationX;
        protected monster_type type;
        [SerializeField] protected Sprite normalImage;
        [SerializeField] protected Sprite cryImage;
        public Sprite CryImage
        {
            get { return cryImage; }
            protected set { cryImage = value; }
        }
        [SerializeField] protected Sprite sleepImage;
        [SerializeField] protected int size = 4;
        [SerializeField] protected float span;
        public float Span
        {
            get { return this.span; }
            set { this.span = value; }
        }
        [SerializeField] protected int moveRate = 8;
        [SerializeField] protected float stopRate = 4;
        [SerializeField] protected float sleepRate = 2;
        [SerializeField] protected int dropRate = 2;
        [SerializeField] protected int limitStop = 2;
        [SerializeField] protected int limitSleep = 10;
        [SerializeField] protected int limitMove = 10;
        [SerializeField] protected float speed = 0;
        public float old_sumTime { get; set; } = 0;
        public bool isTouchTarget { get; set; } = false;
        public float decreasePoint { get; set; } = 1.0f;
        public bool isComplete { get; protected set; } = false;
        public SpriteRenderer spriteRenderer { get; set; }
        protected bool isScale, isTarget_paramChange;
        protected GameObject target;
        protected GameObject UICanvas;
        protected GameObject gameDirector;
        protected GameObject farmDirector;
        protected Vector3 targetPos;
        protected Vector3 currentPos;
        protected Vector3 moveDirection = Vector3.zero;
        protected Vector3 prevCurrentPos;
        protected FarmDirector _farmDirector;
        protected DetectCollider detectCollider;
        protected ItemController itemController;
        protected AdController adController;
        protected float stopTimer = 0;
        protected float sleepTimer = 0;
        protected float moveTimer = 0;
        protected int num = -1;
        protected int monsterNum = -1;
        protected int flipCount = 0;
        protected int currentPosCount;
        protected Rigidbody2D rbody;
        protected string prevStateName;
        public struct Parameter
        {
            public float nowGauge { get; set; }
            public float sumTime { get; set; }
            public float second { get; set; }
            public int minute { get; set; }
            public int hour { get; set; }
        }
        public Parameter param;
        public enum State
        {
            STOP,
            SLEEP,
            MOVE,
            CHASE,
        }
        public State nowState { get; set; }
        //ステート
        public StateProcessor StateProcessor { get; set; } = new StateProcessor();
        public MonsterStateNormal StateNormal { get; set; } = new MonsterStateNormal();
        public MonsterStateBubbly StateBubbly { get; set; } = new MonsterStateBubbly();
        public MonsterStateSweat StateSweat { get; set; } = new MonsterStateSweat();
        public MonsterStateTouch StateTouch { get; set; } = new MonsterStateTouch();
        public MonsterStateTalk StateTalk { get; set; } = new MonsterStateTalk();

        protected void Awake()
        {
            //ステートの初期化
            StateProcessor.State.Value = StateNormal;
            StateNormal.execAction = Normal;
            StateBubbly.execAction = Bubbly;
            StateSweat.execAction = Sweat;
            StateTouch.execAction = Touch;
            StateTalk.execAction = Talk;
            nowState = State.STOP;

            isScale = false;
            isTarget_paramChange = false;
            currentPos = gameObject.transform.localPosition;
            gameObject.AddComponent<Rigidbody2D>();
            rbody = gameObject.GetComponent<Rigidbody2D>();
            //重力無効化
            rbody.gravityScale = 0;
            // 回転固定
            rbody.freezeRotation = true;

            param = new Parameter();
            param.nowGauge = 100.0f;
        }

        protected virtual void Start()
        {
            Vector3 kero = new(50, 50, 1);
            transform.localScale = kero;
            var col = gameObject.GetComponent<CapsuleCollider2D>();
            col.size = new Vector3(size, size, size);
            type = monster_type.num;
            monsterNum = -1;
            target = (GameObject)Resources.Load("target");
            UICanvas = GameObject.FindWithTag("UICanvas");
            gameDirector = GameObject.FindWithTag("GameDirector");
            farmDirector = GameObject.FindWithTag("FarmDirector");
            _farmDirector = farmDirector.GetComponent<FarmDirector>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            itemController = GetComponent<ItemController>();
            adController = GameObject.FindWithTag("AdController").GetComponent<AdController>();
        }
        /////////////////////////Update is called once per frame///////////////////////////
        protected virtual void Update()
        {
            //ランダムシード値を時刻により初期化
            Random.InitState(System.DateTime.Now.Millisecond);
            float spanPercent = decreasePoint; //元気ゲージの減速量

            //ステートの値が変更されたら実行処理を行うようにする
            StateProcessor.State
                .Where(_ => StateProcessor.State.Value.GetStateName() != prevStateName)
                .Subscribe(_ =>
                {
                    Debug.Log("Now State:" + StateProcessor.State.Value.GetStateName());
                    prevStateName = StateProcessor.State.Value.GetStateName();
                })
                .AddTo(this);
            KeepArea();
            FarmTime();
            Chase_Item("item1");
            Chase_Item("item2");
            Chase_Item("item3");
            Chase_Item("item4");
            StateProcessor.Execute();


            //育成時間が10％進むにつれて元気ゲージを10％ずつ減少
            if (param.sumTime - old_sumTime >= span / 10)
            {
                param.nowGauge -= 10;
                old_sumTime = param.sumTime;
                if (param.nowGauge < 0)
                    param.nowGauge = 0;
            }

        }
        public void DecreaseEnePower()
        {
            param.sumTime += Time.deltaTime * 1.002f;
            param.second += Time.deltaTime * 1.002f;
        }
        protected State Judge(int rate)
        {
            if (rate <= stopRate) { return State.STOP; }
            else if (rate <= moveRate) { return State.MOVE; }
            else { return State.SLEEP; }
        }
        protected void StateManagement(State state)
        {
            switch (state)
            {
                case State.STOP:
                    nowState = State.STOP;
                    spriteRenderer.sprite = normalImage;
                    if (isComplete) { spriteRenderer.sprite = cryImage; }
                    Stop();
                    break;
                case State.SLEEP:
                    nowState = State.SLEEP;
                    spriteRenderer.sprite = sleepImage;
                    Sleep();
                    break;
                case State.MOVE:
                    nowState = State.MOVE;
                    spriteRenderer.sprite = normalImage;
                    Move();
                    break;
                case State.CHASE:
                    nowState = State.CHASE;
                    spriteRenderer.sprite = normalImage;
                    break;
                default:
                    break;
            }
        }

        public void Normal()
        {
            StateManagement(nowState);
        }

        public void Bubbly()
        {
            itemController.Use_item(_item_type.bubbly_meat);
            StateManagement(nowState);
        }

        public void Sweat()
        {
            itemController.Use_item(_item_type.sweat_juice);
            StateManagement(nowState);
        }

        public void Touch()
        {
            itemController.Use_item(_item_type.touch_light);
        }

        public void Talk()
        {
            itemController.Use_item(_item_type.talk_grass);
        }

        ////////////////////////////育成時間////////////////////////////
        protected virtual void FarmTime()
        {
            param.sumTime += Time.deltaTime;
            param.second += Time.deltaTime;
            if (param.second > 60f)
            {
                param.minute += 1;
                param.second = 0;
            }
            if (param.minute > 60)
            {
                param.hour++;
                param.minute = 0;
            }
            //育成時間に達したら
            if (param.second > span && isComplete == false)   //本来はparam.second →　hour
            {
                isComplete = true;
                // モンスターごとに引数設定
                _farmDirector.FarmComplete(type);
            }

            else if (param.second > (span / 2))
            {
                isScale = true;
                Vector3 kero = new(65, 65, 1);
                this.transform.localScale = kero;
                var col = gameObject.GetComponent<CapsuleCollider2D>();
                col.size = new Vector3(size, size, size);
            }

        }

        protected void Stop()
        {
            //Debug.Log("モンスタが止まっている" + stopTimer);
            stopTimer += Time.deltaTime;
            if (stopTimer >= limitStop)
            {
                stopTimer = 0;
                int rand = Random.Range(0, 100);
                StateManagement(Judge(rand));
                return;
            }
        }
        protected void Sleep()
        {
            //Debug.Log("モンスタが寝ている" + sleepTimer);
            sleepTimer += Time.deltaTime;
            if (sleepTimer >= limitSleep)
            {
                sleepTimer = 0;
                int rand = Random.Range(0, 100);
                StateManagement(Judge(rand));
                return;
            }
        }

        /// <summary>
        /// 歩行動作
        /// </summary>
        protected void MoveToBottom()
        {
            if (targetLane > MinLane) targetLane = Random.Range(-2, 1);
        }
        protected void MoveToTop()
        {
            if (targetLane < MaxLane) targetLane = Random.Range(0, 3);
        }
        protected void MoveToLeft()
        {
            spriteRenderer.flipX = false;  //左向き
            if (currentPos.x <= -1.6f)
            {
                isTarget_paramChange = false;
                MoveToBottom();
                return;
            }
            // 徐々に加速しX方向に常に前進させる
            float acceleratedX = moveDirection.x + (accelerationX * Time.deltaTime);
            moveDirection.x = (Mathf.Clamp(acceleratedX, 0, speedX));
            // 移動実行
            Vector3 globalDirection = transform.TransformDirection(moveDirection);
            transform.Translate(-1 * globalDirection.x * Time.deltaTime, globalDirection.y * Time.deltaTime, 0);
        }
        protected void MoveToRight()
        {
            spriteRenderer.flipX = true;  //右向き
            if (currentPos.x >= 1.6f)
            {
                isTarget_paramChange = true;
                MoveToTop();
                return;
            }
            // 徐々に加速しX方向に常に前進させる
            float acceleratedX = moveDirection.x + (accelerationX * Time.deltaTime);
            moveDirection.x = Mathf.Clamp(acceleratedX, 0, speedX);
            // 移動実行
            Vector3 globalDirection = transform.TransformDirection(moveDirection);
            transform.Translate(globalDirection.x * Time.deltaTime, globalDirection.y * Time.deltaTime, 0);

        }
        protected void Move()
        {
            if (moveTimer >= limitMove)
            {
                moveTimer = 0;
                int rand = Random.Range(0, 100);
                StateManagement(Judge(rand));
                return;
            }

            moveTimer += Time.deltaTime;
            currentPos = transform.position;
            if (isTarget_paramChange == false)
            {
                MoveToRight();
            }
            else
            {
                MoveToLeft();
            }
            //　X方向は目標のポジションまでの差分の割合で速度を計算
            float ratioY = (targetLane * LaneWidth - transform.position.y) / LaneWidth;
            moveDirection.y = ratioY * speedY;
        }


        protected void KeepArea()
        {
            Drag_Monsters drag_Monsters;
            drag_Monsters = gameObject.GetComponent<Drag_Monsters>();
            if ((!drag_Monsters.isDrag) && (nowState != State.MOVE))
            {
                var currentPos = transform.position;
                if (currentPos.y < -1.8f) currentPos.y += 0.5f;
                else if (currentPos.y > 1.8f) currentPos.y -= 0.5f;
                else if (currentPos.x < -1.6f) currentPos.x += 0.5f;
                else if (currentPos.x > 1.6f) currentPos.x -= 0.5f;
                transform.position = currentPos;
            }

        }


        protected void Chase_Item(string tag_name)
        {
            const float speed = 0.5f;
            if (GameObject.FindWithTag(tag_name) != null)
            {
                StateManagement(State.CHASE);
                GameObject item = GameObject.FindWithTag(tag_name);
                var itemPos = item.transform.position;
                var currentPos = transform.position;
                transform.position = Vector3.MoveTowards
                                    (transform.position,
                                     GameObject.FindWithTag(tag_name).transform.position,
                                      Time.deltaTime * speed
                                    );
                if (currentPos.y < -1.8f) currentPos.y += 0.5f;
                else if (currentPos.y > 1.8f) currentPos.y -= 0.5f;
                //イラスト左右方向転換
                if (currentPos.x < itemPos.x)
                    spriteRenderer.flipX = true;  //右向き
                else
                    spriteRenderer.flipX = false;//左向き
            }
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("item1"))
            {
                StateProcessor.State.Value = StateBubbly;
                Destroy(GameObject.FindWithTag("item1"));
            }
            else if (collision.gameObject.CompareTag("item2"))
            {
                StateProcessor.State.Value = StateSweat;
                Destroy(GameObject.FindWithTag("item2"));
            }
            else if (collision.gameObject.CompareTag("item3"))
            {
                StateProcessor.State.Value = StateTouch;
                Destroy(GameObject.FindWithTag("item3"));
            }
            else if (collision.gameObject.CompareTag("item4"))
            {
                StateProcessor.State.Value = StateTalk;
                Destroy(GameObject.FindWithTag("item4"));
            }
            else if (collision.gameObject.name == "target")
            {
                GameObject target = GameObject.Find("target");
            }
            else
            {
                Debug.Log("Unknown Hit Object ");
            }
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("DeliveryBox"))
            {
                if (!isComplete) return;
                // お金を増やす
                _farmDirector.GetCoin(type);

                Destroy(gameObject);
                // コインのアニメーション
                gameDirector.SendMessage("IsStartCoinAnimation");

                if (Random.Range(0, 100) < dropRate)
                {
                    _farmDirector.completeText.text = "少しだけ、広告が入るよ〜";
                    adController.CallAd();
                }

            }
        }


    }
}


