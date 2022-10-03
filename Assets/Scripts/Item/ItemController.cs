using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Item_farm
{

    public class ItemController : MonoBehaviour
    {
        Camera UiCamera;
        GameObject UIcanvas;
        GameObject commentSpawn;
        Transform parenTran;
        [NonSerialized] public GameObject target_obj;
        public GameObject commentPrefab;
        public GameObject targetPrefab;
        public float touch_light_speed = 0;
        public float decreasePoint = 0.005f;
        Bubbly_meat bubbly = new(0, 30.0f);
        Sweat_juice sweat = new(0, 30.0f);
        Touch_light touch = new(0, 30.0f);
        Talk_grass talk = new(0, 30.0f);

        public void Awake()
        {
            UiCamera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();
            UIcanvas = GameObject.FindWithTag("UICanvas");
            parenTran = UIcanvas.transform;
            touch_light_speed = 0.1f;
        }

        public void Use_item(_item_type type)
        {
            MonsterController Monster = GetComponent<MonsterController>();
            DetectCollider Detect = GetComponent<DetectCollider>();
            switch (type)
            {
                case _item_type.bubbly_meat:
                    //ぶくぶく肉の効果適用
                    Monster.Chase_Item("item1");
                    if (Detect.isBubbly == true)
                    {
                        Debug.Log("ぶくぶく肉　効果適用");
                        bubbly.Effect_time += Time.deltaTime;
                        Debug.Log("ぶくぶく肉:経過時間　" + bubbly.Effect_time);
                        //経過時間になったら
                        if (bubbly.Effect_time > bubbly.Limit_time)
                        {
                            Detect.isBubbly = false;
                            Debug.Log("ぶくぶく肉　効果終了");
                            bubbly.Effect_time = 0;
                        }
                        //育成スピード増加（ぶくぶく肉の効果）
                        Monster.second += Time.deltaTime * 1.004f;
                    }
                    break;
                case _item_type.sweat_juice:
                    //甘いジュースの効果適用
                    Monster.Chase_Item("item2");
                    if (Detect.isSweat == true)
                    {
                        Debug.Log("甘いジュース　効果適用");
                        float spanPercent = Monster.span * decreasePoint; //元気ゲージの減速量
                        sweat.Effect_time += Time.deltaTime;
                        Debug.Log("甘いジュース:経過時間　" + sweat.Effect_time);
                        //経過時間になったら
                        if (sweat.Effect_time > sweat.Limit_time)
                        {
                            Detect.isSweat = false;
                            Debug.Log("ぶくぶく肉　効果終了");
                            sweat.Effect_time = 0;
                        }
                        //育成時間が10％進むにつれて元気ゲージを増加（甘いジュースの効果）
                        if (Monster.second - Monster.oldSecond > spanPercent)
                        {
                            Monster.nowGauge += 11;
                            Monster.oldSecond = Monster.second;
                            if (Monster.nowGauge > 100)
                                Monster.nowGauge = 100;
                        }
                    }
                    break;
                case _item_type.touch_light:
                    Monster.Chase_Item("item3");
                    //ふれあいライトの効果適用
                    if (Detect.isTouch == true)
                    {
                        int now_pos_x = 400;
                        int now_pos_y = 400;
                        Monster.GetComponent<Drag_Monsters>().enabled = false;
                        if (GameObject.Find("target") == null)
                        {
                            target_obj = Instantiate(targetPrefab, new Vector3(0, -100, 0), Quaternion.identity);
                            target_obj.name = targetPrefab.name;

                            target_obj.GetComponent<MousePoint>().isTarget = true;
                        }
                        Debug.Log("ふれあいライト　効果適用");
                        touch.Effect_time += Time.deltaTime;
                        Debug.Log("ふれあいライト経過時間　" + touch.Effect_time);
                        //経過時間になったら
                        if (touch.Effect_time > touch.Limit_time)
                        {
                            Detect.isTouch = false;
                            Debug.Log("ふれあいライト　効果終了");
                            Destroy(target_obj);
                            touch.Effect_time = 0;
                            Monster.GetComponent<Drag_Monsters>().enabled = true;
                        }
                        //アイテム3の効果    
                        if (Detect.isTouchtarget != true)
                        {
                            Transform tran;
                            tran = Monster.transform;
                            if ((-now_pos_x < tran.position.x) && (tran.position.x < now_pos_x)
                                && (-now_pos_y < tran.position.x) && (tran.position.x < now_pos_y))
                            {
                                Monster.transform.position = Vector3.MoveTowards(Monster.transform.position, target_obj.transform.position, touch_light_speed);
                            }

                        }
                    }
                    break;
                case _item_type.talk_grass:
                    Monster.Chase_Item("item4");
                    //甘いジュースの効果適用
                    if (Detect.isTalk == true)
                    {
                        //Debug.Log("しゃべれ草　効果適用");
                        Monster.GetComponent<Info_AnimateDialog_Open>().enabled = false;
                        talk.Effect_time += Time.deltaTime;
                        //Debug.Log("しゃべれ草:経過時間　" + talk.Effect_time);
                        //経過時間になったら
                        if (talk.Effect_time > talk.Limit_time)
                        {
                            Monster.GetComponent<Info_AnimateDialog_Open>().enabled = true;
                            Detect.isTalk = false;
                            //Debug.Log("しゃべれ草　効果終了");
                            talk.Effect_time = 0;
                        }
                        if (Input.GetMouseButtonDown(0))
                        {
                            Ray ray = UiCamera.ScreenPointToRay(Input.mousePosition);
                            float max_distance = 100.0f;
                            RaycastHit2D hit_info = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, max_distance);

                            if (hit_info.collider)
                            {
                                if (hit_info.collider.gameObject.CompareTag("Monster"))
                                {
                                    CreateComment();
                                }
                            }

                        }

                    }
                    break;
            }
        }

        public void CreateComment()
        {
            Vector3 tran;
            float adjust_x = 0.5f;
            float adjust_y = 1.0f;
            tran = gameObject.transform.position;
            float x = tran.x;
            float y = tran.y;
            float z = tran.z;
            if (GameObject.FindWithTag("comment") == null)
            {
                commentSpawn = Instantiate(commentPrefab);
                commentSpawn.transform.position = new Vector3(x + adjust_x, y + adjust_y, z);
                commentSpawn.transform.SetParent(parenTran);
                Vector3 kero = new(1, 1, 1);
                kero.x = 1;
                kero.y = 1;
                kero.z = 1;
                commentSpawn.transform.localScale = kero;
            }
            Destroy(commentSpawn, 10.0f);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
