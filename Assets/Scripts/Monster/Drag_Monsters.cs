using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Drag_Monsters : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [System.NonSerialized] public Transform parenTran;
    private Vector3 preposition;
    public bool isDrag { get; set; } = false;
    Vector3 worldpoint;
    Vector3[] corners;
    Camera UIcamera;

    void InvalidClick()
    {
        if (isDrag == true)
            gameObject.GetComponent<Info_AnimateDialog_Open>().enabled = false;
        else
            gameObject.GetComponent<Info_AnimateDialog_Open>().enabled = true;
    }

    void Judge_ScriptEnabled(string name)
    {
        switch (name)
        {
            case "Nasubi":
                InvalidClick();
                break;
            case "Orange":
                InvalidClick();
                break;
            case "Carot":
                InvalidClick();
                break;
            case "Cabbage":
                InvalidClick();
                break;
            case "Banana":
                InvalidClick();
                break;
            case "Corn":
                InvalidClick();
                break;
            case "Pieman":
                InvalidClick();
                break;
            case "Peach":
                InvalidClick();
                break;
            case "Melon":
                InvalidClick();
                break;
            case "Suika":
                InvalidClick();
                break;

        }
    }
    void Awake()
    {
        GameObject DropArea = GameObject.Find("ItemDropAreas");
        UIcamera = GameObject.Find("UI Camera").GetComponent<Camera>();
        //preposition = new Vector3(0.09f, -0.38f, 0);
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
        Judge_ScriptEnabled(gameObject.name);
        parenTran = transform.parent;
    }
    private Vector3 GetMousePosition(Vector3 position)
    {
        //画面上の座標(Screen Point)を Transform上のローカル座標に変換
        worldpoint = UIcamera.ScreenToWorldPoint(position);
        return new Vector3(worldpoint.x, worldpoint.y, 0);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //ドラッグ中は位置を更新する
        transform.position = GetMousePosition(eventData.position);
        Vector3 pos = transform.localPosition;
        pos.z = 0;
        transform.localPosition = pos;
        Debug.Log($"trasnform.position : {transform.position}");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        Vector3 worldpos = transform.position;
        float diff = 0.7f;
        if ((worldpos.x > corners[2].x) && (worldpos.y > corners[3].y)) //四隅の右横
        {
            worldpos.x = corners[2].x - diff;
            transform.position = worldpos;
        }
        else if (worldpos.y > corners[2].y) //四隅の上部
        {
            worldpos.y = corners[1].y - diff;
            transform.position = worldpos;
        }
        else if ((worldpos.x < corners[0].x) && (worldpos.y > corners[0].y)) //四隅の左横
        {
            worldpos.x = corners[0].x + diff;
            transform.position = worldpos;
        }
        else if (worldpos.y < corners[0].y) //四隅の下部
        {
            worldpos.y = corners[0].y + diff;
            transform.position = worldpos;
        }

        gameObject.transform.SetParent(parenTran);
        Judge_ScriptEnabled(gameObject.name);

    }


}
