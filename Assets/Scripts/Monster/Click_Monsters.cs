using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click_Monsters : MonoBehaviour,IPointerClickHandler
{

    /*[SerializeField] protected GameObject Ms_infoPrefab;
    protected GameObject UICanvas;
    protected GameObject Info;
    protected Transform parentTran;*/

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Monster Click");
        Info_AnimateDialog animate = GetComponent<Info_AnimateDialog>();
        animate.Open();
    }

}
