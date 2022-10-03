using UnityEngine;
using TMPro;


[RequireComponent(typeof(GSSReader))]
public class Comment_monster : MonoBehaviour
{
    protected TextMeshProUGUI comment;
    GSSReader r;
    string[][] d;
    int rnd = 0;
    bool _isRead;
    void Awake()
    {
        _isRead = false;
        //ランダムシード値を時刻により初期化
        Random.InitState(System.DateTime.Now.Second);
        rnd = Random.Range(0, 5);
        comment = GetComponent<TextMeshProUGUI>();
        r = GetComponent<GSSReader>();
        if (_isRead != true)
        {
            r.Reload();
            _isRead = true;
        }
    }

    void Start()
    {

    }

    public void OnGSSLoadEnd()
    {
        Debug.Log("GSS Start");
        r = GetComponent<GSSReader>();
        r.Reload();
        if (d == null)
        {
            d = r.Datas;
        }
        else
        {
            for (var row = 0; row < d.Length; row++)
            {
                for (var col = 0; col < d[row].Length; col++)
                {
                    Debug.Log("[" + row + "][" + col + "]=" + d[row][col]);
                }
            }

            switch (rnd)
            {
                case 0:
                    comment.text = d[0][0];
                    break;
                case 1:
                    comment.text = d[1][0];
                    break;
                case 2:
                    comment.text = d[2][0];
                    break;
                case 3:
                    comment.text = d[3][0];
                    break;
                case 4:
                    comment.text = d[4][0];
                    break;
            }

        }
    }
}
