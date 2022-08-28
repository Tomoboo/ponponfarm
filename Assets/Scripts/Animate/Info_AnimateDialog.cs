using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


/// <summary>
/// ダイアログのア二メーション
/// </summary>

public class Info_AnimateDialog : MonoBehaviour, IPointerClickHandler
{
    // モンスター育成情報プレハブ
    [SerializeField] protected GameObject Ms_infoPrefab;
    [System.NonSerialized] public GameObject Info;
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
    protected GameObject UICanvas;
    protected Transform parentTran;

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
        Vector3 kero = new Vector3(1, 1, 1);
        kero.x = 1;
        kero.y = 1;
        kero.z = 1;
        Info.transform.localScale = kero;
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
        //不操作防止
        //if (IsOpen || IsTransition) { Debug.Log("Open"); return; }
        //パネル自体を生成する
        Instantiate();
        //IsOpenフラグをリセット
        _animator.SetBool(ParamIsOpen, true);
        //アニメーション待機
        //StartCoroutine(WaitAnimation("Shown"));
    }

    //ダイアログを閉じる
    public void Close()
    {
        //不操作防止
        //if (!IsOpen || IsTransition) return;
        //IsOpenフラグをクリア
        _animator.SetBool(ParamIsOpen, false);
        //アニメーション待機し、終わったらパネル自体を破壊する
        Destroy(Info);
        //StartCoroutine(WaitAnimation("Hidden", () => Destroy(Info)));
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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
