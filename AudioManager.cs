using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    string Str(float length)
    {
        int len = (int)length;
        int min = len / 60;
        int sec = len % 60;

        return $"{min}:{sec.ToString("D2")}";
    }

    void Func()
    {
        string s1 = Str(audioSource.time);
        string s2 = Str(audioSource.clip.length);
        Debug.Log($"{s1}/{s2}");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Func();
        }
    }
}
