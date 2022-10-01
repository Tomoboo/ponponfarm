using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Info_AnimateDialog_Close : MonoBehaviour
{

    /// <summary>
    /// ダイアログのア二メーション
    /// </summary>

    // モンスター育成情報prefab
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

    // Start is called before the first frame update
    void Start()
    {
        Info = GameObject.FindWithTag("Monster_Info");
        gameObject.AddComponent<EventTrigger>();//EventTriggerコンポーネントを取得
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();//イベントの設定に入る
        entry.eventID = EventTriggerType.PointerDown;//押した瞬間

        entry.callback.AddListener((x) =>
        {
            Close();
        });

        //イベントの設定をEventTriggerに反映
        eventTrigger.triggers.Add(entry);
    }

    //ダイアログを閉じる
    public void Close()
    {
        //不操作防止
        if (!IsOpen || IsTransition) { Debug.Log("Close"); return; }
        //IsOpenフラグをクリア
        _animator.SetBool(ParamIsOpen, false);
        //アニメーション待機し、終わったらパネル自体を破壊する
        //Destroy(Info,1);
        StartCoroutine(WaitAnimation("Hidden", () => Destroy(Info)));
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
