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

    Comment_monster Comment;
    bool isRead = false;
    public bool IsLoading { get; private set; }
    public string[][] Datas { get; set; }

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
                Comment = GetComponent<Comment_monster>();

                Comment.OnGSSLoadEnd();
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
    }    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

