using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleScript : MonoBehaviour
{
    float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void SwitchScene2()
    {
        //注意事項画面
        //SceneManager.LoadScene("hontai2", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 6.0f)//6秒経過
        {
            SwitchScene2();
        }

        //左クリックを受け付ける
        if (Input.GetMouseButtonDown(0))
            SwitchScene();

        //右クリックを受け付ける
        if (Input.GetMouseButtonDown(1))
            SwitchScene2();

        //ミドルクリックを受け付ける
        if (Input.GetMouseButtonDown(2))
            SwitchScene();
        
    }
}
