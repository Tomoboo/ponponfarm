using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;
using MonsterState;
using UniRx;

public class DetectCollider : MonoBehaviour
{
    [System.NonSerialized] public bool isBubbly = false;
    [System.NonSerialized] public bool isSweat = false;
    [System.NonSerialized] public bool isTouch = false;
    [System.NonSerialized] public bool isTalk = false;
    [System.NonSerialized] public bool isTouchTarget = false;
    private void Start()
    {

    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        MonsterController Monster = GetComponent<MonsterController>();
        if (collision.gameObject.CompareTag("item1"))
        {
            isBubbly = true;
            Destroy(GameObject.FindWithTag("item1"));
        }
        else if (collision.gameObject.CompareTag("item2"))
        {
            isSweat = true;
            Destroy(GameObject.FindWithTag("item2"));
        }
        else if (collision.gameObject.CompareTag("item3"))
        {
            isTouch = true;
            Destroy(GameObject.FindWithTag("item3"));
        }
        else if (collision.gameObject.CompareTag("item4"))
        {
            isTalk = true;
            Destroy(GameObject.FindWithTag("item4"));
        }
        else if (collision.gameObject.name == "target")
        {
            GameObject target = GameObject.Find("target");
            //Monster.isTouchTarget = true;
        }
    }

}
