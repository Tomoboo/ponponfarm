using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollider : MonoBehaviour
{
    [System.NonSerialized] public bool item1_flg = false;
    [System.NonSerialized] public bool item2_flg = false;
    [System.NonSerialized] public bool item3_flg = false;
    [System.NonSerialized] public bool item4_flg = false;
    [System.NonSerialized] public bool isTouch = false;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        MonsterController Monster = GetComponent<MonsterController>();
        if (collision.gameObject.CompareTag("item1"))
        {
            item1_flg = true;
            Monster.item1start_flg = false;
            Destroy(GameObject.FindWithTag("item1"));
        }
        else if (collision.gameObject.CompareTag("item2"))
        {
            item2_flg = true;
            Monster.item2start_flg = false;
            Destroy(GameObject.FindWithTag("item2"));
        }
        else if (collision.gameObject.CompareTag("item3"))
        {
            item3_flg = true;
            Monster.item3start_flg = false;
            Destroy(GameObject.FindWithTag("item3"));
        }
        else if (collision.gameObject.CompareTag("item4"))
        {
            item4_flg = true;
            Monster.item4start_flg = false;
            Destroy(GameObject.FindWithTag("item4"));
        }
        else if(collision.gameObject.name == "target")
        {
            GameObject target = GameObject.Find("target");
            Monster.isTouch = true;
        }
    }

}
