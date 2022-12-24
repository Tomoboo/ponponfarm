using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Monster;

public class Info_AnimateDialog_Close : MonoBehaviour
{

    private GameObject Info;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _layer;
    private static readonly int ParamIsOpen = Animator.StringToHash("IsOpen");
    public bool IsOpen => gameObject.activeSelf;
    public bool IsTransition { get; private set; }

    void Start()
    {
        Info = transform.parent.gameObject;
        gameObject.AddComponent<EventTrigger>();
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;

        entry.callback.AddListener((x) =>
        {
            Close();
        });

        eventTrigger.triggers.Add(entry);
    }

    public void Close()
    {
        if (!IsOpen || IsTransition) { Debug.Log("Close"); return; }
        _animator.SetBool(ParamIsOpen, false);
        StartCoroutine(WaitAnimation("Hidden", () => Destroy(Info)));
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
