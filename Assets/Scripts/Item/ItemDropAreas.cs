using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropAreas : MonoBehaviour, IDropHandler
{

    private Transform parenTran;
    private GameObject ItemScreen;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void OnDrop(PointerEventData data)
    {
        Debug.Log(gameObject.name);
  
        Item_Drag dragObj = data.pointerDrag.GetComponent<Item_Drag>();
        if (dragObj != null)
            dragObj.parenTran = this.transform;
    }
  
}
