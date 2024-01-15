using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float speed;

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + transform.up * Time.deltaTime * speed;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position - transform.up * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + transform.right * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position - transform.right * Time.deltaTime * speed;
        }
    }
}
