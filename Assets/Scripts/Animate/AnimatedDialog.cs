using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>

/// </summary>

public class AnimatedDialog : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _layer;
    private static readonly int ParamIsOpen = Animator.StringToHash("IsOpen");
    public bool IsOpen => gameObject.activeSelf;
    public bool IsTransition { get; private set; }

    public void Open()
    {
        //if (IsOpen || IsTransition) return;
        gameObject.SetActive(true);
        //IsOpen�t���O�����Z�b�g
        _animator.SetBool(ParamIsOpen, true);
        //StartCoroutine(WaitAnimation("Shown"));
    }

    public void Close()
    {
       // if (!IsOpen || IsTransition) return;
        _animator.SetBool(ParamIsOpen, false);
        //StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
