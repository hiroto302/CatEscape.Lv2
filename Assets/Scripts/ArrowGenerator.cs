using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    public GameObject arrowPrefab;
    float span = 1.0f;
    float delta = 0;
    float speed = -0.1f;

    public bool readyTime;  //スタートするまで矢の生産停止

    public void SetParameter(float span, float speed)
    {
        this.span = span;
        this.speed = speed;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(readyTime) return;  //ここで処理を返すことで、以下の処理を停止状態のようにすることができる
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject arrow = Instantiate(arrowPrefab) as GameObject;
            int px = Random.Range(-6, 7);
            arrow.transform.position = new Vector3(px, 7, 0);
            arrow.GetComponent<ArrowController>().arrowSpeed =this.speed;
        }
    }
}
