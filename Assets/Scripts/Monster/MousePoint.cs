using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour
{
    private Vector3 mousePosition;
    private Vector3 objPosition;
    Camera UIcamera;
    public bool isTarget = false;

    void Awake()
    {
        UIcamera = GameObject.Find("UI Camera").GetComponent<Camera>();
    }
    void Update()
    {
        if (isTarget == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mousePosition = Input.mousePosition;
                mousePosition.z = 10.0f;
                objPosition = UIcamera.ScreenToWorldPoint(mousePosition);
                this.transform.position = objPosition;
            }
        }

    }
}
