using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_controller : MonoBehaviour
{
    // 背景移動速度
    public float speed = 2f ; 
    // 背景尺寸
    private float backgroundSize;
    // Start is called before the first frame update
    void Start()
    {
        backgroundSize = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        print(backgroundSize);
        if(transform.position.x < -backgroundSize)
        {
            RepositionBackground();
        }
    }

    void RepositionBackground(){
        Vector3 groundOffset = new Vector3(backgroundSize * 2f, 0, 0);
        transform.position = (Vector3)transform.position + groundOffset;
    }
}
