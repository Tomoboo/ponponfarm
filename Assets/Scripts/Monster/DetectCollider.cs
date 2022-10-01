using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;

public class DetectCollider : MonoBehaviour
{
    [System.NonSerialized] public bool isBubbly = false;
    [System.NonSerialized] public bool isSweat = false;
    [System.NonSerialized] public bool isTouch = false;
    [System.NonSerialized] public bool isTalk = false;
    [System.NonSerialized] public bool isTouchtarget = false;

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        MonsterController Monster = GetComponent<MonsterController>();
        if (collision.gameObject.CompareTag("item1"))
        {
            isBubbly = true;
            Monster.isStart_bubbly = false;
            Destroy(GameObject.FindWithTag("item1"));
        }
        else if (collision.gameObject.CompareTag("item2"))
        {
            isSweat = true;
            Monster.isStart_sweat = false;
            Destroy(GameObject.FindWithTag("item2"));
        }
        else if (collision.gameObject.CompareTag("item3"))
        {
            isTouch = true;
            Monster.isStart_touch = false;
            Destroy(GameObject.FindWithTag("item3"));
        }
        else if (collision.gameObject.CompareTag("item4"))
        {
            isTalk = true;
            Monster.isStart_talk = false;
            Destroy(GameObject.FindWithTag("item4"));
        }
        else if(collision.gameObject.name == "target")
        {
            GameObject target = GameObject.Find("target");
            Monster.isTouch = true;
        }
    }

}
