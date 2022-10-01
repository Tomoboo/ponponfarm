using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin_Animation : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float plusSpeed = 0.00001f;
    Vector3 pos;
    bool isCall;
    // Start is called before the first frame update
    void Start()
    {
        isCall = false;
        var col = gameObject.AddComponent<CapsuleCollider2D>();
        col.size = new Vector3(2,2,2);
        gameObject.GetComponent<Renderer>().sortingOrder = 1;
        Invoke("CoinBar_Forward",2);

    }

    private void CoinBar_Forward()
    {
        GameObject coin_icon = GameObject.Find("Icon_Coin");
        pos = coin_icon.transform.position - transform.position;//　現在の位置から、コインBarオブジェクトまで進むベクトル
        transform.position += speed * Time.deltaTime * pos;
        speed += plusSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        if (IsInvoking())
            isCall = true;
        else if (isCall)
            CoinBar_Forward();
        else if (pos.magnitude < 2.0f)  //近づいたら到着したとみなす
        {
            Destroy(gameObject); 
        }
    }

}
