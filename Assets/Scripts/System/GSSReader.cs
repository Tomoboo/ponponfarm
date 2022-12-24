using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class GSSReader : MonoBehaviour
{
    public string SheetID = "1ZEp4I2mvnrAIcEU5CjmcyCc-gDJ6Q4MEj-JaFO7z0L8";
    public string SheetName = "しゃべれ草";
    public UnityEvent OnLoadEnd;
    public Comment_monster Comment;
    bool isRead = false;
    public bool IsLoading { get; private set; }
    public string[][] Datas { get; set; }
    public static string[][] d;

    IEnumerator GetFromWeb()
    {
        if (isRead != true)
        {
            IsLoading = true;

            var tqx = "tqx=out:csv";
            var url = "https://docs.google.com/spreadsheets/d/" + SheetID + "/gviz/tq?" + tqx + "&sheet=" + SheetName;
            UnityWebRequest request = UnityWebRequest.Get(url);
            yield return request.SendWebRequest();

            IsLoading = false;

            var protocol_error = request.result == UnityWebRequest.Result.ProtocolError ? true : false;
            var connection_error = request.result == UnityWebRequest.Result.ConnectionError ? true : false;
            if (protocol_error || connection_error)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Datas = ConvertCSVtoJaggedArray(request.downloadHandler.text);

                OnGSSLoadEnd();
                Debug.Log("Load");
                isRead = true;
                //OnLoadEnd.Invoke();
            }
        }
    }

    public void Reload() => StartCoroutine(GetFromWeb());

    static string[][] ConvertCSVtoJaggedArray(string t)
    {
        var reader = new StringReader(t);
        reader.ReadLine(); //ヘッダ読み飛ばし
        var rows = new List<string[]>();
        while (reader.Peek() >= 0)
        {
            var line = reader.ReadLine(); //一行ずつ読み込み
            var elements = line.Split(',');
            for (var i = 0; i < elements.Length; i++)
            {
                elements[i] = elements[i].TrimStart('"').TrimEnd('"');
            }
            rows.Add(elements);
        }
        return rows.ToArray();
    }

    public void OnGSSLoadEnd()
    {
        Debug.Log("GSS Start");
        if (d == null)
        {
            d = Datas;
            for (var row = 0; row < d.Length; row++)
            {
                for (var col = 0; col < d[row].Length; col++)
                {
                    Debug.Log("[" + row + "][" + col + "]=" + d[row][col]);
                }
            }
        }
    }
    void Start()
    {
        Reload();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

