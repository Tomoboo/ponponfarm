using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_click : MonoBehaviour
{
    [SerializeField] Transform parenTran;
    public ParticleSystem particle;
    private Vector3 mousePosition;
    private Vector3 spherePosition;
    Camera UIcamera;
    // Start is called before the first frame update
    void Start()
    {
        UIcamera = GameObject.Find("UI Camera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition.z = 10.0f;
            spherePosition = UIcamera.ScreenToWorldPoint(mousePosition);
            ParticleSystem click_effect = Instantiate(particle ,spherePosition, Quaternion.identity);
            click_effect.transform.SetParent(parenTran);
            click_effect.transform.SetAsLastSibling();
        }
    }
}
