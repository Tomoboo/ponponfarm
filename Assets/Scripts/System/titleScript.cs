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
        //���ӎ������
        //SceneManager.LoadScene("hontai2", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 6.0f)//6�b�o��
        {
            SwitchScene2();
        }

        //���N���b�N���󂯕t����
        if (Input.GetMouseButtonDown(0))
            SwitchScene();

        //�E�N���b�N���󂯕t����
        if (Input.GetMouseButtonDown(1))
            SwitchScene2();

        //�~�h���N���b�N���󂯕t����
        if (Input.GetMouseButtonDown(2))
            SwitchScene();
        
    }
}
