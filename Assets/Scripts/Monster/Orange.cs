using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orange : MonsterController
{

    new void Start()
    {
        base.Start();
        span = 20.0f;
        col_size = 6;
        monster_num = 1;
        int size = 5;
        var col = gameObject.GetComponent<CapsuleCollider2D>();
        col.size = new Vector3(size, size, size);
    }

    // Update is called once per frame
    protected override void Update()
    {
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

        if ((detectCollider.item3_flg == false || detectCollider.item4_flg == false) && isStop == false && isSleep == false &&
           (GameObject.FindWithTag("item1") == null) && (GameObject.FindWithTag("item2") == null) &&
           (GameObject.FindWithTag("item3") == null) && (GameObject.FindWithTag("item4") == null))
        {
            // stopTimer & sleepTimer Reset
            stopTimer = 0;
            sleepTimer = 0;
            base.Move();
            //イラスト左右方向転換
            if (currentpos.x < targetpos.x)
                Renderer.flipX = true;  //右向き
            else
                Renderer.flipX = false;//左向き

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
}
