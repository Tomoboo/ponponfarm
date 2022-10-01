using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


/// <summary>
/// ダイアログのア二メーション
/// </summary>

public class Info_AnimateDialog_Open : MonoBehaviour, IPointerClickHandler
{
    // モンスター育成情報prefab
    [SerializeField] private GameObject Ms_infoPrefab;
    private GameObject Info;
    //アニメーター
    [SerializeField] private Animator _animator;
    //アニメーターコントローラーのレイヤー
    [SerializeField] private int _layer;
    //isOpenフラグ（アニメーターコントローラー内で定義したフラグ）
    private static readonly int ParamIsOpen = Animator.StringToHash("IsOpen");
    //ダイアログは開いているかどうか
    public bool IsOpen => gameObject.activeSelf;
    //アニメーション中かどうか
    public bool IsTransition { get; private set; }
    private GameObject UICanvas;
    private Transform parentTran;
    private GameObject obj;

    void Awake()
    {
        this.UICanvas = GameObject.Find("UICanvas");
        parentTran = GameObject.Find("UICanvas").transform;
    }

    public void Instantiate()
    {
        Info = Instantiate(Ms_infoPrefab);
        Info.name = Ms_infoPrefab.name;
        Info.transform.SetParent(parentTran);
        Info.transform.localPosition = new Vector3(40, 0, 0);
        Info.GetComponent<RectTransform>().SetAsLastSibling();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Monster Click");
        Open();
    }

    //ダイアログを開く
    public void Open()
    {
        //パネル自体を生成する
        Instantiate();
        //不操作防止
        if (IsOpen || IsTransition) { Debug.Log("Open"); return; }
        //IsOpenフラグをリセット
        _animator.SetBool(ParamIsOpen, true);
        //アニメーション待機
        StartCoroutine(WaitAnimation("Shown"));
    }

    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        IsTransition = true;

        yield return new WaitUntil(() =>
        {
            //ステートが変化し、アニメーションが終了するまでループ
            var state = _animator.GetCurrentAnimatorStateInfo(_layer);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });
        IsTransition = false;

        onCompleted?.Invoke();
    }

}
