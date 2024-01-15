using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneShot : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        // 돌을 시간에 맞게 지운다
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //시간 프레임과 speed에 따라 y축 바향으로 움직인다
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
