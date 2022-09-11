using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDropAreas : MonoBehaviour, IDropHandler
{

    // Start is called before the first frame update
    void Start()
    {
        

    }

    public void OnDrop(PointerEventData data)
    {
        Debug.Log(gameObject.name);
        Item_Drag drag_Item = data.pointerDrag.GetComponent<Item_Drag>();
        if (drag_Item != null)
            drag_Item.parenTran = this.transform;
    }

}
