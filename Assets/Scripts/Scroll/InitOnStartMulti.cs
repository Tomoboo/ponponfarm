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
        //  prefab毎にプールを生成する
        _pools = new ObjectPool<GameObject>[_prefabs.Length];
        for (var i = 0; i < _prefabs.Length; i++)
        {
            var prefab = _prefabs[i];
            var pool = new ObjectPool<GameObject>(
            //  オブジェクト生成処理
            () => Instantiate(prefab),
            //  オブジェクトがプールから取得される時の処理
            o => o.SetActive(true),
            //  オブジェクトがプールに戻される時の処理
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

    // LoopScrollPrefabSourceの実装
    // GameObjectが新しく表示のために必要になった時に呼ばれる
    GameObject LoopScrollPrefabSource.GetObject(int index)
    {
        //  Indexに応じて違うPrefabを使う
        var prefabIndex = index % _prefabs.Length;
        //  オブジェクトプールからGameObjectを取得
        var instance = _pools[prefabIndex].Get();
        _prefabIndexMap.Add(instance, prefabIndex);
        return instance;
    }

    // LoopScrollPrefabSourceの実装
    // GameObjectが不要になった時に呼ばれる
    void LoopScrollPrefabSource.ReturnObject(Transform trans)
    {
        //  Indexに応じたプールを取得して返却
        var instance = trans.gameObject;
        var prefabIndex = _prefabIndexMap[instance];
        _prefabIndexMap.Remove(instance);
        // オブジェクトプールにGameObjectを返却
        _pools[prefabIndex].Release(instance);
    }

    // LoopScrollDataSourceの実装
    // 要素が表示される時の処理を書く
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
