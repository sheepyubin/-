using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    Vector2 target, mouse;
    // Start is called before the first frame update
    void Start()
    {
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mouse=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }
}
