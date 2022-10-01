using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// �_�C�A���O�̃A�񃁁[�V����
/// </summary>

public class AnimatedDialog : MonoBehaviour
{
    //�A�j���[�^�[
    [SerializeField] private Animator _animator;
    //�A�j���[�^�[�R���g���[���[�̃��C���[
    [SerializeField] private int _layer;
    //isOpen�t���O�i�A�j���[�^�[�R���g���[���[���Œ�`�����t���O�j
    private static readonly int ParamIsOpen = Animator.StringToHash("IsOpen");
    //�_�C�A���O�͊J���Ă��邩�ǂ���
    public bool IsOpen => gameObject.activeSelf;
    //�A�j���[�V���������ǂ���
    public bool IsTransition { get; private set; }

    //�_�C�A���O���J��
    public void Open()
    {
        //�s����h�~
        //if (IsOpen || IsTransition) return;
        //�p�l�����̂��A�N�e�B�u�ɂ���
        gameObject.SetActive(true);
        //IsOpen�t���O�����Z�b�g
        _animator.SetBool(ParamIsOpen, true);
        //�A�j���[�V�����ҋ@
        //StartCoroutine(WaitAnimation("Shown"));
    }

    //�_�C�A���O�����
    public void Close()
    {
        //�s����h�~
       // if (!IsOpen || IsTransition) return;
        //IsOpen�t���O���N���A
        _animator.SetBool(ParamIsOpen, false);
        //�A�j���[�V�����ҋ@���A�I�������p�l�����̂��A�N�e�B�u�ɂ���
        //StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
    }

    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        IsTransition = true;

        yield return new WaitUntil(() =>
        {
            //�X�e�[�g���ω����A�A�j���[�V�������I������܂Ń��[�v
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
