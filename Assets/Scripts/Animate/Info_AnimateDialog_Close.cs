using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Info_AnimateDialog_Close : MonoBehaviour
{

    /// <summary>
    /// �_�C�A���O�̃A�񃁁[�V����
    /// </summary>

    // �����X�^�[�琬���prefab
    private GameObject Info;
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

    // Start is called before the first frame update
    void Start()
    {
        Info = GameObject.FindWithTag("Monster_Info");
        gameObject.AddComponent<EventTrigger>();//EventTrigger�R���|�[�l���g���擾
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();//�C�x���g�̐ݒ�ɓ���
        entry.eventID = EventTriggerType.PointerDown;//�������u��

        entry.callback.AddListener((x) =>
        {
            Close();
        });

        //�C�x���g�̐ݒ��EventTrigger�ɔ��f
        eventTrigger.triggers.Add(entry);
    }

    //�_�C�A���O�����
    public void Close()
    {
        //�s����h�~
        if (!IsOpen || IsTransition) { Debug.Log("Close"); return; }
        //IsOpen�t���O���N���A
        _animator.SetBool(ParamIsOpen, false);
        //�A�j���[�V�����ҋ@���A�I�������p�l�����̂�j�󂷂�
        //Destroy(Info,1);
        StartCoroutine(WaitAnimation("Hidden", () => Destroy(Info)));
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

}
