using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

[RequireComponent(typeof(LoopScrollRect))]
[DisallowMultipleComponent]
public class InitOnStartMulti : MonoBehaviour, LoopScrollPrefabSource, LoopScrollMultiDataSource
{
    [SerializeField] private GameObject[] _prefabs;

    public int totalCount = -1;

    private ObjectPool<GameObject>[] _pools;

    private Dictionary<GameObject, int> _prefabIndexMap = new Dictionary<GameObject, int>();
    private void Start()
    {
        //  prefab���Ƀv�[���𐶐�����
        _pools = new ObjectPool<GameObject>[_prefabs.Length];
        for (var i = 0; i < _prefabs.Length; i++)
        {
            var prefab = _prefabs[i];
            var pool = new ObjectPool<GameObject>(
            //  �I�u�W�F�N�g��������
            () => Instantiate(prefab),
            //  �I�u�W�F�N�g���v�[������擾����鎞�̏���
            o => o.SetActive(true),
            //  �I�u�W�F�N�g���v�[���ɖ߂���鎞�̏���
            o =>
            {
                o.transform.SetParent(transform);
                o.SetActive(false);
            });
            _pools[i] = pool;
        }
        var scrollRect = GetComponent<LoopScrollRectMulti>();
        scrollRect.prefabSource = this;
        scrollRect.dataSource = this;
        scrollRect.totalCount = totalCount;
        scrollRect.RefillCells();
    }

    // LoopScrollPrefabSource�̎���
    // GameObject���V�����\���̂��߂ɕK�v�ɂȂ������ɌĂ΂��
    GameObject LoopScrollPrefabSource.GetObject(int index)
    {
        //  Index�ɉ����ĈႤPrefab���g��
        var prefabIndex = index % _prefabs.Length;
        //  �I�u�W�F�N�g�v�[������GameObject���擾
        var instance = _pools[prefabIndex].Get();
        _prefabIndexMap.Add(instance, prefabIndex);
        return instance;
    }

    // LoopScrollPrefabSource�̎���
    // GameObject���s�v�ɂȂ������ɌĂ΂��
    void LoopScrollPrefabSource.ReturnObject(Transform trans)
    {
        //  Index�ɉ������v�[�����擾���ĕԋp
        var instance = trans.gameObject;
        var prefabIndex = _prefabIndexMap[instance];
        _prefabIndexMap.Remove(instance);
        // �I�u�W�F�N�g�v�[����GameObject��ԋp
        _pools[prefabIndex].Release(instance);
    }

    // LoopScrollDataSource�̎���
    // �v�f���\������鎞�̏���������
    void LoopScrollMultiDataSource.ProvideData(Transform trans, int index)
    {
        Vector3 kero = new Vector3(1, 1, 1);
        kero.x = 1;
        kero.y = 1;
        kero.z = 1;
        trans.transform.localScale = kero;
        Debug.Log(index);
    }
}
