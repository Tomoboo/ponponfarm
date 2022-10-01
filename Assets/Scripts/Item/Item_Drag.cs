using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Item_Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [System.NonSerialized] public Transform parenTran;
    GameObject gameDirector;
    GameObject ItemCanvas;
    GameObject draggingObj;
    GameObject DropArea;
    private RectTransform prePos1;
    private Vector3 preposition;
    Vector2 localpoint;
    Vector3[] corners;
    Camera UIcamera;
    Color color;

    void Awake()
    {
        UIcamera = GameObject.Find("UI Camera").GetComponent<Camera>();
        DropArea = GameObject.Find("ItemDropAreas");
        ItemCanvas = GameObject.Find("FarmItemScene");
        gameDirector = GameObject.Find("GameDirector");
        this.color = gameObject.GetComponent<Image>().color;

        var rectTransform = DropArea.GetComponent<RectTransform>();
        //　ローカル空間におけるRectTransformの長方形の角の座標を取得
        //　返される4つの頂点は時計回りに「左下、左上、右上、右下」
        corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        for (var i = 0; i < corners.Length; i++)
        {
            Debug.Log($"Local Corners[{i}] : {corners[i]}");
        }
    }

    private GameObject Instatiate_item(GameObject drag_obj, string item_name, string item_tag_name)
    {
        //アイテムのイメージを複製
        drag_obj = Instantiate(gameObject, parenTran);
        drag_obj.name = item_name;
        //複製したアイテムにタグを追加
        drag_obj.tag = item_tag_name;
        return drag_obj;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parenTran = transform.parent;
        //オブジェクトの前面にある障害を取り除く
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        if (transform.parent.name == "item1Parent")
        {
            //グローバル変数に代入
            draggingObj = Instatiate_item(draggingObj, "SpawnItem1", "item1");
        }
        else if (transform.parent.name == "item2Parent")
        {
            //グローバル変数に代入
            draggingObj = Instatiate_item(draggingObj, "SpawnItem2", "item2");
        }
        else if (transform.parent.name == "item3Parent")
        {
            //グローバル変数に代入
            draggingObj = Instatiate_item(draggingObj, "SpawnItem3", "item3");
        }
        else if (transform.parent.name == "item4Parent")
        {
            //グローバル変数に代入
            draggingObj = Instatiate_item(draggingObj, "SpawnItem4", "item4");
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
        draggingObj.GetComponent<RectTransform>().localPosition = GetLocalPosition(eventData.position, this.transform);
        Debug.Log("(x ,y ,z ) = " + draggingObj.transform.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int index = 0;
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
            Vector3 worldpos = draggingObj.transform.position;
            float diff = 0.5f;
            if ((worldpos.x > corners[2].x) && (worldpos.y > corners[3].y)) //四隅の右横
            {
                worldpos.x = corners[2].x - diff;
                draggingObj.transform.position = worldpos;
            }
            else if (worldpos.y > corners[2].y) //四隅の上部
            {
                if (worldpos.x > 0)
                    worldpos.x -= diff;
                else
                    worldpos.x += diff;
                worldpos.y = corners[1].y - diff;
                draggingObj.transform.position = worldpos;
            }
            else if ((worldpos.x < corners[0].x) && (worldpos.y > corners[0].y)) //四隅の左横
            {
                worldpos.x = corners[0].x + diff;
                draggingObj.transform.position = worldpos;
            }
            else if (worldpos.y < corners[0].y) //四隅の下部
            {
                if (worldpos.x > 0)
                    worldpos.x -= diff;
                else
                    worldpos.x += diff;
                worldpos.y = corners[0].y + diff;
                draggingObj.transform.position = worldpos;
            }
            var localpos = draggingObj.transform.localPosition;
            localpos.z = 0;
            draggingObj.transform.localPosition = localpos;
        }

        draggingObj.transform.SetParent(parenTran);
        draggingObj.transform.SetSiblingIndex(index);
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
