using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;


/// <summary>
/// �_�C�A���O�̃A�񃁁[�V����
/// </summary>

public class Info_AnimateDialog_Open : MonoBehaviour, IPointerClickHandler
{
    // �����X�^�[�琬���prefab
    [SerializeField] private GameObject Ms_infoPrefab;
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

    //�_�C�A���O���J��
    public void Open()
    {
        //�p�l�����̂𐶐�����
        Instantiate();
        //�s����h�~
        if (IsOpen || IsTransition) { Debug.Log("Open"); return; }
        //IsOpen�t���O�����Z�b�g
        _animator.SetBool(ParamIsOpen, true);
        //�A�j���[�V�����ҋ@
        StartCoroutine(WaitAnimation("Shown"));
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
