using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Monster;

public class Info_AnimateDialog_Open : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject Ms_infoPrefab;
    private GameObject Info;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _layer;
    private static readonly int ParamIsOpen = Animator.StringToHash("IsOpen");
    public bool IsOpen => gameObject.activeSelf;
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

    public void Open()
    {
        Instantiate();
        if (IsOpen || IsTransition) { Debug.Log("Open"); return; }
        _animator.SetBool(ParamIsOpen, true);
        StartCoroutine(WaitAnimation("Shown"));
    }

    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        IsTransition = true;

        yield return new WaitUntil(() =>
        {
            var state = _animator.GetCurrentAnimatorStateInfo(_layer);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });
        IsTransition = false;

        onCompleted?.Invoke();
    }

}
