using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyMonster : MonoBehaviour
{
    //[System.NonSerialized] public int pick_monster = -1;
    //public int pick_monster = -1;
    [SerializeField] GameObject notice_Nasubi;
    [SerializeField] GameObject notice_Orange;
    public void PayButtonDown(int num)
    {
        switch (num)
        {
            case 0: //Nasubi
                GameObject Notice_nasubi = Instantiate(notice_Nasubi);
                Notice_nasubi.name = notice_Nasubi.name;
                break;
            case 1: //Orange
                GameObject Notice_orange = Instantiate(notice_Orange);
                Notice_orange.name = notice_Orange.name;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
