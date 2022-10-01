using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Click : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject ItemInfo;

    public void OnPointerDown(PointerEventData eventData)
    {
        //オブジェクトの前面にある障害を取り除く
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        ItemInfo.GetComponent<AnimatedDialog>().Open();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ItemInfo.GetComponent<AnimatedDialog>().Close();
        //オブジェクトの前面にある障害を元に戻す
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
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
