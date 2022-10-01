using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Click : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] GameObject ItemInfo;

    public void OnPointerDown(PointerEventData eventData)
    {
        //�I�u�W�F�N�g�̑O�ʂɂ����Q����菜��
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        ItemInfo.GetComponent<AnimatedDialog>().Open();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ItemInfo.GetComponent<AnimatedDialog>().Close();
        //�I�u�W�F�N�g�̑O�ʂɂ����Q�����ɖ߂�
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
