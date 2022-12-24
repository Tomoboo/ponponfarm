using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePoint : MonoBehaviour
{
    public bool isTouchTarget = false;
    public float baseWidth;
    public float baseHeight;

    void Update()
    {
        if (isTouchTarget == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                MovePosition();
            }
        }
    }

    private void MovePosition()
    {
        float x = baseWidth *
            (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        float y = baseHeight *
            (Input.mousePosition.y / Screen.height) - (baseHeight / 2);
        Debug.Log("mousePosition = " + Input.mousePosition);

        transform.localPosition = new Vector3(x, y, 0);

        Debug.Log(transform.localPosition);
    }
}
