using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item_Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    GameObject gameDirector;
    GameObject ItemCanvas;
    GameObject draggingObj;
    private RectTransform prePos1;
    private Vector3 preposition;
    public Transform parenTran;
    Vector2 localpoint;
    Camera UIcamera;
    Color color;

    void Awake()
    {
        ItemCanvas = GameObject.Find("FarmItemScene");
        gameDirector = GameObject.Find("GameDirector");
        UIcamera = GameObject.Find("UI Camera").GetComponent<Camera>();
        preposition =  new Vector3(0.09f, -0.38f, 0);
        this.color = gameObject.GetComponent<Image>().color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        GameObject draggingObj1; 
        GameObject draggingObj2;
        GameObject draggingObj3;
        GameObject draggingObj4;
        parenTran = transform.parent;

        //オブジェクトの前面にある障害を取り除く
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        if (transform.parent.name == "item1Parent")
        {
            //アイテムのイメージを複製
            draggingObj1 = Instantiate(gameObject, parenTran);
            draggingObj1.name = "SpawnItem1";
            //複製したアイテムにタグを追加
            draggingObj1.tag = "item1";
            //グローバル変数に代入
            draggingObj = draggingObj1;
        }
        else if (transform.parent.name == "item2Parent")
        {
            //アイテムのイメージを複製
            draggingObj2 = Instantiate(gameObject, parenTran);
            draggingObj2.name = "SpawnItem2";
            //複製したアイテムにタグを追加
            draggingObj2.tag = "item2";
            //グローバル変数に代入
            draggingObj = draggingObj2;
        }
        else if (transform.parent.name == "item3Parent")
        {
            //アイテムのイメージを複製
            draggingObj3 = Instantiate(gameObject, parenTran);
            draggingObj3.name = "SpawnItem3";
            //複製したアイテムにタグを追加
            draggingObj3.tag = "item3";
            //グローバル変数に代入
            draggingObj = draggingObj3;
        }
        else if (transform.parent.name == "item4Parent")
        {
            //アイテムのイメージを複製
            draggingObj4 = Instantiate(gameObject, parenTran);
            draggingObj4.name = "SpawnItem4";
            //複製したアイテムにタグを追加
            draggingObj4.tag = "item4";
            //グローバル変数に代入
            draggingObj = draggingObj4;
        }
        //複製を最前面に配置
        draggingObj.transform.SetAsFirstSibling();
        //複製に元のアイテム座標を代入
        draggingObj.GetComponent<RectTransform>().localPosition = GetLocalPosition(((PointerEventData)eventData).position, this.transform);
        //複製元の色を暗くする
        gameObject.GetComponent<Image>().color = Color.gray;
    }

    private  Vector3 GetLocalPosition(Vector3 position, Transform transform)
    {
        //画面上の座標(Screen Point)を RectTransform上のローカル座標に変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(), position, UIcamera, out  localpoint);
        return new Vector3(localpoint.x, localpoint.y, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //ドラッグ中は位置を更新する
        draggingObj.GetComponent<RectTransform>().localPosition = GetLocalPosition(((PointerEventData)eventData).position, this.transform);
       Debug.Log("(x ,y ,z ) = " + draggingObj.transform.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool ishit = true;

        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach (var hit in raycastResults)
        {
            //ドロップエリア内
            if (hit.gameObject.CompareTag("ItemdropArea"))
            {
                ishit = false;
            }
        }
        // ドロップエリア外
        if (ishit)
        {
            GameObject dropArea = GameObject.Find("ItemDropAreas");
            parenTran = dropArea.transform;
            draggingObj.transform.position = preposition;
            var localpos = draggingObj.transform.localPosition;
            localpos.z = 0;
            draggingObj.transform.localPosition = localpos;
        }

        draggingObj.transform.SetParent(parenTran);
        draggingObj.AddComponent<BoxCollider2D>();
        /////////////////////////////再度ロックする//////////////////////////////////////
        if (gameDirector.GetComponent<GameDirector>().Lock_item1.activeSelf == false)
            gameDirector.GetComponent<GameDirector>().Lock_item1.SetActive(true);

       else if(gameDirector.GetComponent<GameDirector>().Lock_item2.activeSelf == false)
            gameDirector.GetComponent<GameDirector>().Lock_item2.SetActive(true);

       else if (gameDirector.GetComponent<GameDirector>().Lock_item3.activeSelf == false)
            gameDirector.GetComponent<GameDirector>().Lock_item3.SetActive(true);

       else if (gameDirector.GetComponent<GameDirector>().Lock_item4.activeSelf == false)
            gameDirector.GetComponent<GameDirector>().Lock_item4.SetActive(true);
         //itemを元のカラーに戻す
        gameObject.GetComponent<Image>().color = this.color;
        //オブジェクトの前面にある障害を元に戻す
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


}
