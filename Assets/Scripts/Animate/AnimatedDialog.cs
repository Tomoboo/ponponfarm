using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// ダイアログのア二メーション
/// </summary>

public class AnimatedDialog : MonoBehaviour
{
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

    //ダイアログを開く
    public void Open()
    {
        //不操作防止
        //if (IsOpen || IsTransition) return;
        //パネル自体をアクティブにする
        gameObject.SetActive(true);
        //IsOpenフラグをリセット
        _animator.SetBool(ParamIsOpen, true);
        //アニメーション待機
        //StartCoroutine(WaitAnimation("Shown"));
    }

    //ダイアログを閉じる
    public void Close()
    {
        //不操作防止
       // if (!IsOpen || IsTransition) return;
        //IsOpenフラグをクリア
        _animator.SetBool(ParamIsOpen, false);
        //アニメーション待機し、終わったらパネル自体を非アクティブにする
        //StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
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
