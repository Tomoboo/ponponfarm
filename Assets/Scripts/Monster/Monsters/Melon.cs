using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Monster;
using Item_farm;

public class Melon : MonsterController
{

    new void Start()
    {
        base.Start();
        type = monster_type.melon;
    }

    protected override void FarmTime()
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
        if (param.sumTime > span && isComplete == false)   //本来はparam.second →　param.hour
        {
            isComplete = true;
            // モンスターごとに引数設定
            _farmDirector.FarmComplete(type);
        }

        else if (param.sumTime >= (span / 2))
        {
            isScale = true;
            Vector3 kero = new(65, 65, 1);
            this.transform.localScale = kero;
            var col = gameObject.GetComponent<CapsuleCollider2D>();
            col.size = new Vector3(size, size, size);
        }
    }


}

