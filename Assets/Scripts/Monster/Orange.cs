using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;
using Item_farm;

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
        Random.InitState(System.DateTime.Now.Millisecond);
        detectCollider = GetComponent<DetectCollider>();
        Enepower_flg = UICanvas.GetComponent<Info_Monsters>().Enepower_flg; 
        float spanPercent = span * decreasePoint; 
        ItemController itemController = GetComponent<ItemController>();

        FarmTime();
        itemController.Use_item(_item_type.bubbly_meat);
        itemController.Use_item(_item_type.sweat_juice);
        itemController.Use_item(_item_type.touch_light);
        //UseItem4();
        Stop();
        Sleep();

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
            
            if (currentpos.x < targetpos.x)
                Renderer.flipX = true;  
            else
                Renderer.flipX = false;

            if (Enepower_flg == true)
            {
                second += Time.deltaTime * 1.002f;
            }

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

