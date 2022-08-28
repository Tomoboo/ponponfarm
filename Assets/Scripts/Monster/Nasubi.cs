using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nasubi : MonsterController
{

    new void Start()
    {
        base.Start();
        this.span = 40.0f;
        rate = 2.5f;
        int size = 5;
        var col = gameObject.GetComponent<CapsuleCollider2D>();
        col.size = new Vector3(size, size, size);
    }

    protected override void farmTime()
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
            director.GetComponent<GameDirector>().FarmComplete(0);
            // ランダム確率で感謝の涙GET
            if (Random.Range(0, 1000) < dropprate)
                director.GetComponent<GameDirector>().kanshaGet();
        }

        else if (second > (this.span / 2))
        {
            int size = 6;
            isScale = true;
            Vector3 kero = new Vector3(80, 80, 1);
            kero.x = 80;
            kero.y = 80;
            kero.z = 1;
            this.transform.localScale = kero;
            var col = gameObject.GetComponent<CapsuleCollider2D>();
            col.size = new Vector3(size,size,size);
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        DetectCollider detectCollider = GetComponent<DetectCollider>();
        Enepower_flg = UICanvas.GetComponent<Info_Monsters>().Enepower_flg; // ※モンスター共通
        float spanPercent = span * decreasePoint; //元気ゲージの減速量

        farmTime();
        base.UseItem1();
        base.UseItem2();
        base.UseItem3();
        base.UseItem4();
        base.Stop();

        if ((detectCollider.item3_flg == false || detectCollider.item4_flg == false) && isStop == false &&
           (GameObject.FindWithTag("item1") == null) && (GameObject.FindWithTag("item2") == null) &&
           (GameObject.FindWithTag("item3") == null) && (GameObject.FindWithTag("item4") == null))
        {
            // ストップタイマリセット
            stoptimer = 0;
            base.Move();
            //イラスト左右方向転換
            if (currentpos.x < targetpos.x)
                Renderer.flipX = true;  //右向き
            else
                Renderer.flipX = false;//左向き
            /*if (isScale == false)
            {
                if (targetpos.x > 464.76){if (num == 1)isStop = true;
                    num = -1;
                }
                else if (targetpos.x < -464.76){if (num == -1)isStop = true;
                    num = 1;
                }
            }
            else
            {
                if (targetpos.x > 386){if (num == 1)isStop = true;
                    num = -1;
                }else if (targetpos.x < -386){if (num == -1)isStop = true;
                    num = 1;
                }
            }*/
        }

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

    }
}
