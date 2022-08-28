using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clickeffect : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] float span = 0.5f;
    void Start()
    {
        Destroy(this.gameObject, span);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
